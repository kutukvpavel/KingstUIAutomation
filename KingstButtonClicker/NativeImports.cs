using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace UIAutomationTool
{
    public static class Native
    {
        [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
        static extern IntPtr FindWindowByCaption(IntPtr ZeroOnly, string lpWindowName);

        public static IntPtr FindWindowByCaption(string caption)
        {
            return FindWindowByCaption(IntPtr.Zero, caption);
        }

        /*public static IntPtr ProcessWindowSearch(string search)
        {
            return Process.GetProcesses().First(x => x.MainWindowTitle.Contains(search)).MainWindowHandle;
        }*/

        [DllImport("user32.dll")]
        static extern IntPtr GetDC(IntPtr hwnd);

        [DllImport("user32.dll")]
        static extern Int32 ReleaseDC(IntPtr hwnd, IntPtr hdc);

        [DllImport("gdi32.dll")]
        static extern uint GetPixel(IntPtr hdc, int nXPos, int nYPos);

        public static Color GetPixelColor(Point p)
        {
            IntPtr hdc = GetDC(IntPtr.Zero);
            uint pixel = GetPixel(hdc, p.X, p.Y);
            ReleaseDC(IntPtr.Zero, hdc);
            Color color = Color.FromArgb((int)(pixel & 0x000000FF),
                         (int)(pixel & 0x0000FF00) >> 8,
                         (int)(pixel & 0x00FF0000) >> 16);
            return color;
        }

        [StructLayout(LayoutKind.Sequential)]
        struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        [DllImport("user32.dll", SetLastError = true)]
        static extern bool GetWindowRect(IntPtr hWnd, ref RECT rect);

        public static Rectangle GetWindowRectangle(IntPtr handle)
        {
            RECT res = new RECT();
            if (!GetWindowRect(handle, ref res))
                throw new ExternalException("Can't get window rectangle, hWnd: " + handle.ToString());
            return new Rectangle(res.Left, res.Top, res.Right - res.Left, res.Bottom - res.Top);
        }

        [DllImport("User32.dll")]
        private static extern bool SetCursorPos(int X, int Y);

        public static void SetCursorPosition(Point p)
        {
            SetCursorPos(p.X, p.Y);
        }


    }
}
