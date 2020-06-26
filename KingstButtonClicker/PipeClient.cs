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
    public enum PipeCommands : byte
    {
        None,
        ExecuteScript,
        LoopScript,
        StopScript
    }

    public class PipeEventArgs : EventArgs
    {
        public PipeEventArgs(PipeCommands cmd)
        {
            Request = cmd;
        }

        public PipeCommands Request { get; }
        public byte Response { get; set; } = 0;
    }

    public sealed class PipeClient : IDisposable
    {
        public const string PipeName = "MyUIAutomationPipe";

        public static readonly PipeClient Instance = new PipeClient();

        public readonly object LockObject = new object();

        public event EventHandler<PipeEventArgs> CommandReceived;

        private PipeClient()
        {
            pipeStream = new NamedPipeClientStream(PipeName);
        }

        private NamedPipeClientStream pipeStream;
        private List<byte> responses = new List<byte>();

        private void DispatchPipeOnce()
        {
            //Return responses
            List<byte> temp = new List<byte>();
            lock (LockObject)
            {
                temp.AddRange(responses);
            }
            for (int i = 0; i < temp.Count; i++)
            {
                pipeStream.WriteByte(temp[i]);
            }
            //Receive new command
            try
            {
                if (pipeStream.CanRead)
                {
                    CommandReceived?.Invoke(this, new PipeEventArgs((PipeCommands)(byte)pipeStream.ReadByte()));
                }
            }
            catch (InvalidCastException e)
            {
                ErrorListener.AddFormat(e, "Bad pipe command!");
            }
        }

        public void SubmitResponse(int val)
        {
            lock (LockObject)
            {
                responses.Add((byte)val);
            }
        }

        public void DispatchPipe(CancellationTokenSource token)
        {
            while (!token.IsCancellationRequested)
            {
                DispatchPipeOnce();
            }
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        private void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    pipeStream.Close();
                    pipeStream.Dispose();
                }
                disposedValue = true;
            }
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
        }
        #endregion
    }
}
