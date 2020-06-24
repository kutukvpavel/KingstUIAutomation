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
            ReadDatabase(Path.Combine(Environment.CurrentDirectory, DatabaseFileName));
            //Start WinForms
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }

        public static string WindowSearchString = "LA1010 Connected - KingstVIS";
        public static readonly string DatabaseFileName = "database.xml";
        public static readonly string LogFileName = "log.txt";

        public static PointDatabase Database = new PointDatabase()
        {
            new ClickPoint(0, 0, PointReference.TopLeft, "Start")
        };


        public static void WriteDatabase(string path)
        {
            //Backup data just in case
            try
            {
                File.Copy(path, Path.ChangeExtension(path, "bak"), true);
            }
            catch (Exception ex)
            {
                ErrorListener.Add(ex);
                if (MessageBox.Show("Failed to backup current database. Continue anyway?",
                    Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
            }
            //Serialize
            try
            {
                using (var fs = new FileStream(path, FileMode.Create))
                using (var writer = XmlDictionaryWriter.CreateTextWriter(fs))
                {
                    DataContractSerializer ser = new DataContractSerializer(typeof(PointDatabase));
                    try
                    {
                        ser.WriteObject(writer, Database);
                    }
                    catch (Exception ex)
                    {
                        ErrorListener.Add(ex);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorListener.Add(ex);
            }

        }
        public static void ReadDatabase(string path)
        {
            try
            {
                if (!File.Exists(path))
                {
                    MessageBox.Show("No database file present! Using default settings.");
                    return;
                }
                using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
                using (XmlDictionaryReader reader = XmlDictionaryReader.CreateTextReader(fs, new XmlDictionaryReaderQuotas()))
                {
                    DataContractSerializer ser = new DataContractSerializer(typeof(PointDatabase));
                    try
                    {
                        Database = (PointDatabase)ser.ReadObject(reader);
                    }
                    catch (Exception ex)
                    {
                        ErrorListener.Add(ex);
                        MessageBox.Show("Failed to deserialize the database. The application will now terminate.",
                            Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Environment.Exit(1);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorListener.Add(ex);
                MessageBox.Show("Failed to open the database file. The application will now terminate.",
                    Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(2);
            }
        }
    }

    public static class ErrorListener
    {
        static List<Exception> data = new List<Exception>();

        public static bool EnableMessages = true;
        public static bool EnableLog = true;
        public static object LockObject = new object();
        public static string LogDatetimeFormat = "G";
        public static string LogLineFormat = "{0} | {1}" + Environment.NewLine;

        private static string logFilePath = Path.Combine(Environment.CurrentDirectory, Program.LogFileName);

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

        public static string GetReport()
        {
            Exception[] arr = null;
            lock (LockObject)
            {
                arr = data.ToArray();
                data.Clear();
            }
            StringBuilder res = new StringBuilder();
            for (int i = 0; i < arr.Length; i++)
            {
                res.AppendLine(arr[i].Message);
            }
            return res.ToString();
        }
        
         
    }
}
