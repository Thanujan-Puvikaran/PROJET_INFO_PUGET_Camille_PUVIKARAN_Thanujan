using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROJET_INFO_PUGET_Camille_PUVIKARAN_Thanujan
{
    class Matrice
    {
        // attributes
        private Pixel2[,] matrix;

        // constructor
        public Matrice(Pixel2[,] matrix)
        {
            this.matrix = matrix;
        }

        // properties
        public Pixel2 [,] Matrix
        {
            get => this.matrix;
            set
            {
                matrix = value;    
            }
        }
    }
}
