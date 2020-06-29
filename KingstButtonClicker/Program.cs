using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.Xml;
using System.Runtime.Serialization;
using System.Threading;
using KingstButtonClicker.

namespace KingstButtonClicker
{
    public static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            //Prepare serialized objects
            Database = Serialization.ReadDatabase(Database);
            Scenario = Serialization.ReadScenario(Scenario);
            WindowSearchString = Serialization.ReadWindowTitle(WindowSearchString);
            //Init pipeline
            PipeClient.Instance.CommandReceived += Instance_CommandReceived;
            //Start WinForms
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
        private static void LoadSettings()
        {
            if ()
            {

            }
        }

        public const string WindowTitleFileName = "title.txt";
        public const string DatabaseFileName = "database.xml";
        public const string ScenarioFileName = "scenario.xml";
        public const string ExampleDatabaseName = "example_database.xml";
        public const string ExampleScenarioName = "example_scenario.xml";

        public static string WindowSearchString = "LA1010 Unconnected - KingstVIS";
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
            );

        private static CancellationTokenSource pipeCancellationToken;
        private static Thread pipeOperationThread;

        public static void StartPipeOperation()
        {
            ErrorListener.EnableMessages = false;
            pipeCancellationToken = new CancellationTokenSource();
            pipeOperationThread = new Thread(() => 
            {
                PipeClient.Instance.DispatchPipe(pipeCancellationToken);
            });
        }
        public static void StopPipeOperation()
        {
            pipeCancellationToken.Cancel();
            pipeOperationThread.Join();
            pipeCancellationToken.Dispose();
            pipeCancellationToken = null;
            pipeOperationThread = null;
            ErrorListener.EnableMessages = Properties.Settings.Default.EnableMessages;
        }
        private static void Instance_CommandReceived(object sender, PipeEventArgs e)
        {
            switch (e.Request)
            {   
                case PipeCommands.ExecuteScript:
                    e.Response = (byte)Scenario.Execute();
                    break;
                case PipeCommands.LoopScript:   //Not implemented
                    break;
                case PipeCommands.StopScript:
                    break;
                default:
                    break;
            }
        }
    }

    public static class ErrorListener
    {
        public static readonly string LogFileName = "log.txt";

        public static bool EnableMessages = true;
        public static bool EnableLog = true;
        public static object LockObject = new object();
        public static string LogDatetimeFormat = "G";
        public static string LogLineFormat = "{0} | {1}" + Environment.NewLine;

        private static string logFilePath = Path.Combine(Environment.CurrentDirectory, LogFileName);
        private static List<Exception> data = new List<Exception>();

        public static void Add(Exception e)
        {
            data.Add(e);
            if (EnableMessages) MessageBox.Show(e.Message, e.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            if (EnableLog)
            {
                try
                {
                    File.AppendAllText(logFilePath, 
                        string.Format(LogLineFormat, DateTime.Now.ToString(LogDatetimeFormat), e.ToString()));
                }
                catch (Exception)
                { }
                finally
                {
                    if (EnableMessages) MessageBox.Show("Failed to save error log!", 
                        Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        public static void AddFormat(Exception e, string f, params object[] args)
        {
            Add(new Exception(string.Format(f, args), e));
        } 
    }
}
