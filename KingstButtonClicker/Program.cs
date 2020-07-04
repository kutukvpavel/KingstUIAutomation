﻿using UIAutomationTool.Properties;
using NamedPipeWrapper;
using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace UIAutomationTool
{
    public enum ExitCodes
    {
        UnhandledException = -2,
        AlreadyRunning = -1,
        OK = 0,
        SerializationError,
        FileIOError,
        FileNotFound
    }

    public static class Program
    {
        public const string WindowTitleFileName = "title.txt";
        public const string DatabaseFileName = "database.xml";
        public const string ScenarioFileName = "scenario.xml";
        public const string ExampleDatabaseName = "example_database.xml";
        public const string ExampleScenarioName = "example_scenario.xml";
        public const string PipeName = "MyUIAutomationPipe";

        public static string WindowTitleString = "Example";
        public static PointDatabase Database = new PointDatabase()
        {
            new ClickPoint(0, 0, PointReference.TopLeft, "Origin")
        };
        public static SimulatorScenario Scenario = new SimulatorScenario(
            new SimulatorAction(ActionTypes.MouseClick, "Origin"),
            new SimulatorAction(ActionTypes.Sleep, 500)
            );

        public static readonly PointDatabase ExampleDatabase = new PointDatabase()
        {
            new ClickPoint(0, 0, PointReference.TopLeft, "Origin")
        };
        public static readonly SimulatorScenario ExampleScenario = new SimulatorScenario(
            new SimulatorAction(ActionTypes.MouseClick, "PointName"),
            new SimulatorAction(ActionTypes.PressKey, WindowsInput.Native.VirtualKeyCode.RETURN),
            new SimulatorAction(ActionTypes.WaitForPixel, "PointName", Color.FromArgb(255, 35, 35, 35)),
            new SimulatorAction(ActionTypes.Sleep, 500)
            )
        { };

        #region Main

        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        public static int Main()
        {
            //Single-instance only
            using (var mutex = new Mutex(false, string.Format("Global\\{{{0}}}",
                ((GuidAttribute)Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(GuidAttribute), false)
                .GetValue(0)).Value.ToString())))
            {
                var hasHandle = false;
                try
                {
                    var previousExitedNormally = true;
                    try
                    {
                        hasHandle = mutex.WaitOne(5000, false);
                        if (!hasHandle) return (int)ExitCodes.AlreadyRunning;
                    }
                    catch (AbandonedMutexException)
                    {
                        hasHandle = true;
                        previousExitedNormally = false;
                    }
                    try
                    {
                        InstanceMain(previousExitedNormally);
                        return (int)ExitCodes.OK;
                    }
                    catch (SerializationException)
                    {
                        return (int)ExitCodes.SerializationError;
                    }
                    catch (FileNotFoundException)
                    {
                        return (int)ExitCodes.FileNotFound;
                    }
                    catch (IOException)
                    {
                        return (int)ExitCodes.FileIOError;
                    }
                    catch (Exception ex)
                    {
                        ErrorListener.AddFormat(ex, "Unhandled exception!");
                    }
                    return (int)ExitCodes.UnhandledException;
                }
                finally
                {
                    if (hasHandle)
                        mutex.ReleaseMutex();
                }
            }
        }
        private static void InstanceMain(bool prevExitOk = true)
        {
            if (!prevExitOk)
            {
                ErrorListener.EnableMessages = false;
                ErrorListener.Add(new Exception("Previous instance exited abnormally!"));
            }
            LoadSettings();
            //Prepare serialized objects
            Database = Serialization.ReadDatabase(Database);
            Scenario = Serialization.ReadScenario(Scenario);
            WindowTitleString = Serialization.ReadWindowTitle(WindowTitleString);
            //Init pipeline
            pipeClient = new NamedPipeClient<string>(PipeName);
            //Start WinForms
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
        private static void LoadSettings()
        {
            ErrorListener.EnableMessages = Settings.Default.EnableMessages;
            ErrorListener.EnableLog = Settings.Default.EnableLog;
        }

        #endregion

        #region Pipes
        private static NamedPipeClient<string> pipeClient;
        private static CancellationTokenSource pipeCancellation;

        public static event EventHandler<PipeEventArgs> PipeCommandReceived;

        public static void SetPipeOperation(bool operate)
        {
            if (operate)
            {
                pipeClient.ServerMessage += PipeClient_ServerMessage;
                pipeClient.Start();
            }
            else
            {
                if (pipeCancellation != null) pipeCancellation.Cancel();
                pipeClient.ServerMessage -= PipeClient_ServerMessage;
                pipeClient.Stop();
            }
        }
        private static void PipeClient_ServerMessage(NamedPipeConnection<string, string> connection, string message)
        {
            if (pipeCancellation != null) return;
            PipeCommandReceived?.Invoke(null, new PipeEventArgs(message));
            int res = -1;
            switch (message)
            {
                case PipeCommands.ExecuteScenario:
                    pipeCancellation = new CancellationTokenSource();
                    res = Scenario.Execute(pipeCancellation);
                    break;
                case PipeCommands.LoopScenario:
                    pipeCancellation = new CancellationTokenSource();
                    res = Scenario.Loop(pipeCancellation);
                    break;
                case PipeCommands.StopScenario:
                    pipeCancellation.Cancel();
                    break;
                default:
                    break;
            }
            pipeCancellation = null;
            pipeClient.PushMessage(res.ToString());
        }

        public class PipeEventArgs : EventArgs
        {
            public PipeEventArgs(string s)
            {
                PipeCommand = s;
            }

            public string PipeCommand { get; }
        }


        #endregion
    }
}
