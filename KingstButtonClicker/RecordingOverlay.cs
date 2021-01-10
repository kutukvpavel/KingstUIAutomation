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
            captionLabel = new Label()
            {
                Text = string.Format(windowCaption, Enum.GetName(typeof(PointReference), currentReference)),
                Margin = new Padding(3),
                Left = 100,
                AutoSize = true,
                Font = new Font(FontFamily.GenericMonospace, 14, FontStyle.Bold),
                ForeColor = ((SolidBrush)ReferenceToBrush(currentReference)).Color
            };
            formForControls.Controls.Add(captionLabel);
        }

        public ClickPoint[] Points
        {
            get
            {
                return pointList.ToArray();
            }
            set
            {
                pointList = value.ToList();
                drawList.Clear();
                foreach (var item in pointList)
                {
                    DrawPoint(item.Name, item.RawPoint, item.RawReference);
                }
            }
        }

        private List<DrawnPoint> drawList;
        private List<ClickPoint> pointList;
        private Form formForControls;
        private Label captionLabel;
        private Graphics drawingContext;
        private PointReference currentReference = PointReference.TopLeft;
        private static readonly string windowCaption = "{0}";

        private void DrawPoint(string name, Point e, PointReference r)
        {
            var b = ReferenceToBrush(r);
            //Set up a label
            var lbl = new Label()
            {
                Text = name,
                BackColor = Color.DarkMagenta,
                Location = new Point(e.X - 10, e.Y + 10),
                AutoSize = true,
                ForeColor = ((SolidBrush)b).Color
            };
            formForControls.Controls.Add(lbl);
            //Create redraw/undo data
            drawList.Add(new DrawnPoint(
                new Rectangle(e.X - 10, e.Y - 10, 20, 20),
                new Rectangle(e.X - 5, e.Y - 5, 10, 10),
                b,
                new SolidBrush(Native.GetPixelColor(PointToScreen(e))),
                lbl));
            formForControls.Invalidate();
        }

        private Brush ReferenceToBrush(PointReference r)
        {
            switch (r)
            {
                case PointReference.TopLeft:
                    return Brushes.Orange;
                case PointReference.TopRight:
                    return Brushes.Cyan;
                case PointReference.BottomLeft:
                    return Brushes.LightGreen;
                case PointReference.BottomRight:
                    return Brushes.Yellow;
                default:
                    return Brushes.Black;
            }
        }

        #region FormForControls events

        private void FormForControls_Shown(object sender, EventArgs e)
        {
            drawingContext = formForControls.CreateGraphics();
            Focus();
            //formForControls.Focus();
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
            using (var dialog = new InputBox() { Text = "Enter name for the point:" })
            {
                if (dialog.ShowDialog() == DialogResult.Cancel) return;
                //Record the event
                var alreadyPresent = pointList.FirstOrDefault(x => x.Name.Equals(dialog.Value));
                if (alreadyPresent != null)
                {
                    if (MessageBox.Show(
                        "This point already exists. Overwrite?", 
                        "Recording Overlay", 
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question) != DialogResult.Yes) return;
                    pointList.Remove(alreadyPresent);
                    drawList.Remove(drawList.First(x => x.NameLabel.Text.Equals(dialog.Value)));
                }
                pointList.Add(new ClickPoint(PointToScreen(e.Location), currentReference, dialog.Value));
                DrawPoint(dialog.Value, e.Location, currentReference);
            }
        }

        private void RecordingOverlay_Load(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
            AddOwnedForm(formForControls);
            formForControls.Show();
        }

        private void RecordingOverlay_Shown(object sender, EventArgs e)
        {
            //formForControls.Focus();
        }

        private void RecordingOverlay_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.Control)
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
            else
            {
                if (e.Shift && (e.KeyCode == Keys.ShiftKey))
                {
                    currentReference++;
                    if ((int)currentReference > Enum.GetValues(typeof(PointReference)).Length - 1)
                        currentReference = PointReference.TopLeft;
                    captionLabel.Text = string.Format(windowCaption, Enum.GetName(typeof(PointReference), currentReference));
                    captionLabel.ForeColor = ((SolidBrush)ReferenceToBrush(currentReference)).Color;
                }
            }
        }

        #endregion
    }
}
