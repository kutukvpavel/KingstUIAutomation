using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using WindowsInput;   
using System.Threading;
using System.Runtime.Serialization;           

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

        public static void MouseClick(Point p)
        {
            simulatorInstance.Mouse.MoveMouseToPositionOnVirtualDesktop(p.X, p.Y).Sleep(1).LeftButtonClick();
        }

        public static void PressKey(WindowsInput.Native.VirtualKeyCode code)
        {
            simulatorInstance.Keyboard.KeyDown(code);
        }

        public static void WaitForPixel(Point p, Color c)
        {
            while (Native.GetPixelColor(p) != c)
            {
                Thread.Sleep(100);
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
            try
            {
                for (int i = 0; i < Actions.Length; i++)
                {
                    switch (Actions[i].Type)
                    {
                        case ActionTypes.MouseClick:
                            break;
                        case ActionTypes.PressKey:
                            break;
                        case ActionTypes.WaitForPixel:
                            break;
                        case ActionTypes.Sleep:
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorListener.Add(ex);
                return 2;
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
        public ActionTypes Type { get; private set; }
        [DataMember]
        public object[] Arguments { get; private set; }
    }
}
