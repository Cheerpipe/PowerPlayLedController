using Device.Net;
using Hid.Net.Windows;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

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
        }

        public bool SetColor(Color color)
        {
            usb_buf[0x00] = 0x11;
            usb_buf[0x01] = 0x07;
            usb_buf[0x02] = 0x0B;
            usb_buf[0x03] = 0x3E;

            usb_buf[0x04] = 0x00;//Zone
            usb_buf[0x05] = 0x01;//Mode

            usb_buf[0x06] = color.R;
            usb_buf[0x07] = color.G;
            usb_buf[0x08] = color.B;
            usb_buf[0x09] = 0x02;
            _device.InitializeAsync().Wait();
            _device.WriteAndReadAsync(usb_buf).Wait();
            return true;
        }
        public void Dispose()
        {
            _device.Dispose();
        }
    }
}
