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

    /// <summary>
    /// Exit codes for scenario execution
    /// </summary>
    public enum ScenarioExitCodes
    {
        OK = 0,
        WindowNotFound,
        EmptyWindow,
        WrongArguments,
        Timeout,
        UnexpectedError,
        Aborted
    }

    [DataContract]
    public class SimulatorScenario
    {
        private static InputSimulator simulatorInstance = new InputSimulator();

        #region Static Methods

        /// <summary>
        /// Move to specified point and click left mouse button
        /// </summary>
        /// <param name="p">Database point</param>
        /// <param name="w">Target window rectangle</param>
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

        public static bool CheckKeyPressed(WindowsInput.Native.VirtualKeyCode code)
        {
            return simulatorInstance.InputDeviceState.IsHardwareKeyDown(code);
        }

        /// <summary>
        /// Wait until the pixel changes its color to the specified one
        /// </summary>
        /// <param name="p">Database point</param>
        /// <param name="w">Target window rectangle</param>
        /// <param name="c">Target color</param>
        /// <param name="t">Cancellation token</param>
        /// <param name="k">Virtual key code for user abort</param>
        /// <param name="lim">Optional time limit (mS), 0 = no limit</param>
        /// <returns>False on timeout, true otherwise</returns>
        public static bool WaitForPixel(ClickPoint p, Rectangle w, Color c, CancellationTokenSource t,
            WindowsInput.Native.VirtualKeyCode k, int lim = 0)
        {
            Point d = p.GetPoint(PointReference.TopLeft, w);
            int cnt = 0;
            while (Native.GetPixelColor(d) != c) //This API has a high performance impact
            {
                Sleep(200);
                cnt += 200;
                if (lim > 0)
                {
                    if (lim <= cnt) return false;
                }
                if (t != null)
                {
                    if (t.IsCancellationRequested) break;
                }
                if (CheckKeyPressed(k)) break;
            }
            return true;
        }
        /// <summary>
        /// Wait until the pixel changes its color to the specified one.
        /// Use default user abort key (right Control)
        /// </summary>
        /// <param name="p">Database point</param>
        /// <param name="w">Target window rectangle</param>
        /// <param name="c">Target color</param>
        /// <param name="t">Cancellation token</param>
        /// <param name="lim">Optional time limit (mS), 0 = no limit</param>
        /// <returns>False on timeout, true otherwise</param>
        /// <returns></returns>
        public static bool WaitForPixel(ClickPoint p, Rectangle w, Color c, CancellationTokenSource t, int lim = 0)
        {
            return WaitForPixel(p, w, c, t, WindowsInput.Native.VirtualKeyCode.RCONTROL, lim);
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
        /// <summary>
        /// Defaults to right Control
        /// </summary>
        [DataMember(EmitDefaultValue = true)]
        public WindowsInput.Native.VirtualKeyCode LoopBreakKey { get; private set; } = WindowsInput.Native.VirtualKeyCode.RCONTROL;
        /// <summary>
        /// Defaults to True
        /// </summary>
        [DataMember(EmitDefaultValue = true)]
        public bool FailOnTimeout { get; private set; } = true;

        public static event EventHandler<ScenarioEventArgs> ScenarioExecuted;

        public int Loop(CancellationTokenSource cancel = null)
        {
            while (!CheckKeyPressed(LoopBreakKey))
            {
                int r = Execute(cancel);
                if (r != 0) return r;
            }
            return (int)ScenarioExitCodes.OK;
        }
        public int Execute(CancellationTokenSource cancel = null)
        {
            int res = ExecutionEngine(cancel);
            ScenarioExecuted?.Invoke(this, new ScenarioEventArgs(res));
            return res;
        }
        private int ExecutionEngine(CancellationTokenSource cancel = null)
        {
            //First, look for the window needed
            IntPtr hWnd = IntPtr.Zero;
            try
            {
                hWnd = Native.FindWindowByCaption(Program.WindowTitleString);
            }
            catch (Exception ex)
            {
                ErrorListener.Add(ex);
            }
            if (hWnd == IntPtr.Zero) return (int)ScenarioExitCodes.WindowNotFound;
            //TODO: Next, try to bring it to front and make active

            //Next, see if it's a 0x0 rectangle (still minimized or smth)
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
            //Finally, execute the scenario
            try
            {
                var dic = Program.Database.ToDictionary(x => x.Name);
                for (int i = 0; i < Actions.Length; i++)
                {
                    if (cancel != null)
                    {
                        if (cancel.IsCancellationRequested) return (int)ScenarioExitCodes.Aborted;
                    }
                    if (CheckKeyPressed(LoopBreakKey)) return (int)ScenarioExitCodes.Aborted;
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
                                    cancel, (Actions[i].Arguments.Length > 2) ? (int)Actions[i].Arguments[2] : 0);
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
        /// For pixel color based waiting - point name, color, (optional time limit)
        /// </summary>
        [DataMember]  
        public object[] Arguments { get; private set; }
    }

    /// <summary>
    /// Contains scenario exit code (int)
    /// </summary>
    public class ScenarioEventArgs : EventArgs
    {
        public ScenarioEventArgs(int code)
        {
            ExitCode = code;
        }

        public int ExitCode { get; }
    }
}
