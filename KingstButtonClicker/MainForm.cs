using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using WindowsInput;
using KingstButtonClicker.Properties;

namespace KingstButtonClicker
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }
        private void PrintDatabase()
        {
            txtOutput.AppendText(Environment.NewLine);
            txtOutput.AppendText("Database contents:" + Environment.NewLine);
            txtOutput.AppendText(string.Join(Environment.NewLine, Program.Database.Select(x => string.Format("{0} = {1} ({2})",
                x.Name, x.RawPoint.ToString(), Enum.GetName(typeof(PointReference), x.RawReference)))));
            txtOutput.AppendText(Environment.NewLine);
        }
        private void LoadSettings()
        {
            enableLogToolStripMenuItem.Checked = Settings.Default.EnableLog;
            enableMessagesToolStripMenuItem.Checked = Settings.Default.EnableMessages;
            enablePipeClientToolStripMenuItem.Checked = Settings.Default.EnablePipeClient;
        }

        private void testColorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var tester = new ColorTester(Program.Database.ToArray());
            txtOutput.AppendText("Current colors of database points:" + Environment.NewLine);
            Hide();
            System.Threading.Thread.Sleep(500);
            txtOutput.AppendText(tester.GetReport());
            Show();
            txtOutput.AppendText(Environment.NewLine);
        }

        private void recordPointsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new RecordingOverlay();
            Hide();
            form.ShowDialog();
            Show();
            Program.Database.AddRange(form.Points);
            PrintDatabase();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            Show();
        }

        private void clearOutputToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtOutput.Clear();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var form = new AboutBox())
            {
                form.ShowDialog();
            }
        }

        private void saveCurrentDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Serialization.WriteDatabase(Program.Database);
        }

        private void clearCurrentDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.Database.Clear();
        }

        private void editScenarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(Serialization.ScenarioPath);
        }

        private void editDatabasemanualToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(Serialization.DatabasePath);
        }

        private void updateDatabaseFromFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.Database = Serialization.ReadDatabase(Program.Database);
            PrintDatabase();
        }

        private void executeScenarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hide();
            txtOutput.AppendText(Environment.NewLine + "Scenario exit code: ");
            txtOutput.AppendText(Program.Scenario.Execute().ToString() + Environment.NewLine);
            Show();
        }

        private void moveMouseToToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var inputBox = new InputBox() { Text = "Enter target coordinates (...x...):" };
            if (inputBox.ShowDialog() == DialogResult.OK)
            {
                int[] parsed = inputBox.Value.Split('x').Select(x => int.Parse(x)).ToArray();
                /*var instance = new InputSimulator();
                instance.Mouse.MoveMouseToPositionOnVirtualDesktop(0, 0).MoveMouseBy(parsed[0], parsed[1]);*/
                Native.SetCursorPosition(new Point(parsed[0], parsed[1]));
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void updateScenarioFromFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.Scenario = Serialization.ReadScenario(Program.Scenario);
        }

        private void showExampleScenarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string path = Path.Combine(Environment.CurrentDirectory, Program.ExampleScenarioName);
                Serialization.WriteScenario(Program.ExampleScenario, path);
                Process.Start(path); 
            }
            catch (Exception ex)
            {
                ErrorListener.Add(ex);
            }
        }

        private void showExampleDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string path = Path.Combine(Environment.CurrentDirectory, Program.ExampleDatabaseName);
                Serialization.WriteDatabase(Program.ExampleDatabase, path);
                Process.Start(path);
            }
            catch (Exception ex)
            {
                ErrorListener.Add(ex);
            }
        }

        private void enablePipeClientToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            //UI
            var menuItems = Controls.OfType<ToolStripMenuItem>();
            foreach (var item in menuItems)
            {
                item.Enabled = enablePipeClientToolStripMenuItem.Checked;
            }
            enablePipeClientToolStripMenuItem.Enabled = true;
            executeToolStripMenuItem.Enabled = true;
            //Pipes
            if (enablePipeClientToolStripMenuItem.Checked)
            {
                Program.StartPipeOperation();
            }
            else
            {
                Program.StopPipeOperation();
            }
        }

        private void loopThroughScenarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Not implemented
        }

        private void enableMessagesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings.Default.EnableMessages = enableLogToolStripMenuItem.Checked;
            ErrorListener.EnableMessages = enableLogToolStripMenuItem.Checked;
        }

        private void enablePipeOnStartupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings.Default.EnablePipeClient = enablePipeOnStartupToolStripMenuItem.Checked;
        }
    }
}
