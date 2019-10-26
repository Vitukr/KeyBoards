using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyBoards
{
    public struct TouchScreenSize
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public string GetInfo { get { return Width + " / " + Height; } }
    }
}
