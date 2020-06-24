using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KingstButtonClicker
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void testColorsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void recordPointsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new RecordingOverlay();
            Hide();
            form.ShowDialog();
            Show();
            Program.Database.AddRange(form.Points);
            txtOutput.AppendText("Database contents:");
            txtOutput.AppendText(string.Join(Environment.NewLine, Program.Database.Select(x => string.Format("{0} = {1} ({2})", 
                x.Name, x.Coordinates.RawPoint.ToString(), Enum.GetName(typeof(PointReference), x.Coordinates.RawReference)))));
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
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
    }
}
