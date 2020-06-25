using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using WindowsInput;
using System.Threading;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace KingstButtonClicker
{
    public enum ActionTypes
    {
        MouseClick,
        PressKey,
        WaitForPixel,
        Sleep
    }

    [DataContract]
    public class SimulatorScenario
    {
        private static InputSimulator simulatorInstance = new InputSimulator();

        #region Static Methods

        public static void MouseClick(ClickPoint p, Rectangle w)
        {
            //Point d = p.GetDesktopPoint(w);
            //simulatorInstance.Mouse.MoveMouseToPositionOnVirtualDesktop(d.X, d.Y).Sleep(1).LeftButtonClick();
            //InputSimulator somehow has effective resolution of 800x600, API import provides real resolution
            Native.SetCursorPosition(p.GetPoint(PointReference.TopLeft, w));
            simulatorInstance.Mouse.LeftButtonClick();
        }

        public static void PressKey(WindowsInput.Native.VirtualKeyCode code)
        {
            simulatorInstance.Keyboard.KeyDown(code);
        }

        public static void WaitForPixel(ClickPoint p, Rectangle w, Color c, int lim = 0)
        {
            Point d = p.GetPoint(PointReference.TopLeft, w);
            int cnt = 0;
            while (Native.GetPixelColor(d) != c)
            {
                Sleep(100);
                cnt += 100;
                if (lim > 0)
                {
                    if (lim <= cnt) break;
                }
            }
        }

        public static void Sleep(int ms)
        {
            Thread.Sleep(ms);
        }

        #endregion

        public SimulatorScenario(params SimulatorAction[] a)
        {
            Actions = a;
        }

        [DataMember]
        public SimulatorAction[] Actions { get; private set; }

        public int Execute()
        {
            IntPtr hWnd = IntPtr.Zero;
            try
            {
                hWnd = Native.FindWindowByCaption(Program.WindowSearchString);
            }
            catch (Exception ex)
            {
                ErrorListener.Add(ex);
            }
            if (hWnd == IntPtr.Zero) return 1;
            Rectangle window = new Rectangle();
            try
            {
                window = Native.GetWindowRectangle(hWnd);
            }
            catch (Exception ex)
            {
                ErrorListener.Add(ex);
            }
            if (window.IsEmpty) return 2;
            try
            {
                var dic = Program.Database.ToDictionary(x => x.Name);
                for (int i = 0; i < Actions.Length; i++)
                {
                    switch (Actions[i].Type)
                    {
                        case ActionTypes.MouseClick:
                            MouseClick(dic[(string)Actions[i].Arguments[0]], window);
                            break;
                        case ActionTypes.PressKey:
                            PressKey((WindowsInput.Native.VirtualKeyCode)Actions[i].Arguments[0]);
                            break;
                        case ActionTypes.WaitForPixel:
                            WaitForPixel(dic[(string)Actions[i].Arguments[0]], window, (Color)Actions[i].Arguments[1],
                                Actions[i].Arguments.Length > 2 ? (int)Actions[i].Arguments[2] : 0);
                            break;
                        case ActionTypes.Sleep:
                            Sleep((int)Actions[i].Arguments[0]);
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (InvalidCastException ex)
            {
                ErrorListener.AddFormat(ex, "Wrong arguments specified for an action!");
                return 3;
            }
            catch (Exception ex)
            {
                ErrorListener.Add(ex);
                return 4;
            }
            return 0;
        }
    }

    [DataContract]
    public class SimulatorAction
    {
        public SimulatorAction(ActionTypes t, params object[] a)
        {
            Type = t;
            Arguments = a;
        }

        [DataMember]
        [Bindable(true)]
        public ActionTypes Type { get; private set; }
        /// <summary>
        /// For mouse click - the name of the point (in the database)
        /// For key press - WindowsInput.Native.VirtualKeyCode
        /// For sleep - int (ms)
        /// For pixel color based waiting - point name and color (+ optional time limit)
        /// </summary>
        [DataMember]  
        public object[] Arguments { get; private set; }
    }
}
