using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROJET_INFO_PUGET_Camille_PUVIKARAN_Thanujan
{
    class Pixel2
    {
        // attributes
        private byte red;
        private byte green;
        private byte blue;

        // constructor
        public Pixel2(byte blue, byte green, byte red)
        {
            this.red = red;
            this.green = green;
            this.blue = blue;
        }

        // properties
        public byte Red
        {
            get => this.red;
            set
            {
                this.red = value;
            }
        }
        public byte Green
        {
            get => this.green;
            set
            {
                this.green = value;
            }
        }
        public byte Blue
        {
            get => this.blue;
            set
            {
                this.blue = value;
            }
        }
    }
}
