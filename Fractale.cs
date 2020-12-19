using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROJET_INFO_PUGET_Camille_PUVIKARAN_Thanujan
{
    class Fractale
    {
        Pixel2[,] matrice;
        

        public Fractale()
        {
            this.matrice =Attribution();
                
        }
        public Pixel2[,] Attribution()
        {
            Pixel2[,] m = new Pixel2[1000,1000];

            return m;
        }
    }
}
