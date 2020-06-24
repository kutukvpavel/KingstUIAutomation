using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using WindowsInput;
using System.Diagnostics;
using System.Threading;
using System.Runtime.InteropServices;

namespace KingstButtonClicker
{
    public enum ActionTypes
    {
        MouseClick,
        PressKey,
        WaitForPixel,
        Sleep
    }

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
            while (NativeImports.GetPixelColor(p) != c)
            {
                Thread.Sleep(100);
            }
        }

        public static void Sleep(int ms)
        {
            Thread.Sleep(ms);
        }

        public static IntPtr FindWindow(string search)
        {
            return Process.GetProcesses().First(x => x.MainWindowTitle.Contains(search)).MainWindowHandle;
        }

        #endregion

        public SimulatorScenario(params SimulatorAction[] a)
        {
            Actions = a;
        }

        public SimulatorAction[] Actions { get; }

        public void Execute()
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
    }

    public class SimulatorAction
    {
        public SimulatorAction(ActionTypes t, params object[] a)
        {
            Type = t;
            Arguments = a;
        }

        public ActionTypes Type { get; }
        public object[] Arguments { get; }
    }
}
