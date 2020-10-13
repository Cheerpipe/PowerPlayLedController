using PowerPlayLed;
using System;
using System.Drawing;
using System.IO;
using System.IO.Pipes;
using System.Windows.Forms;

namespace PowerPlayLedListener
{
    public class Listener
    {
        public bool StopListening = false;
        //private Thread _setterThread;
        //private readonly ManualResetEvent _setterThreadEvent = new ManualResetEvent(false);
        private bool _shutingDown = false;
        private PowerPlayLedController _deviceController;
        private byte R = 0;
        private byte G = 0;
        private byte B = 0;

        public Listener(PowerPlayLedController hue2AmbientDeviceController)
        {
            _deviceController = hue2AmbientDeviceController;
            StartArgsPipeServer("PowerPlayLedController");
        }

        //TODO: Use MMF instead of pipes
        public void StartArgsPipeServer(string pipeName)
        {
            var s = new NamedPipeServerStream(pipeName, PipeDirection.In);
            Action<NamedPipeServerStream> a = GetArgsCallBack;
            a.BeginInvoke(s, callback: ar => { }, @object: null);
        }

        private void GetArgsCallBack(NamedPipeServerStream pipe)
        {
            while (!StopListening)
            {
                pipe.WaitForConnection();
                var sr = new BinaryReader(pipe);
                var args = sr.ReadBytes(512);
                pipe.Disconnect();
                Setter(args);
            }
        }

        public void Setter(byte[] args)
        {
            if (_shutingDown)
                return;

            R = args[0];
            G = args[1];
            B = args[2];
            _deviceController.SetColor(Color.FromArgb(R, G, B));
        }
    }
}
