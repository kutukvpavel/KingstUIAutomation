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
    /// <summary>
    /// Exit codes for the application itself
    /// </summary>
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
        /// <summary>
        /// This file is expected to exist (unless it's the first run) inside the working directory
        /// </summary>
        public const string WindowTitleFileName = "title.txt";
        /// <summary>
        /// This file is expected to exist (unless it's the first run) inside the working directory
        /// </summary>
        public const string DatabaseFileName = "database.xml";
        /// <summary>
        /// This file is expected to exist (unless it's the first run) inside the working directory
        /// </summary>
        public const string ScenarioFileName = "scenario.xml";
        /// <summary>
        /// This file is created on demand (or overwritten!) by the application
        /// </summary>
        public const string ExampleDatabaseName = "example_database.xml";
        /// <summary>
        /// This file is created on demand (or overwritten!) by the application
        /// </summary>
        public const string ExampleScenarioName = "example_scenario.xml";
        /// <summary>
        /// Pipe server application has to know this string
        /// </summary>
        public const string PipeName = "MyUIAutomationPipe";

        /// <summary>
        /// The title of the window to look for before scenario execution (normally is read from the file, defaults to Example)
        /// </summary>
        public static string WindowTitleString = "Example";
        /// <summary>
        /// Current database. Has to be saved to disk (serialized) explicitly!
        /// </summary>
        public static PointDatabase Database = new PointDatabase()
        {
            new ClickPoint(0, 0, PointReference.TopLeft, "Origin")
        };
        /// <summary>
        /// Current scenario. Is normally (unless first run) read from the file. Can't be edited inside the application (yet).
        /// </summary>
        public static SimulatorScenario Scenario = new SimulatorScenario(
            new SimulatorAction(ActionTypes.MouseClick, "Origin"),
            new SimulatorAction(ActionTypes.Sleep, 500)
            );

        /// <summary>
        /// Constant example
        /// </summary>
        public static readonly PointDatabase ExampleDatabase = new PointDatabase()
        {
            new ClickPoint(0, 0, PointReference.TopLeft, "Origin")
        };
        /// <summary>
        /// Constant example
        /// </summary>
        public static readonly SimulatorScenario ExampleScenario = new SimulatorScenario(
            new SimulatorAction(ActionTypes.MouseClick, "PointName"),
            new SimulatorAction(ActionTypes.RightMouseClick, "PointName"),
            new SimulatorAction(ActionTypes.LeftMouseDoubleClick, "PointName"),
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
                    catch (System.Runtime.Serialization.SerializationException)
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

        /// <summary>
        /// Starts or stops the pipe listener, manages its events.
        /// </summary>
        /// <param name="operate">True = enable operation</param>
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

        /// <summary>
        /// Contains received command (string identifier)
        /// </summary>
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
