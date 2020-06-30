using UIAutomationTool.Properties;
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

        public static string WindowSearchString = "Example";
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

        private static Mutex instanceMutex = new Mutex(true, @"Global\{0}");
        private static NamedPipeClient<string> pipeClient;

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
            WindowSearchString = Serialization.ReadWindowTitle(WindowSearchString);
            //Init pipeline
            pipeClient = new NamedPipeClient<string>(PipeName);
            pipeClient.ServerMessage += PipeClient_ServerMessage;
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

        public static void SetPipeOperation(bool operate)
        {
            if (operate)
            {
                pipeClient.Start();
            }
            else
            {
                pipeClient.Stop();
            }
        }
        private static void PipeClient_ServerMessage(NamedPipeConnection<string, string> connection, string message)
        {
            int res = -1;
            switch (message)
            {
                case PipeCommands.ExecuteScenario:
                    res = Scenario.Execute();
                    break;
                case PipeCommands.LoopScenario:
                    res = Scenario.Loop();
                    break;
                case PipeCommands.StopScenario:

                default:
                    break;
            }
            pipeClient.PushMessage(res.ToString());
        }

        #endregion
    }
}
