using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WindowsInput;

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

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
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
            Program.Scenario.Execute();
            Show();
        }

        private void moveMouseToToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var inputBox = new InputBox();
            if (inputBox.ShowDialog() == DialogResult.OK)
            {
                int[] parsed = inputBox.Value.Split('x').Select(x => int.Parse(x)).ToArray();
                /*var instance = new InputSimulator();
                instance.Mouse.MoveMouseToPositionOnVirtualDesktop(0, 0).MoveMouseBy(parsed[0], parsed[1]);*/
                Native.SetCursorPosition(new Point(parsed[0], parsed[1]));
            }
        }

        private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
