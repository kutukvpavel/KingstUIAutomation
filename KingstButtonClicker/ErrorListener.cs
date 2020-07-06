using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace UIAutomationTool
{
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
                int wait = 5;
                while (wait > 0)
                {
                    try
                    {
                        File.AppendAllText(logFilePath,
                            string.Format(LogLineFormat, DateTime.Now.ToString(LogDatetimeFormat), e.ToString()));
                    }
                    catch (IOException)
                    {
                        System.Threading.Thread.Sleep(100);
                    }
                    catch (Exception)
                    {
                        if (EnableMessages) MessageBox.Show("Failed to save error log!",
                            Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        break;
                    }
                    wait--;
                }
            }
        }

        public static void AddFormat(Exception e, string f, params object[] args)
        {
            Add(new Exception(string.Format(f, args), e));
        }
    }
}
