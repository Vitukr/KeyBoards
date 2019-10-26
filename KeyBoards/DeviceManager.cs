using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace KeyBoards
{
    public class DeviceManager
    {
        #region Events
        public event EventHandler<DeviceAddedEventArgs> DeviceAdded;
        public event EventHandler<DeviceDeletedEventArgs> DeviceDeleted;

        protected virtual void OnDeviceAdded(DeviceAddedEventArgs e)
        {
            DeviceAdded?.Invoke(this, e);
        }

        protected virtual void OnDeviceDeleted(DeviceDeletedEventArgs e)
        {
            DeviceDeleted?.Invoke(this, e);
        }
        #endregion

        private static readonly DeviceManager instance = new DeviceManager();

        static DeviceManager()
        {
            instance.DeviceAdded += Instance_DeviceAdded;
            instance.DeviceDeleted += Instance_DeviceDeleted;
        }

        private static void Instance_DeviceAdded(object sender, DeviceAddedEventArgs e)
        {
            var mainWindow = (MainWindow)Application.Current.MainWindow;            
            mainWindow.ListBoxMessages.Items.Add("Device added: Serial number - " + e.SerialNumber);
            if(mainWindow.ListBoxMessages.Items.Count > 5)
            {
                mainWindow.ListBoxMessages.Items.RemoveAt(0);
            }
        }

        private static void Instance_DeviceDeleted(object sender, DeviceDeletedEventArgs e)
        {
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.ListBoxMessages.Items.Add("Device deleted: Serial number - " + e.SerialNumber);
            if (mainWindow.ListBoxMessages.Items.Count > 5)
            {
                mainWindow.ListBoxMessages.Items.RemoveAt(0);
            }
        }        

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
            DeviceAddedEventArgs e = new DeviceAddedEventArgs() {SerialNumber = device.SerialNumber };
            OnDeviceAdded(e);
        }

        public void Delete(string serialNumber)
        {
            var device = devices.Where(r => r.SerialNumber == serialNumber).FirstOrDefault();
            devices.Remove(device);
            DeviceDeletedEventArgs e = new DeviceDeletedEventArgs() { SerialNumber = device.SerialNumber };
            OnDeviceDeleted(e);
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

    public class DeviceAddedEventArgs : EventArgs
    {
        public string SerialNumber { get; set; }
    }

    public class DeviceDeletedEventArgs : EventArgs
    {
        public string SerialNumber { get; set; }
    }
}
