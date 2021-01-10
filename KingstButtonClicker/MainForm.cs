using UIAutomationTool.Properties;
using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UIAutomationTool
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Show database contents inside the console textbox
        /// </summary>
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
            enablePipeOnStartupToolStripMenuItem.Checked = Settings.Default.EnablePipeClient;
            disableWindowFilterToolStripMenuItem.Checked = Settings.Default.DisableWindowFilter;
        }

        #region Non-UI events

        private void SimulatorScenario_ScenarioExecuted(object sender, ScenarioEventArgs e)
        {
            Invoke((Action)(() =>
            {
                //txtOutput.AppendText(Environment.NewLine);
                txtOutput.AppendText("Scenario exit code: " + e.ExitCode.ToString());
                txtOutput.AppendText(Environment.NewLine);
            }));
        }

        private void Program_PipeCommandReceived(object sender, Program.PipeEventArgs e)
        {
            Invoke((Action)(() =>
            {
                txtOutput.AppendText(Environment.NewLine);
                txtOutput.AppendText("Pipe command received: " + e.PipeCommand);
                txtOutput.AppendText(Environment.NewLine);
            }));
        }

        #endregion

        #region Form Events

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
            else
            {
                Program.PipeCommandReceived -= Program_PipeCommandReceived;
                SimulatorScenario.ScenarioExecuted -= SimulatorScenario_ScenarioExecuted;
                Settings.Default.Save();
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadSettings();
            Program.PipeCommandReceived += Program_PipeCommandReceived;
            SimulatorScenario.ScenarioExecuted += SimulatorScenario_ScenarioExecuted;
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            enablePipeClientToolStripMenuItem.Checked = Settings.Default.EnablePipeClient;
        }

        #endregion

        #region Menu Events

        private void minimizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings.Default.Save();
            Hide();
        }

        private void testColorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var tester = new ColorTester(Program.Database.ToArray());
            txtOutput.AppendText("Current colors of database points:" + Environment.NewLine);
            Hide();
            Thread.Sleep(500);
            txtOutput.AppendText(tester.GetReport());
            Show();
            txtOutput.AppendText(Environment.NewLine);
        }

        private void recordPointsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new RecordingOverlay
            {
                Points = Program.Database.ToArray()
            };
            Hide();
            form.ShowDialog();
            Show();
            Program.Database.Clear();
            Program.Database.AddRange(form.Points);
            PrintDatabase();
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
            Settings.Default.Save();
            Application.Exit();
        }

        private void updateScenarioFromFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtOutput.AppendText("Reading scenario... ");
            try
            {
                Program.Scenario = Serialization.ReadScenario(Program.Scenario);
                txtOutput.AppendText("OK!");
            }
            catch (Exception ex)
            {
                txtOutput.AppendText(Environment.NewLine);
                txtOutput.AppendText(ex.Message + Environment.NewLine);
            }
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

        private void enableLogToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.EnableLog = enableLogToolStripMenuItem.Checked;
            ErrorListener.EnableLog = enableLogToolStripMenuItem.Checked;
        }

        private void enableMessagesToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.EnableMessages = enableLogToolStripMenuItem.Checked;
            ErrorListener.EnableMessages = enableLogToolStripMenuItem.Checked;
        }

        private void enablePipeOnStartupToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.EnablePipeClient = enablePipeOnStartupToolStripMenuItem.Checked;
        }

        private void editWindowTitleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(Serialization.WindowTitlePath);
        }

        private void updateWindowTitleFromFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtOutput.AppendText(Environment.NewLine + "Window title: ");
            Program.WindowTitleString = Serialization.ReadWindowTitle(Program.WindowTitleString);
            txtOutput.AppendText(Program.WindowTitleString + Environment.NewLine);
        }

        private void activateWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
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
            if (enablePipeClientToolStripMenuItem.Checked) Hide();
            //Pipes
            Program.SetPipeOperation(enablePipeClientToolStripMenuItem.Checked);
        }

        private CancellationTokenSource loopCancellation = null;
        private void loopThroughScenarioToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            TaskWithCancellation(loopThroughScenarioToolStripMenuItem.Checked,
                (t) => { return Program.Scenario.Loop(t); }, loopCancellation);
            loopThroughScenarioToolStripMenuItem.Checked = false;
        }

        private CancellationTokenSource execCancellation = null;
        private void executeScenarioToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            TaskWithCancellation(executeScenarioToolStripMenuItem.Checked,
                (t) => { return Program.Scenario.Execute(t); }, execCancellation);
            executeScenarioToolStripMenuItem.Checked = false;
        }

        #endregion

        #region Other UI Events

        private void txtOutput_TextChanged(object sender, EventArgs e)
        {
            if (txtOutput.Lines.Length > 100)
            {
                txtOutput.Lines = txtOutput.Lines.Skip(10).ToArray();
            }
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            Show();
        }

        #endregion

        /// <summary>
        /// Start/stop a task, wait for its completion (but don't block events)
        /// </summary>
        /// <param name="startStop"></param>
        /// <param name="act"></param>
        /// <param name="token"></param>
        private void TaskWithCancellation(bool startStop, Func<CancellationTokenSource, int> act,
            CancellationTokenSource token)
        {
            if (!startStop)
            {
                if (token != null) token.Cancel();
                return;
            }
            Hide();
            try
            {
                token = new CancellationTokenSource();
                Task<int> exec = new Task<int>(delegate () { return act(token); });
                exec.Start();
                while (!exec.IsCompleted)
                {
                    Thread.Sleep(100);
                    Application.DoEvents();
                }
            }
            catch (Exception ex)
            {
                ErrorListener.Add(ex);
                txtOutput.AppendText(ex.Message);
            }
            txtOutput.AppendText(Environment.NewLine);
            Show();
        }

        private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            exitToolStripMenuItem_Click(sender, e);
        }

        private void disableWindowFilterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings.Default.DisableWindowFilter = disableWindowFilterToolStripMenuItem.Checked;
        }
    }
}
