using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace KeyBoards
{
    public class DeviceManager
    {
        #region Events
        public event EventHandler<DeviceAddedEventArgs> DeviceAdded;
        public event EventHandler<DeviceDeletedEventArgs> DeviceDeleted;
        public event EventHandler<DeviceGetInfoEventArgs> DeviceGetInfo;

        protected virtual void OnDeviceAdded(DeviceAddedEventArgs e)
        {
            DeviceAdded?.Invoke(this, e);
        }

        protected virtual void OnDeviceDeleted(DeviceDeletedEventArgs e)
        {
            DeviceDeleted?.Invoke(this, e);
        }

        protected virtual void OnDeviceGetInfo(DeviceGetInfoEventArgs e)
        {
            DeviceGetInfo?.Invoke(this, e);
        }
        #endregion

        private static readonly DeviceManager instance = new DeviceManager();

        static DeviceManager()
        {
            instance.DeviceAdded += Instance_DeviceAdded;
            instance.DeviceDeleted += Instance_DeviceDeleted;
            instance.DeviceGetInfo += Instance_DeviceGetInfo;
        }

        private static void Instance_DeviceAdded(object sender, DeviceAddedEventArgs e)
        {
            var t = Thread.CurrentThread.ManagedThreadId;
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                var mainWindow = (MainWindow)Application.Current.MainWindow;
                mainWindow.ListBoxTest.Items.Add("Device added: Serial number - " + e.SerialNumber + " : thread - " + t);
                mainWindow.Devices.Add(e.Device);
            }));
        }

        private static void Instance_DeviceDeleted(object sender, DeviceDeletedEventArgs e)
        {
            var t = Thread.CurrentThread.ManagedThreadId;
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                var mainWindow = (MainWindow)Application.Current.MainWindow;
                mainWindow.ListBoxTest.Items.Add("Device deleted: Serial number - " + e.SerialNumber + " : thread - " + t);
                mainWindow.Devices.Remove(e.Device);
            }));
        }

        private static void Instance_DeviceGetInfo(object sender, DeviceGetInfoEventArgs e)
        {
            var t = Thread.CurrentThread.ManagedThreadId;
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                var mainWindow = (MainWindow)Application.Current.MainWindow;
                mainWindow.ListBoxTest.Items.Add("Device info: " + e.Info + " : thread - " + t);
            }));
        }

        private static Object lockObj = new Object();

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
            lock (lockObj)
            {
                devices.Add(device);
                DeviceAddedEventArgs e = new DeviceAddedEventArgs() { SerialNumber = device?.SerialNumber, Device = device };
                OnDeviceAdded(e);
            }
        }

        public void Delete(string serialNumber)
        {
            lock (lockObj)
            {
                var device = devices.Where(r => r?.SerialNumber == serialNumber).FirstOrDefault();
                DeviceDeletedEventArgs e = new DeviceDeletedEventArgs() { SerialNumber = device?.SerialNumber, Device = device };
                OnDeviceDeleted(e);
                devices.Remove(device);
            }
        }
        public void Clear()
        {
            lock (lockObj)
            {
                devices.Clear();
            }
        }
        public string GetInfo(string serialNumber)
        {
            lock (lockObj)
            {
                var device = devices.Where(r => r?.SerialNumber == serialNumber).FirstOrDefault();
                DeviceGetInfoEventArgs e = new DeviceGetInfoEventArgs() { Info = device?.GetInfo, Device = device };
                OnDeviceGetInfo(e);
                return device?.GetInfo;
            }
        }

        public Device GetDevice(int index)
        {
            lock (lockObj)
            {
                if (devices.Count > index)
                {
                    return devices.ElementAt(index);
                }
                else
                {
                    return null;
                }
            }
        }

        public int GetLenth()
        {
            lock (lockObj)
            {
                return devices.Count > 0 ? devices.Count - 1 : 0;
            }
        }
    }

    public class DeviceAddedEventArgs : EventArgs
    {
        public Device Device { get; set; }
        public string SerialNumber { get; set; }
    }

    public class DeviceDeletedEventArgs : EventArgs
    {
        public Device Device { get; set; }
        public string SerialNumber { get; set; }
    }

    public class DeviceGetInfoEventArgs : EventArgs
    {
        public Device Device { get; set; }
        public string Info { get; set; }
    }
}
