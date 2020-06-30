using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UIAutomationTool
{
    //Everything is now handled by PipeWrapper NuGet package, except command strings
    public static class PipeCommands
    {
        public const string ExecuteScenario = "Exec";
        public const string LoopScenario = "Loop";
        public const string StopScenario = "Stop";
    }
}
