using Device.Net;
using Hid.Net.Windows;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace PowerPlayLed
{
    public static class PowerPlayLedControllerLoader
    {
        private static IDevice[] _devices;

        private static PowerPlayLedController[] _PowerPlayLedController;
        public static PowerPlayLedController[] Devices { get => _PowerPlayLedController; }

        public static async Task InitDevice(int deviceIndex)
        {
            WindowsHidDeviceFactory.Register(null, null);
            List<FilterDeviceDefinition> deviceDefinitions = new List<FilterDeviceDefinition>();
            FilterDeviceDefinition d = new FilterDeviceDefinition { DeviceType = DeviceType.Hid, VendorId = 0x046D, ProductId = 0xC53A, Label = "Logitech G Powerplay Mousepad with Lightspeed" };
            deviceDefinitions.Add(d);
            List<IDevice> devices = await DeviceManager.Current.GetDevicesAsync(deviceDefinitions);
            _devices = devices.ToArray();
            _PowerPlayLedController = new PowerPlayLedController[_devices.Length];
            /*
            for (int i = 0; i < _devices.Length; i++)
            {
                _PowerPlayLedController[i] = new PowerPlayLedController(_devices[i]);
            }
            */
            _PowerPlayLedController[deviceIndex] = new PowerPlayLedController(_devices[deviceIndex]);
        }
    }
}
