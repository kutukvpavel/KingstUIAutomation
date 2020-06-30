using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using NamedPipeWrapper;

namespace YaskawaEncoderListener
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                instanceMutex = new Mutex(false, @"Global\YaskawaEncoderListener");
                if (!instanceMutex.WaitOne(1000)) return;
            }
            catch (AbandonedMutexException)
            {
                //Previous instance did not exit gracefully
            }

            try
            {
                InitServer("MyUIAutomationPipe");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            while (clients == 1) //Spin forever until the client shuts down
            {
                Thread.Sleep(10);
            }

            instanceMutex.ReleaseMutex();
        }

        private static Mutex instanceMutex;
        private static NamedPipeServer<string> server;
        private static int clients = 0;

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

        public static void PipeCommandReceived(string cmd)
        {
            try
            {
                ScenarioExitCodes code = (ScenarioExitCodes)int.Parse(cmd);

            }
            catch (FormatException)
            {

            }
            catch (InvalidCastException)
            {
                
            }
        }

        public static void Log(string txt)
        {

        }
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
