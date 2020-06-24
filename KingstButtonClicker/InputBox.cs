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
    public partial class InputBox : Form
    {
        public InputBox()
        {
            InitializeComponent();
        }

        public new string Text { get; set; } = "";
        public string Value { get; set; } = "";

        private void InputBox_Load(object sender, EventArgs e)
        {
            label1.Text = Text;
            txtInput.Text = Value;
        }

        private void InputBox_FormClosing(object sender, FormClosingEventArgs e)
        {
            Value = txtInput.Text;
        }
    }
}
