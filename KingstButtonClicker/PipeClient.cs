using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KingstButtonClicker
{
    public enum PipeCommands : int
    {
        None,
        ExecuteScript,
        LoopScript,
        StopScript
    }
}
