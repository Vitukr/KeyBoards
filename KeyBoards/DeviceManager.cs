using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyBoards
{
    public class DeviceManager
    {        
        private static readonly DeviceManager instance = new DeviceManager();
        
        private DeviceManager()
        {
        }

        public static DeviceManager GetInstance()
        {
            return instance;
        }

        private List<Device> devices = new List<Device>();
        public void Add(Device device)
        {
            devices.Add(device);
        }

        public void Delete(string serialNumber)
        {
            var device = devices.Where(r => r.SerialNumber == serialNumber).FirstOrDefault();
            devices.Remove(device);
        }
        public void Clear()
        {
            devices.Clear();
        }
        public string GetInfo(string serialNumber)
        {
            var device = devices.Where(r => r.SerialNumber == serialNumber).FirstOrDefault();
            return device.GetInfo;
        }
    }
}
