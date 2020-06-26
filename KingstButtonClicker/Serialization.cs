using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace KingstButtonClicker
{
    public static class Serialization
    {
        public static readonly string DatabasePath = Path.Combine(Environment.CurrentDirectory, Program.DatabaseFileName);
        public static readonly string ScenarioPath = Path.Combine(Environment.CurrentDirectory, Program.ScenarioFileName);

        private static readonly Type[] databaseKnown = new Type[] { typeof(Point), typeof(PointReference), typeof(ClickPoint) };
        private static readonly Type[] scenarioKnown = new Type[] { typeof(SimulatorAction), typeof(ActionTypes), typeof(Color) };

        public static string ReadWindowTitle(string defaultValue)
        {
            string p = Path.Combine(Environment.CurrentDirectory, Program.WindowTitleFileName);
            if (File.Exists(p)) return File.ReadAllText(p);
            return defaultValue;
        }
        public static void WriteDatabase(PointDatabase database, string path)
        {
            Write(path, database, typeof(PointDatabase), databaseKnown);
        }
        public static void WriteDatabase(PointDatabase database)
        {
            WriteDatabase(database, DatabasePath);
        }
        public static PointDatabase ReadDatabase(PointDatabase defaultValue)
        {
            return (PointDatabase)Read(DatabasePath, defaultValue, typeof(PointDatabase), databaseKnown);
        }
        public static void WriteScenario(SimulatorScenario scenario, string path)
        {
            Write(path, scenario, typeof(SimulatorScenario), scenarioKnown);
        }
        public static void WriteScenario(SimulatorScenario scenario)
        {
            WriteScenario(scenario, ScenarioPath);
        }
        public static SimulatorScenario ReadScenario(SimulatorScenario defaultValue)
        {
            return (SimulatorScenario)Read(ScenarioPath, defaultValue, typeof(SimulatorScenario), scenarioKnown);
        }

        private static void Write(string path, object data, Type type, params Type[] knownTypes)
        {
            //Backup data just in case
            try
            {
                if (File.Exists(path)) File.Copy(path, Path.ChangeExtension(path, "bak"), true);
            }
            catch (Exception ex)
            {
                ErrorListener.Add(ex);
                if (MessageBox.Show("Failed to backup current database/scenario. Continue anyway?",
                    Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
            }
            //Serialize
            try
            {
                using (var fs = new FileStream(path, FileMode.Create))
                using (var writer = XmlWriter.Create(fs, new XmlWriterSettings() { Indent = true, IndentChars = "\t" }))
                {
                    DataContractSerializer ser = new DataContractSerializer(type, knownTypes);
                    try
                    {
                        ser.WriteObject(writer, data);
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
        private static object Read(string path, object defaultValue, Type type, params Type[] knownTypes)
        {
            try
            {
                if (!File.Exists(path))
                {
                    MessageBox.Show("No database/scenario file present! Using default settings.", Application.ProductName,
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    Write(path, defaultValue, type, knownTypes);
                    return defaultValue;
                }
                using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
                using (XmlDictionaryReader reader = XmlDictionaryReader.CreateTextReader(fs, new XmlDictionaryReaderQuotas()))
                {
                    DataContractSerializer ser = new DataContractSerializer(type, knownTypes);
                    try
                    {
                        return ser.ReadObject(reader);
                    }
                    catch (Exception ex)
                    {
                        ErrorListener.Add(ex);
                        MessageBox.Show("Failed to deserialize the database/scenario. The application will now terminate.",
                            Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Environment.Exit(1);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorListener.Add(ex);
                MessageBox.Show("Failed to open the database/scenario file. The application will now terminate.",
                    Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(2);
            }
            return null;  //Not really gonna reach here
        }
    }
}
