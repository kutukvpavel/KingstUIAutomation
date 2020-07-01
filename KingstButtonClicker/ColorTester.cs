using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIAutomationTool
{
    public class ColorTester
    {
        public ColorTester(params ClickPoint[] p)
        {
            Points = p;
        }

        public ClickPoint[] Points { get; }

        public string GetReport()
        {
            IntPtr hWnd = IntPtr.Zero;
            try
            {
                hWnd = Native.FindWindowByCaption(Program.WindowTitleString);
            }
            catch (Exception ex)
            {
                ErrorListener.Add(ex);
            }
            if (hWnd == IntPtr.Zero) return "Window not found!";
            Rectangle window = new Rectangle();
            try
            {
                window = Native.GetWindowRectangle(hWnd);
            }
            catch (Exception ex)
            {
                ErrorListener.Add(ex);
            }
            if (window.IsEmpty) return "The client rectangle is empty!";
            StringBuilder res = new StringBuilder();
            for (int i = 0; i < Points.Length; i++)
            {
                try
                {
                    Color c = Native.GetPixelColor(Points[i].GetPoint(PointReference.TopLeft, window));
                    res.AppendFormat("{0} = {1} ({2})", Points[i].Name, c.ToString(), (uint)c.ToArgb());
                }
                catch (Exception ex)
                {
                    ErrorListener.Add(ex);
                    res.AppendFormat("{0} = Error: {1}", Points[i].Name, ex.Message);
                }
                res.AppendLine();
            }
            return res.ToString();
        }
    }
}
