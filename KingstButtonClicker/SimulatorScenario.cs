using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading;
using WindowsInput;

namespace UIAutomationTool
{
    public enum ActionTypes
    {
        MouseClick,
        PressKey,
        WaitForPixel,
        Sleep
    }

    public enum ScenarioExitCodes
    {
        OK = 0,
        WindowNotFound,
        EmptyWindow,
        WrongArguments,
        Timeout,
        UnexpectedError
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

        public static bool WaitForPixel(ClickPoint p, Rectangle w, Color c, int lim = 0)
        {
            Point d = p.GetPoint(PointReference.TopLeft, w);
            int cnt = 0;
            while (Native.GetPixelColor(d) != c)
            {
                Sleep(100);
                cnt += 100;
                if (lim > 0)
                {
                    if (lim <= cnt) return false;
                }
            }
            return true;
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
        [DataMember(EmitDefaultValue = true)]
        public WindowsInput.Native.VirtualKeyCode LoopBreakKey { get; private set; } = WindowsInput.Native.VirtualKeyCode.RCONTROL;
        [DataMember(EmitDefaultValue = true)]
        public bool FailOnTimeout = true;

        public int Loop()
        {
            return Loop(null);
        }
        public int Loop(CancellationTokenSource cancel)
        {
            while (!BreakOutPressed() && (cancel == null ? true : !cancel.IsCancellationRequested))
            {
                int r = Execute();
                if (r != 0) return r;
            }
            return (int)ScenarioExitCodes.OK;
        }
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
            if (hWnd == IntPtr.Zero) return (int)ScenarioExitCodes.WindowNotFound;
            Rectangle window = new Rectangle();
            try
            {
                window = Native.GetWindowRectangle(hWnd);
            }
            catch (Exception ex)
            {
                ErrorListener.Add(ex);
            }
            if (window.IsEmpty) return (int)ScenarioExitCodes.EmptyWindow;
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
                            {
                                bool r = WaitForPixel(dic[(string)Actions[i].Arguments[0]], window, (Color)Actions[i].Arguments[1],
                                    Actions[i].Arguments.Length > 2 ? (int)Actions[i].Arguments[2] : 0);
                                if (FailOnTimeout && !r) throw new TimeoutException();
                                break;
                            }
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
                return (int)ScenarioExitCodes.WrongArguments;
            }
            catch (TimeoutException)
            {
                ErrorListener.Add(new Exception("Timed out during waiting for pixel color (FailOnTimeout is set to True)."));
                return (int)ScenarioExitCodes.Timeout;
            }
            catch (Exception ex)
            {
                ErrorListener.Add(ex);
                return (int)ScenarioExitCodes.UnexpectedError;
            }
            return (int)ScenarioExitCodes.OK;
        }

        private bool BreakOutPressed()
        {
            return simulatorInstance.InputDeviceState.IsHardwareKeyDown(LoopBreakKey);
        }
    }

    [DataContract]
    public class SimulatorAction
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="t"></param>
        /// <param name="a">
        /// For mouse click - the name of the point (in the database)
        /// For key press - WindowsInput.Native.VirtualKeyCode
        /// For sleep - int (ms)
        /// For pixel color based waiting - point name and color (+ optional time limit)
        /// </param>
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
