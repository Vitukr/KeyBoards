using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyBoards
{
    public class DeviceB : Device
    {
        public EncoderType EncoderType { get; set; }

        public override string GetInfo
        {
            get { return SerialNumber + " : " + FirmwareVersion + " : " + ButtonType + " : " + EncoderType; }
        }
    }
}
