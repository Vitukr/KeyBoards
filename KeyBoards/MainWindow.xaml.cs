using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
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
                    Devices.Add(deviceA);
                    break;
                case 1:
                    var deviceB = new DeviceB() { SerialNumber = SerialNumber, FirmwareVersion = FirmwareVersion, ButtonType = ButtonType, EncoderType = EncoderType };
                    instance.Add(deviceB);
                    Devices.Add(deviceB);
                    break;
                case 2:
                    var deviceC = new DeviceC() { SerialNumber = SerialNumber, FirmwareVersion = FirmwareVersion, ButtonType = ButtonType, EncoderType = EncoderType, TouchScreenSize = TouchScreenSize };
                    instance.Add(deviceC);
                    Devices.Add(deviceC);
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
                    Devices.Remove(device);
                    break;
                case DeviceB device:
                    instance.Delete(device.SerialNumber);
                    Devices.Remove(device);
                    break;
                case DeviceC device:
                    instance.Delete(device.SerialNumber);
                    Devices.Remove(device);
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
            RunTest();
        }

        private void RunTest()
        {

        }
    }
}
