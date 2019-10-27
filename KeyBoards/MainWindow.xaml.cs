using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KeyBoards
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static readonly DependencyProperty SerialNumberProperty = DependencyProperty.Register("SerialNumber", typeof(string), typeof(MainWindow));
        public string SerialNumber
        {
            get { return (string)GetValue(SerialNumberProperty); }
            set { SetValue(SerialNumberProperty, value); }
        }

        public static readonly DependencyProperty FirmwareVersionProperty = DependencyProperty.Register("FirmwareVersion", typeof(string), typeof(MainWindow));
        public string FirmwareVersion
        {
            get { return (string)GetValue(FirmwareVersionProperty); }
            set { SetValue(FirmwareVersionProperty, value); }
        }

        public static readonly DependencyProperty ButtonTypeProperty = DependencyProperty.Register("ButtonType", typeof(ButtonType), typeof(MainWindow));
        public ButtonType ButtonType
        {
            get { return (ButtonType)GetValue(ButtonTypeProperty); }
            set { SetValue(ButtonTypeProperty, value); }
        }

        public static readonly DependencyProperty EncoderTypeProperty = DependencyProperty.Register("EncoderType", typeof(EncoderType), typeof(MainWindow));
        public EncoderType EncoderType
        {
            get { return (EncoderType)GetValue(EncoderTypeProperty); }
            set { SetValue(EncoderTypeProperty, value); }
        }

        public static readonly DependencyProperty TouchScreenSizeProperty = DependencyProperty.Register("TouchScreenSize", typeof(TouchScreenSize), typeof(MainWindow));
        public TouchScreenSize TouchScreenSize
        {
            get { return (TouchScreenSize)GetValue(TouchScreenSizeProperty); }
            set { SetValue(TouchScreenSizeProperty, value); }
        }

        public ObservableCollection<Device> Devices = new ObservableCollection<Device>();
        private bool testRun = false;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            ListBoxDevices.ItemsSource = Devices;
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            var deviceType = ComboBoxDevices.SelectedIndex;
            DeviceManager instance = DeviceManager.GetInstance();
            switch (deviceType)
            {
                case 0:
                    var deviceA = new DeviceA() { SerialNumber = SerialNumber, FirmwareVersion = FirmwareVersion, ButtonType = ButtonType };
                    instance.Add(deviceA);
                    break;
                case 1:
                    var deviceB = new DeviceB() { SerialNumber = SerialNumber, FirmwareVersion = FirmwareVersion, ButtonType = ButtonType, EncoderType = EncoderType };
                    instance.Add(deviceB);
                    break;
                case 2:
                    var deviceC = new DeviceC() { SerialNumber = SerialNumber, FirmwareVersion = FirmwareVersion, ButtonType = ButtonType, EncoderType = EncoderType, TouchScreenSize = TouchScreenSize };
                    instance.Add(deviceC);
                    break;
            }
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = ListBoxDevices.SelectedItem;
            DeviceManager instance = DeviceManager.GetInstance();
            switch (selectedItem)
            {
                case DeviceA device:
                    instance.Delete(device.SerialNumber);
                    break;
                case DeviceB device:
                    instance.Delete(device.SerialNumber);
                    break;
                case DeviceC device:
                    instance.Delete(device.SerialNumber);
                    break;
            }
        }

        private void ButtonClear_Click(object sender, RoutedEventArgs e)
        {
            DeviceManager instance = DeviceManager.GetInstance();
            instance.Clear();
            Devices.Clear();
        }

        private void ButtonExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ButtonTest_Click(object sender, RoutedEventArgs e)
        {
            DeviceManager deviceManager = DeviceManager.GetInstance();
            deviceManager.Clear();
            Devices.Clear();
            ListBoxTest.Items.Clear();
            testRun = true;
            Task.Run(() => RunTest());
        }

        private void RunTest()
        {
            int sn = 0;
            Random random = new Random();

            int numThreads = 0;
            while (numThreads < 1000 && testRun)
            {                
                numThreads++;
                int index = random.Next(0, 9);
                switch (index)
                {
                    case 0:
                        sn++;
                        new Thread(() =>
                        {
                            Thread.Sleep(100 * (sn % 10));
                            DeviceManager deviceManager = DeviceManager.GetInstance();
                            deviceManager.Add(new DeviceA() { SerialNumber = "A" + sn, FirmwareVersion = "AV" + sn, ButtonType = (ButtonType)(sn % 2) });
                            Thread.Sleep(100 * (sn % 10));
                        }).Start();
                        break;
                    case 1:
                        sn++;
                        new Thread(() =>
                        {
                            Thread.Sleep(100 * (sn % 10));
                            DeviceManager deviceManager = DeviceManager.GetInstance();
                            deviceManager.Add(new DeviceB() { SerialNumber = "B" + sn, FirmwareVersion = "BV" + sn, ButtonType = (ButtonType)(sn % 2), EncoderType = (EncoderType)(sn % 2) });
                            Thread.Sleep(100 * (sn % 10));
                        }).Start();
                        break;
                    case 2:
                        sn++;
                        new Thread(() =>
                        {
                            Thread.Sleep(100 * (sn % 10));
                            DeviceManager deviceManager = DeviceManager.GetInstance();
                            deviceManager.Add(new DeviceC() { SerialNumber = "C" + sn, FirmwareVersion = "CV" + sn, ButtonType = (ButtonType)(sn % 2), EncoderType = (EncoderType)(sn % 2), TouchScreenSize = new TouchScreenSize() { Width = sn, Height = sn } });
                            Thread.Sleep(100 * (sn % 10));
                        }).Start();
                        break;
                    case 3:
                        new Thread(() =>
                        {
                            Thread.Sleep(100 * (sn % 10));
                            DeviceManager deviceManager = DeviceManager.GetInstance();
                            var device = deviceManager.GetDevice(random.Next(0, deviceManager.GetLenth()));
                            if (device != null)
                                deviceManager.Delete(device.SerialNumber);
                            Thread.Sleep(100 * (sn % 10));
                        }).Start();
                        break;
                    case 4:
                        new Thread(() =>
                        {
                            Thread.Sleep(100 * (sn % 10));
                            DeviceManager deviceManager = DeviceManager.GetInstance();
                            var device = deviceManager.GetDevice(random.Next(0, deviceManager.GetLenth()));
                            if (device != null)
                                deviceManager.Delete(device.SerialNumber);
                            Thread.Sleep(100 * (sn % 10));
                        }).Start();
                        break;
                    case 5:
                        new Thread(() =>
                        {
                            Thread.Sleep(100 * (sn % 10));
                            DeviceManager deviceManager = DeviceManager.GetInstance();
                            var device = deviceManager.GetDevice(random.Next(0, deviceManager.GetLenth()));
                            if (device != null)
                                deviceManager.Delete(device.SerialNumber);
                            Thread.Sleep(100 * (sn % 10));
                        }).Start();
                        break;
                    case 6:
                        new Thread(() =>
                        {
                            Thread.Sleep(100 * (sn % 10));
                            DeviceManager deviceManager = DeviceManager.GetInstance();
                            var device = deviceManager.GetDevice(random.Next(0, deviceManager.GetLenth()));
                            if (device != null)
                                deviceManager.Delete(device.SerialNumber);
                            Thread.Sleep(100 * (sn % 10));
                        }).Start();
                        break;
                    case 7:
                        new Thread(() =>
                        {
                            Thread.Sleep(100 * (sn % 10));
                            DeviceManager deviceManager = DeviceManager.GetInstance();
                            var device = deviceManager.GetDevice(random.Next(0, deviceManager.GetLenth()));
                            if (device != null)
                                deviceManager.GetInfo(device.SerialNumber);
                            Thread.Sleep(100 * (sn % 10));
                        }).Start();
                        break;
                    case 8:
                        new Thread(() =>
                        {
                            Thread.Sleep(100 * (sn % 10));
                            DeviceManager deviceManager = DeviceManager.GetInstance();
                            var device = deviceManager.GetDevice(random.Next(0, deviceManager.GetLenth()));
                            if (device != null)
                                deviceManager.GetInfo(device.SerialNumber);
                            Thread.Sleep(100 * (sn % 10));
                        }).Start();
                        break;
                    case 9:
                        new Thread(() =>
                        {
                            Thread.Sleep(100 * (sn % 10));
                            DeviceManager deviceManager = DeviceManager.GetInstance();
                            var device = deviceManager.GetDevice(random.Next(0, deviceManager.GetLenth()));
                            if (device != null)
                                deviceManager.GetInfo(device.SerialNumber);
                            Thread.Sleep(100 * (sn % 10));
                        }).Start();
                        break;
                }
            }
        }

        private void ButtonTestEnd_Click(object sender, RoutedEventArgs e)
        {
            testRun = false;
        }
    }
}
