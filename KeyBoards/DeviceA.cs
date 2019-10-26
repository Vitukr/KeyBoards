using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyBoards
{
    public class DeviceA : Device
    {
        public override string GetInfo
        {
            get { return SerialNumber + " : " + FirmwareVersion + " : " + ButtonType; }
        }
    }
}
