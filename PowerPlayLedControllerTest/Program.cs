using System.Drawing;
using System.Threading;

namespace PowerPlayLed
{
    public class Program
    {
        //private static HUE2AmbientDevice controller = new HUE2AmbientDevice();

        private static ushort m_vid = 1165;
        private static ushort m_pid = 33431;
        private static byte m_reportId = 204;

        static void Main(string[] args)
        {
            PowerPlayLedControllerLoader.InitDevice(2).Wait();
            PowerPlayLedControllerLoader.Devices[2].SetColor(Color.Red);
        }
    }
}
