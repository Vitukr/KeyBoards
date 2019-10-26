using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyBoards
{
    public enum ButtonType 
    {
        [Description("Cherry Red")]
        CherryRed,
        [Description("Cherry Brown")]
        CherryBrown 
    }
    public enum EncoderType 
    {
        [Description("Alps")]
        Alps = 0,
        [Description("Bourns")]
        Bourns = 1 
    }
    public abstract class Device
    {
        public string SerialNumber { get; set; }
        public string FirmwareVersion { get; set; }
        public ButtonType ButtonType { get; set; }

        public virtual string GetInfo
        {
            get { return SerialNumber + " : " + FirmwareVersion + " : " + ButtonType; }
        }
    }
}
