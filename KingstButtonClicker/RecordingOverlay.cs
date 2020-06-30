using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UIAutomationTool
{
    public partial class RecordingOverlay : Form
    {
        private class DrawnPoint
        {
            public DrawnPoint(Rectangle or, Rectangle ir, Brush ob, Brush ib, Label n)
            {
                OuterRectangle = or;
                InnerRectangle = ir;
                OuterBrush = ob;
                InnerBrush = ib;
                NameLabel = n; 
            }

            public Rectangle OuterRectangle { get; set; }
            public Rectangle InnerRectangle { get; set; }
            public Brush OuterBrush { get; set; }
            public Brush InnerBrush { get; set; }
            public Label NameLabel { get; set; }

            public void Draw(Graphics context)
            {
                context.FillEllipse(OuterBrush, OuterRectangle);
                context.FillEllipse(InnerBrush, InnerRectangle);
            }
        }

        public RecordingOverlay()
        {
            InitializeComponent();
            pointList = new List<ClickPoint>();
            drawList = new List<DrawnPoint>();
            formForControls = new Form()
            {
                TransparencyKey = Color.DarkMagenta,
                BackColor = Color.DarkMagenta,
                WindowState = FormWindowState.Maximized,
                FormBorderStyle = FormBorderStyle.None,
            };
            formForControls.Shown += FormForControls_Shown;
            formForControls.Paint += FormForControls_Paint;
        }

        public ClickPoint[] Points
        {
            get
            {
                return pointList.ToArray();
            }
        }

        private List<DrawnPoint> drawList;
        private List<ClickPoint> pointList;
        private Form formForControls;
        private Graphics drawingContext;
        private PointReference currentReference = PointReference.TopLeft;
        private static readonly string windowCaption = "Recording Overlay - Current coordinate system: {0}";

        #region FormForControls events

        private void FormForControls_Shown(object sender, EventArgs e)
        {
            drawingContext = formForControls.CreateGraphics();
            formForControls.Focus();
        }

        private void FormForControls_Paint(object sender, PaintEventArgs e)
        {
            foreach (var item in drawList)
            {
                item.Draw(drawingContext);
            }
        }

        #endregion

        #region This form's events

        private void RecordingOverlay_MouseDown(object sender, MouseEventArgs e)
        {
            //Ask for the name if the point
            var dialog = new InputBox() { Text = "Enter name for the point:" };
            if (dialog.ShowDialog() == DialogResult.Cancel) return;
            //Record the event
            pointList.Add(new ClickPoint(PointToScreen(e.Location), PointReference.TopLeft, dialog.Value));
            //Set up a label
            var lbl = new Label()
            {
                Text = dialog.Value,
                BackColor = Color.DarkMagenta,
                Location = new Point(e.X - 10, e.Y + 10),
                AutoSize = true
            };
            formForControls.Controls.Add(lbl);
            //Create redraw/undo data
            drawList.Add(new DrawnPoint(
                new Rectangle(e.X - 10, e.Y - 10, 20, 20),
                new Rectangle(e.X - 5, e.Y - 5, 10, 10),
                Brushes.Red,
                new SolidBrush(Native.GetPixelColor(PointToScreen(e.Location))),
                lbl));
            formForControls.Invalidate();
            // ShowDialog doesn't dispose the InputBox
            dialog.Dispose();
        }

        private void RecordingOverlay_Load(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
            AddOwnedForm(formForControls);
            formForControls.Show();
        }

        private void RecordingOverlay_Shown(object sender, EventArgs e)
        {
            formForControls.Focus();
        }

        private void RecordingOverlay_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.Control)
            {
                if (e.KeyCode == Keys.None)
                {
                    currentReference++;
                    if ((int)currentReference > Enum.GetValues(typeof(PointReference)).Length - 1)
                        currentReference = PointReference.TopLeft;
                    Text = string.Format(windowCaption, Enum.GetName(typeof(PointReference), currentReference));
                }
                else
                {
                    if (e.KeyCode == Keys.Z)
                    {
                        var toRemove = drawList.Last();
                        formForControls.Controls.Remove(toRemove.NameLabel);
                        formForControls.Invalidate(toRemove.OuterRectangle);
                        drawList.Remove(toRemove);
                        pointList.RemoveAt(pointList.Count - 1);
                    }
                } 
            }
        }

        #endregion
    }
}
