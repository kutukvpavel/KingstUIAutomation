using System;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization;
using System.Windows.Forms;
using System.Xml;

namespace UIAutomationTool
{
    public static class Serialization
    {
        public static readonly string DatabasePath = Path.Combine(Environment.CurrentDirectory, Program.DatabaseFileName);
        public static readonly string ScenarioPath = Path.Combine(Environment.CurrentDirectory, Program.ScenarioFileName);

        private static readonly Type[] databaseKnown = new Type[] { typeof(Point), typeof(PointReference), typeof(ClickPoint) };
        private static readonly Type[] scenarioKnown = new Type[] 
        { typeof(SimulatorAction), typeof(ActionTypes), typeof(Color), typeof(WindowsInput.Native.VirtualKeyCode) };

        public static string ReadWindowTitle(string defaultValue)
        {
            string p = Path.Combine(Environment.CurrentDirectory, Program.WindowTitleFileName);
            if (File.Exists(p))
            {
                return File.ReadAllText(p);
            }
            else
            {
                if (MessageBox.Show("No window title file found. Continue with 'Example'?", Application.ProductName,
                    MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No) throw new FileNotFoundException();
            }
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
                        throw new SerializationException(); //This is fatal, terminate
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorListener.Add(ex);
                MessageBox.Show("Failed to open the database/scenario file. The application will now terminate.",
                    Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw new IOException(); //This is fatal, terminate
            }
        }
    }

    public class SerializationException : Exception
    {

    }
}
