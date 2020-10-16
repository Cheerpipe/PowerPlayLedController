using Device.Net;
using Hid.Net.Windows;
using System;
using System.Drawing;

namespace PowerPlayLed
{
    public class PowerPlayLedController : IDisposable
    {
        private string _hidDeviceId = string.Empty;
        byte[] usb_buf = new byte[20];
        private Color _currentColor;
        private WindowsHidDevice _device;

        public PowerPlayLedController(IDevice device)
        {
            Initialize(device);
        }
        public void Initialize(IDevice device)
        {
            _device = (WindowsHidDevice)device;
            _device.InitializeAsync().Wait();
            usb_buf[0x00] = 0x11;
            usb_buf[0x01] = 0x07;
            usb_buf[0x02] = 0x0B;
            usb_buf[0x03] = 0x3E;
            usb_buf[0x04] = 0x00;//Zone
            usb_buf[0x05] = 0x01;//Mode
            usb_buf[0x09] = 0x02;
        }

        bool _changing = false;
        public void SetColor(Color color)
        {
            if (!_changing)
            {
                _changing = true;
                usb_buf[0x06] = color.R;
                usb_buf[0x07] = color.G;
                usb_buf[0x08] = color.B;
                _ = _device.WriteAsync(usb_buf).Wait(16);
                _changing = false;
            }
        }
        public void Dispose()
        {
            _device.Dispose();
        }
    }
}
