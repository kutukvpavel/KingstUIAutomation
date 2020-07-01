using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using NamedPipeWrapper;
using System.Runtime.InteropServices;
using System.Reflection;
using System.IO;

namespace YaskawaEncoderListener
{
    public static class Program
    {
        public static string ExportFolderPath { get { return Properties.Settings.Default.ExportFolderPath; } }

        private static NamedPipeServer<string> server;
        private static int clients = 0;

        #region Main

        private static void Main(string[] args)
        {
            //Single-instance only
            using (var mutex = new Mutex(false, string.Format("Global\\{{{0}}}",
                ((GuidAttribute)Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(GuidAttribute), false)
                .GetValue(0)).Value.ToString())))
            {
                var hasHandle = false;
                try
                {
                    try
                    {
                        hasHandle = mutex.WaitOne(5000, false);
                        if (!hasHandle) return;
                    }
                    catch (AbandonedMutexException)
                    {
                        hasHandle = true;
                    }
                    InstanceMain(args);
                }
                finally
                {
                    if (hasHandle)
                        mutex.ReleaseMutex();
                }
            }
        }
        private static void InstanceMain(string[] args)
        {
            try
            {
                InitServer("MyUIAutomationPipe");
                SendInitialExecutionCommand();
                while (clients == 1) //Spin forever until the client shuts down
                {
                    Thread.Sleep(10);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        #endregion

        #region Pipes Init

        private static void InitServer(string pipeName)
        {
            server = new NamedPipeServer<string>(pipeName);

            server.ClientConnected += delegate (NamedPipeConnection<string, string> conn)
            {
                Console.WriteLine("Client {0} is now connected!", conn.Id);
                clients += 1;
            };
            server.ClientDisconnected += delegate (NamedPipeConnection<string, string> conn)
            {
                Console.WriteLine("Client {0} disconnected!", conn.Id);
                clients -= 1;
            };
            server.ClientMessage += delegate (NamedPipeConnection<string, string> conn, string message)
            {
                Console.WriteLine("Client {0} says: {1}", conn.Id, message);
                PipeCommandReceived(message);
            };

            server.Start();
            int prevClients = 0;
            Console.WriteLine("Waiting for client...");
            while (clients == 0) Thread.Sleep(1000);
            prevClients = clients;
            Thread.Sleep(1000);
            while (clients != prevClients) Thread.Sleep(1000);
            if (clients != 1) throw new Exception("More than 1 client instance has connected!");
        }

        public static void SendPipeCommand(string cmd)
        {
            server.PushMessage(cmd);
        }

        #endregion

        #region Payload

        /// <summary>
        /// First execution command has to be sent explicitly. All following commands are in fact responses to responses.
        /// </summary>
        private static void SendInitialExecutionCommand()
        {
            SendPipeCommand(PipeCommands.ExecuteScenario);
        }

        private static void PipeCommandReceived(string cmd)
        {
            try
            {
                ScenarioExitCodes code = (ScenarioExitCodes)int.Parse(cmd);
                switch (code)
                {
                    case ScenarioExitCodes.OK:
                        if (CheckFile())
                        {
                            Console.WriteLine("False alarm. Continuing scenario execution...");
                            //SendPipeCommand(PipeCommands.ExecuteScenario);
                        }
                        break;
                    case ScenarioExitCodes.WindowNotFound:
                        break;
                    case ScenarioExitCodes.EmptyWindow:
                        break;
                    case ScenarioExitCodes.WrongArguments:
                        break;
                    case ScenarioExitCodes.Timeout:
                        break;
                    case ScenarioExitCodes.UnexpectedError:
                        break;
                    default:
                        break;
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Client has sent a non-numeric response!");
            }
            catch (InvalidCastException)
            {
                Console.WriteLine("Client has sent an invalid exit code!");
            }
            catch (Exception e)
            {
                Console.WriteLine("Can't react to a client response:");
                Console.WriteLine(e.ToString());
            }
        }

        private static bool CheckFile()
        {
            try
            {
                var files = Directory.GetFiles(ExportFolderPath).OrderBy((x) => { return File.GetCreationTime(x); });
                string lastFile = files.Last();
                string lastLine = File.ReadAllLines(lastFile).Last(x => x.Length > 0);
                if (lastLine.Split(',')[Properties.Settings.Default.ColumnIndex].Trim() == Properties.Settings.Default.AlarmText)
                {
                    return false;
                }
                else
                {
                    File.Delete(lastFile);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Can't check file contents:");
                Console.WriteLine(e.ToString());
            }
            return true; //true == continue scenario execution
        }

        #endregion
    }

    public static class PipeCommands
    {
        public const string ExecuteScenario = "Exec";
        public const string LoopScenario = "Loop";
        public const string StopScenario = "Stop";
    }

    public enum ScenarioExitCodes
    {
        OK = 0,
        WindowNotFound,
        EmptyWindow,
        WrongArguments,
        Timeout,
        UnexpectedError
    }
}
