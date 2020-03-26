using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;


namespace PROJET_INFO_PUGET_Camille_PUVIKARAN_Thanujan
{
    class Fractale_image_cache
    {
        Pixel2[,] matrice;
        private Complexe c;
        private byte[] header=new byte[54];
        private int length;
        private int height;
        
        public Fractale_image_cache()
        {
            this.matrice = attribution();
        }
        public int Length
        {
            get => this.length;
            set => this.length = value;
        }
        public int Height
        {
            get => this.height;
            set => this.height = value;

        }
        public byte[] Header
        {
            get => this.header;
            set => this.header = value;
        }
        public Complexe C
        {
            get => this.c;
            set => this.c = value;
        }
        public Pixel2[,] Matrice
        {
            get => this.matrice;
            set => this.matrice = value;
        }
        public Pixel2[,] attribution()
        {
            this.matrice = new Pixel2[400, 400];

            for (int i = 0; i < this.matrice.GetLength(0); i++)
            {
                for (int j = 0; j < this.matrice.GetLength(1); j++)
                {
                    this.matrice[i, j] = new Pixel2(0, 0, 0);
                }
            }
            return this.matrice;

        }
        public Pixel2[,] Mandelbrot(int maxiteration)
        {
            this.Height = matrice.GetLength(0);
            this.Length = matrice.GetLength(1);
            double xmax = 0.5;
            double xmin = -2;
            double ymin = -1.25;
            double ymax = 1.25;
            Pixel2[,] returned = new Pixel2[height, length];
            //Pixel2 color = new Pixel2(120, 120, 120); //just to color in grey 
            Pixel2 color1 = new Pixel2(0, 0, 0);
            Pixel2 color2 = new Pixel2(255, 255, 255);

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    if (i == 0 || j == 0 || i == height - 1 || j == length - 1)
                    {
                        returned[i, j] = color1;
                    }
                    if (returned[i, j] == null)
                    {
                        Complexe c = new Complexe(((j * (xmax - xmin)) / length) - 2, ((i * (ymin - ymax)) / height) + 1.25);
                        Complexe z = new Complexe(0, 0);
                        //z = Complexe.somme(z.Carre(), c);
                        int n = 0;
                        do
                        {
                            n++;
                            Complexe a = Complexe.somme(z.Carre(), c);
                            z.Pr = a.Pr;
                            z.Pi = a.Pi;
                            //z.Module()
                            if (z.Module() > 2.0) break;

                        }
                        while (n < maxiteration);
                        if (n == maxiteration)
                        {
                            returned[i, j] = color1;
                        }
                        else
                        {
                            returned[i, j] = color2;
                        }
                    }
                }
            }
            return returned;
        }
        public Pixel2[,] Julia(int maxiteration)
        {
            this.Height = matrice.GetLength(0);
            this.Length = matrice.GetLength(1);
            double xmin = -1.25;
            double xmax = 1.25;
            double ymin = -1.25;
            double ymax = 1.25;
            Pixel2[,] returned = new Pixel2[height, length];
            Pixel2 color1 = new Pixel2(0, 0, 0);
            Pixel2 color2 = new Pixel2(255, 255, 255);

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    if (i == 0 || j == 0 || i == height - 1 || j == length - 1)
                    {
                        returned[i, j] = color1;
                    }
                    if (returned[i, j] == null)
                    {
                        Complexe c = new Complexe(-0.7927, 0.1609);
                        Complexe z = new Complexe(((j * (xmax - xmin)) / length) + xmin, ((i * (ymin - ymax)) / height) + ymax);
                        //z = Complexe.somme(z.Carre(), c);

                        int n = 0;
                        do
                        {
                            n++;
                            Complexe a = Complexe.somme(z.Carre(), c);
                            z.Pr = a.Pr;
                            z.Pi = a.Pi;
                            //z.Module()
                            if (z.Module() > 2.0) break;

                        }
                        while (n < maxiteration);
                        if (n == maxiteration)
                        {
                            returned[i, j] = color1;
                        }
                        else
                        {
                            returned[i, j] = color2;
                        }
                    }
                }
            }
            return returned;
        }
        public Pixel2[,] Newton(int maxiteration)
        {
            this.Height = matrice.GetLength(0);
            this.Length = matrice.GetLength(1);
            //double xmax =0.5;
            //double xmin = -2;
            //double ymin = -1.25;
            //double ymax = 1.25;
            Pixel2[,] returned = new Pixel2[height, length];
            //Pixel2 color = new Pixel2(120, 120, 120); //just to color in grey 
            Pixel2 color1 = new Pixel2(0, 0, 0);
            Pixel2 color2 = new Pixel2(255, 255, 255);

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    if (i == 0 || j == 0 || i == height - 1 || j == length - 1)
                    {
                        returned[i, j] = color1;
                    }
                    if (returned[i, j] == null)
                    {
                        Complexe z = new Complexe(0, 1);
                        int n = 0;
                        do
                        {
                            n++;
                            Complexe a = Complexe.Division(z.Cube().multiplication_constante(2).Somme_reel(1), z.Carre().multiplication_constante(3));
                            z.Pr = a.Pr;
                            z.Pi = a.Pi;
                            //z.Module()
                            if (z.Module() > 2.0) break;

                        }
                        while (n < maxiteration);
                        if (n == maxiteration)
                        {
                            returned[i, j] = color1;//black
                        }
                        else
                        {
                            returned[i, j] = color2;//white
                        }
                    }
                }
            }
            return returned;
        }
        public Pixel2[,] Histogramme(Pixel2[,] mat)
        {
            Console.Write("Donner la valeur de zoom sachant que 1000 c'est bien et 12000 c'est trop zoom=");
            int zoom = Convert.ToInt32(Console.ReadLine());
            //Console.WriteLine(mat.GetLength(0) * mat.GetLength(1));
            Pixel2[,] Mat = new Pixel2[260, 260];//on laisse de l'espace entre chaque barre
            Console.Write("Quelle couleur ? sachant que les couleurs possibles sont rouge,bleu ou vert, noté r,b ou g : ");
            int[] tab = new int[256];
            int somme = mat.GetLength(0) * mat.GetLength(1);
            string couleur = Console.ReadLine();
            while (couleur != "r" && couleur != "g" && couleur != "b")
            {
                Console.Write("Quelle couleur ? : ");
                couleur = Console.ReadLine();
            }
            //for (int i = 0; i < tab.Length; i++)
            //{
            //    tab[i] = 0;
            //}
            int compteur = 0;
            tab[0] = 0;
            Pixel2 color = new Pixel2(0, 0, 0);
            if (couleur == "r") color = new Pixel2(0, 0, 255);
            else if (couleur == "g") color = new Pixel2(0, 255, 0);
            else color = new Pixel2(255, 0, 0);
            for (int i = 0; (i < mat.GetLength(0)); i++)
            {
                for (int j = 0; j < mat.GetLength(1); j++)
                {
                    if (couleur == "r")
                    {
                        compteur = mat[i, j].Red;
                        tab[compteur] = tab[compteur] + 1;
                    }
                    else if (couleur == "g")
                    {
                        compteur = mat[i, j].Green;
                        tab[compteur] = tab[compteur] + 1;
                    }
                    else
                    {
                        compteur = mat[i, j].Blue;
                        tab[compteur] = tab[compteur] + 1;
                    }

                }
            }
            //boucle pour savoir lesquelles sont nulles
            //for (int i = 0; i < tab.Length; i++)
            //{
            //    if (tab[i] != 0)
            //    {
            //        Console.Write("(" + i + ";" + tab[i] + ")" + " ");
            //    }
            //}
            //Console.WriteLine(somme);
            compteur = 0;
            //while (compteur < 256)
            //{

            //}
            for (int j = 0; j < Mat.GetLength(1); j++)
            {
                for (int i = 0; i < Mat.GetLength(0); i++)
                {
                    if (/*j == -1 ||*/ i == 0 /*|| (i == Mat.GetLength(0) - 1) || (j == Mat.GetLength(1) - 2)*/)
                    {
                        Mat[i, j] = new Pixel2(0, 0, 0);
                    }
                    else if (j >= tab.Length) Mat[i, j] = new Pixel2(255, 255, 255);
                    else
                    {
                        if (j == compteur && i <= ((float)tab[compteur] / (float)somme) * zoom)
                        {



                            Mat[i, j] = color;


                            //Console.WriteLine("tab[compteur]="+ tab[compteur]+"; somme= "+somme);
                            //Console.WriteLine("(tab[compteur]/somme)*100 = " + ((float)tab[compteur] / somme) * zoom);
                            //Console.WriteLine("bhhgsyf");
                            //Console.WriteLine("i="+i);
                            //Console.WriteLine("compteur="+compteur);
                            //Console.WriteLine("j="+j);
                            //z++;
                            //Mat[i, j] = new Pixel2(0, 0, 0);

                        }
                        else /*if(Mat[i, j] == null)*/
                        {
                            Mat[i, j] = new Pixel2(255, 255, 255);
                            //y++;
                        }
                    }


                }
                //Console.WriteLine();
                compteur++;
            }
            //Console.WriteLine("");
            //Console.WriteLine("value of z="+z);
            //Console.WriteLine("value of y ="+y);

            //int a = 0;
            //for(int i=0;i< Mat.GetLength(0); i++)
            //{
            //    for(int j=0;j< Mat.GetLength(1); j++)
            //    {
            //        if (Mat[i, j] == null)
            //        {
            //            a++;
            //        }
            //    }
            //}
            //Console.WriteLine(a);
            //Console.WriteLine(Mat.GetLength(0) * Mat.GetLength(1));
            return Mat;
        }
        /// <summary>
        /// Little indian convertor 
        /// We could have used the one of MyImage but it would have taken much more memory space
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public byte[] Little_Indian(int value)
        {
            byte[] tab = new byte[4];
            for(int i = tab.Length - 1; i >= 0; i--)
            {
                int reste = value % Convert.ToInt32(Math.Pow(256,i));
                value = value / Convert.ToInt32(Math.Pow(256, i));
                tab[i] = Convert.ToByte(value);
                value = reste;
            }
            return tab;
        }

        public byte[] Definition_Header()
        {
            //header = new byte[54];
            header[0] = 66;
            header[1] = 77;
            byte[] tab = Little_Indian(this.height * this.length * 3 + 54);//taille du fichier largeur*longueur*3+54
            for (int i = 0; i < 4; i++)
            {
                header[i + 2] = tab[i];
            }
            for (int i = 6; i <= 13; i++)
            {
                if (i == 10)header[i] = 54;
                else header[i] = 0; 
            }
            header[14]= 40;
            int compteur = 0;
            while (compteur <= 3)
            {
                compteur++;
                header[compteur + 14] = 0;
            }
            byte[] tab1 = Little_Indian(this.Length);//Mat.GetLength(1)
            byte[] tab2 = Little_Indian(this.Height);//Mat.GetLength(0)
            byte[] tab3 = Little_Indian(Height * Length * 3);
            for (int i = 0; i < 4; i++)
            {
                header[18 + i] = tab2[i];
                header[22 + i] = tab1[i];
                header[34 + i] = tab3[i];
            }
            header[26] =1;
            header[27] = 2;
            header[28] =24;
            for(int i = 0; i < 5; i++)
            {
                header[29+i] = 0;
            }
            for(int i = 38; i < header.Length; i++)
            {
                header[i] = 0;
            }
            for (int i = 0; i < header.Length; i++)
            {
                Console.Write(header[i]+" ");
                if (i == 13) Console.WriteLine();
            }
            Console.WriteLine();

            return header;
        }
        public void From_fractale_toFile(Pixel2[,] Mat,string file)
        {
            this.length = Mat.GetLength(1);
            this.height = Mat.GetLength(0);
            this.header = Definition_Header();
            //Console.WriteLine(height + " " + length);
            byte[] returned = new byte[length * height * 3 + 54];
            for(int i = 0; i < this.header.Length; i++)
            {
                returned[i] = this.header[i];
            }
            int compteur = 0;
            for (int i = 0; i < Mat.GetLength(0); i++)
            {
                for (int j = 0; j < Mat.GetLength(1); j++)
                {
                    returned[this.header.Length + compteur] = Mat[i, j].Blue;
                    returned[this.header.Length + compteur + 1] = Mat[i, j].Green;
                    returned[this.header.Length + compteur + 2] = Mat[i, j].Red;
                    compteur = compteur + 3;
                }
            }
            Console.WriteLine("IMAGE PRETE !");
            File.WriteAllBytes(file, returned);
        }
        public byte[] From_int_to_byte(int value)
        {
            byte[] tab = new byte[8];
            for (int i = tab.Length - 1; i >= 0; i--)
            {
                int reste = value % Convert.ToInt32(Math.Pow(2, i));
                value = value / Convert.ToInt32(Math.Pow(2, i));
                tab[tab.Length-1-i] = Convert.ToByte(value);
                value = reste;
            }
            return tab;
        }
        public byte From_byte_To_int(byte[] tab)
        {
            double a = 0;
            for(int i = 0; i < tab.Length; i++)
            {
                a += tab[i] * Math.Pow(2, tab.Length-1-i);

            }
            byte b = Convert.ToByte(a);
            return b;
        }
        public Pixel2[,] codage(Pixel2[,] mat1,Pixel2[,] mat2)
        {
            int hauteur = 0;
            int largeur = 0;
            if (mat1.GetLength(0) <= mat2.GetLength(0))
            {
                hauteur = mat2.GetLength(0);
            }
            else
            {
                hauteur = mat1.GetLength(0);
            }
            if(mat1.GetLength(1) <= mat2.GetLength(1))
            {
                largeur = mat2.GetLength(1);
            }
            else
            {
                largeur = mat1.GetLength(1);
            }
            Pixel2[,] mat = new Pixel2[hauteur,largeur];
            for (int i = 0; i <hauteur; i++)
            {
                for(int j = 0 ; j < largeur; j++)
                { 
                    byte b =retrieving(From_int_to_byte(mat1[i, j].Blue),From_int_to_byte(mat2[i, j].Blue));
                    byte g = retrieving(From_int_to_byte(mat1[i, j].Green), From_int_to_byte(mat2[i, j].Green)); ;
                    byte r = retrieving(From_int_to_byte(mat1[i, j].Red), From_int_to_byte(mat2[i, j].Red)); ;
                    mat[i, j] = new Pixel2(b, g, r);
                }
            }
            return mat;
        }
        public Pixel2[,] decodage(Pixel2[,] mat1)
        {
            Console.Write("Quelle image voulez vous, 2 possibilites soit 1 soit 2 : ");
            int which_one = Convert.ToInt32(Console.ReadLine());
            Pixel2[,] mat = new Pixel2[mat1.GetLength(0), mat1.GetLength(1)] ;
            for(int i = 0; i < mat.GetLength(0); i++)
            {
                for(int j=0;j< mat.GetLength(1); j++)
                {
                    byte b=returning(From_int_to_byte(mat1[i,j].Blue),which_one);
                    byte g= returning(From_int_to_byte(mat1[i, j].Green), which_one);
                    byte r = returning(From_int_to_byte(mat1[i, j].Red), which_one);
                    mat[i, j] = new Pixel2(b,g,r);
                }
            }
            
            return mat;
        }
        public byte retrieving(byte[] tab1, byte[] tab2)
        {
            byte[] tab = new byte[8];
            byte value = 0;
            for(int i=0; i < 4; i++)
            {
                tab[i] = tab1[i];
                tab[i + 4] = tab2[i];
            }
            value =From_byte_To_int(tab);
            return value;
        }
        public byte returning(byte[] tab,int which_one)
        {
            byte value = 0;
            byte[] tab1 = new byte[8];
            if (which_one == 1)
            {
                for(int i = 0; i < 8; i++)
                {
                    if (i < 4)
                    {
                        tab1[i] = tab[i];
                    }
                    else
                    {
                        tab1[i] =0;
                    }
                }
            }
            else
            {
                for (int i = 0; i < 8; i++)
                {
                    if (i < 4)
                    {
                        tab1[i] = tab[i+4];
                    }
                    else
                    {
                        tab1[i] =0;
                    }
                }
            }
            value = From_byte_To_int(tab1);
            return value;
        }
    }
}
