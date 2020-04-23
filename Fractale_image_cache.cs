using System;
using System.IO;


namespace PROJET_INFO_PUGET_Camille_PUVIKARAN_Thanujan
{
    public class Fractale_image_cache
    {
        //attributes
        Pixel2[,] matrice;
        private Complexe c;
        private byte[] header = new byte[54];
        private int length;
        private int height;
        //constructor
        public Fractale_image_cache()
        {
            this.matrice = attribution();
        }
        //properties
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
        /// <summary>
        /// Function which attribute O to the cells of the matrix
        /// </summary>
        /// <returns></returns>
        public Pixel2[,] attribution()
        {
            this.matrice = new Pixel2[500, 500];

            for (int i = 0; i < this.matrice.GetLength(0); i++)
            {
                for (int j = 0; j < this.matrice.GetLength(1); j++)
                {
                    this.matrice[i, j] = new Pixel2(0, 0, 0);
                }
            }
            return this.matrice;

        }
        /// <summary>
        /// Picture which gives us a mandelbrot fractal
        /// I must specify that the value used in the function, or proposed by the computer code are from the website : https://complexe.jimdofree.com/la-g%C3%A9om%C3%A9trie-complexe-2d/principe-de-construction-de-l-ensemble-de-mandelbrot/ , https://mathete.net/la-fractale-de-mandelbrot/?cn-reloaded=1
        /// </summary>
        /// <param name="maxiteration"></param>
        /// <returns></returns>
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
        /// <summary>
        /// julia set, source: from the same website for mandelbrot set
        /// </summary>
        /// <param name="maxiteration"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        public Pixel2[,] Julia(int maxiteration, Complexe c)
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
        /// <summary>
        /// the function is not finished currently and if it's not finished I would like your help in order to finish it, this is my email: thanujan.puvikaran@edu.devinci.fr ;my source :https://fr.wikipedia.org/wiki/Fractale_de_Newton
        /// </summary>
        /// <param name="iteration"></param>
        /// <param name="posX"></param>
        /// <param name="posY"></param>
        /// <param name="width"></param>
        /// <returns></returns>
        public Pixel2[,] Fractale_Newton(double posX, double posY, int iteration, int width)
        {
            // taille de l'image
            double scale = -posY * 2 / width;
            double tmp = 0;
            Pixel2[,] mat = new Pixel2[width, width];
            double d;
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < width; y++)
                {
                    Complexe z = new Complexe(posX + x * scale, posY + y * scale);
                    for (int i = 1; i < iteration; i++)
                    {
                        d = 3.0 * (Math.Pow(z.Carre().Pr, 2) + Math.Pow(z.Carre().Pi, 2));
                        if (d == 0.0)
                        {
                            d = 0.00001;
                        }
                        /* Decomposition of z = z^3 -1*/

                        tmp = z.Pr;
                        z.Pr = (2.0 / 3.0) * z.Pr + (z.Carre().Pr) / d; //+ (posX + x * scale);
                        z.Pi = (2.0 / 3.0) * z.Pi - (2.0 * tmp * z.Pi) / d; //+ (posY + y * scale);
                    }
                    int j;
                    j = 255;
                    if (z.Pr > 0.0)
                    {
                        mat[x, y] = new Pixel2((byte)j, 0, 0);
                    }
                    else
                    {
                        if ((z.Pr < -0.3) && (z.Pi > 0.0))
                        {

                            mat[x, y] = new Pixel2(0, (byte)j, 0);
                        }
                        else
                        {
                            mat[x, y] = new Pixel2(0, 0, (byte)j);
                        }
                    }
                }
            }
            return mat;


            //Complexe r1 = new Complexe(1,0);
            //Complexe r2 = new Complexe(-1 / 2, a.Pi);
            //Complexe r3 = new Complexe(-1 / 2, -a.Pi);
            //this.Height = matrice.GetLength(0);
            //this.Length = matrice.GetLength(1);
            //double xmax = 2;
            //double xmin = -2;
            ////double ymin = -1.5;
            ////double ymax = 1.5;
            //Pixel2[,] returned = new Pixel2[height, length];
            ////Pixel2 color = new Pixel2(120, 120, 120); //just to color it in grey 
            //Pixel2 color1 = new Pixel2(0, 0, 255);//red
            //Pixel2 color2 = new Pixel2(0, 255, 0);//green
            //Pixel2 color3 = new Pixel2(255, 0, 0);//blue
            //Pixel2 color4 = new Pixel2(0, 0, 0);//black
            //int apparition1 = 0;
            //int apparition2 = 0;
            //int apparition3 = 0;
            //Complexe r = new Complexe(0, 0);
            ////Complexe j_c = new Complexe(-1/2,Math.Sqrt(3)/2);
            //for (int i = 0; i < height; i++)
            //{
            //    for (int j = 0; j < length; j++)
            //    {
            //        if (i == 0 || j == 0 || i == height - 1 || j == length - 1)
            //        {
            //            returned[j, i] = color4;
            //        }
            //        if (returned[j, i] == null)
            //        {
            //            Complexe z = new Complexe(xmin + i / (500*(xmax - xmin)), xmax - j /(500*(xmax - xmin)));
            //            if (i == 1 && j == 1) Console.Write("Complexe z1 = " + z.Pr + "+" + z.Pi + "i");
            //            try
            //            {
            //                Complexe s = Newton(z, a, epsilon);
            //                //Console.WriteLine();
            //                //Console.Write("Complexe s = " + s.Pr + "+" + s.Pi + "i");

            //                if (Math.Abs(s.Somme(r1.Negatif()).Module()) < epsilon)
            //                {
            //                    returned[j, i] = color1;//red
            //                    apparition1++;
            //                }
            //                else if ((s.Somme(r2.Negatif()).Module() < epsilon))
            //                {
            //                    returned[j, i] = color2;//green
            //                    apparition2++;
            //                }
            //                else if (s.Somme(r3.Negatif()).Module() < epsilon)
            //                {
            //                    returned[j, i] = color3;//blue
            //                    apparition3++;
            //                }
            //                else
            //                {
            //                    returned[j, i] = color4;//black
            //                }
            //                r = s;
            //            }
            //            catch (System.DivideByZeroException)
            //            {
            //                // returned[j, i] = color4;
            //            }
            //            //catch (System.ArgumentException)
            //            //{
            //            //    //returned[j, i] = color4;
            //            //}
            //        }
            //    }
            //}
            //Console.WriteLine();
            ////Console.WriteLine("Complexe r = " + r.Pr + "+" + r.Pi + "i");
            //Console.Write("apparition1=" + apparition1);
            //Console.WriteLine();
            //Console.Write("apparition2=" + apparition2);
            //Console.WriteLine();
            //Console.Write("apparition3=" + apparition3);
            //Console.WriteLine();
            //return returned;
        }
        /// <summary>
        /// This function is able to give an interesting form of newton set, currently not finished, this is the link for the expected set :"https://www.google.com/url?sa=i&url=https%3A%2F%2Ffr.m.wikipedia.org%2Fwiki%2FFichier%3ANewton_COSH.jmb.jpg&psig=AOvVaw2Gf8IwLZQZsJMP5bhHUDN3&ust=1586594109532000&source=images&cd=vfe&ved=0CAIQjRxqFwoTCPC41-e53egCFQAAAAAdAAAAABAE"
        /// </summary>
        /// <param name="posX"></param>
        /// <param name="posY"></param>
        /// <param name="iteration"></param>
        /// <param name="width"></param>
        /// <returns></returns>
        public Pixel2[,] Fractale_Newton2(double posX, double posY, int iteration, int width)
        {
            // taille de l'image
            double scale = -posY * 2 / width;
            double tmp = 0;
            Pixel2[,] mat = new Pixel2[width, width];
            double d;
            // couleurs (provisoire)
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < width; y++)
                {
                    Complexe z = new Complexe(posX + x * scale, posY + y * scale);
                    //n = 0;
                    for (int i = 1; i < iteration; i++)
                    {
                        d = 3.0 * (Math.Pow(z.Carre().Pr, 2) + Math.Pow(z.Carre().Pi, 2));
                        if (d == 0.0)
                        {
                            d = 0.00001;
                        }
                        /* Decomposition of z = z^3 -1*/

                        tmp = z.Pr;
                        z.Pr = (2.0 / 3.0) * z.Pr + (z.Carre().Pr) / d; //+ (posX + x * scale);
                        z.Pi = (2.0 / 3.0) * z.Pi - (2.0 * tmp * z.Pi) / d; //+ (posY + y * scale);
                    }
                    int j;
                    j = 255;
                    if (z.Pr > 0.0)
                    {
                        mat[x, y] = new Pixel2((byte)j, 0, 0);
                    }
                    else
                    {
                        if ((z.Pr < -0.3) && (z.Pi > 0.0))
                        {

                            mat[x, y] = new Pixel2(0, (byte)j, 0);
                        }
                        else
                        {
                            mat[x, y] = new Pixel2(0, 0, (byte)j);
                        }
                    }
                }
            }
            return mat;

        }
        /// <summary>
        /// Histogram made in order to represent a picture with the color the user wants, it may be blue, red or green
        /// Idea we can develop, creating a computer code in which the user is asked to propose a color with the value of the red pixel, blue pixel and the green one, 
        /// and then the code will return the quantity of that particular color in the four part of the picture 
        /// </summary>
        /// <param name="mat"></param>
        /// <returns></returns>
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
        /// Convert an int value to little indian  
        /// We could have used the one of MyImage but it would have taken much more memory space
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public byte[] Little_Indian(int value)
        {
            byte[] tab = new byte[4];
            for (int i = tab.Length - 1; i >= 0; i--)
            {
                int reste = value % Convert.ToInt32(Math.Pow(256, i));
                value = value / Convert.ToInt32(Math.Pow(256, i));
                tab[i] = Convert.ToByte(value);
                value = reste;
            }
            return tab;
        }
        /// <summary>
        /// Create a byte tab in which, the code fill the value like the length, the width or the the picture weight and others.
        /// </summary>
        /// <returns></returns>
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
                if (i == 10) header[i] = 54;
                else header[i] = 0;
            }
            header[14] = 40;
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
            header[26] = 1;
            header[27] = 2;
            header[28] = 24;
            for (int i = 0; i < 5; i++)
            {
                header[29 + i] = 0;
            }
            for (int i = 38; i < header.Length; i++)
            {
                header[i] = 0;
            }
            for (int i = 0; i < header.Length; i++)
            {
                Console.Write(header[i] + " ");
                if (i == 13) Console.WriteLine();
            }
            Console.WriteLine();

            return header;
        }
        /// <summary>
        /// Convert the matrix of value to a byte file including the header values
        /// </summary>
        /// <param name="Mat"></param>
        /// <param name="file"></param>
        public void From_fractale_toFile(Pixel2[,] Mat, string file)
        {
            this.length = Mat.GetLength(1);
            this.height = Mat.GetLength(0);
            this.header = Definition_Header();
            //Console.WriteLine(height + " " + length);
            byte[] returned = new byte[length * height * 3 + 54];
            for (int i = 0; i < this.header.Length; i++)
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
        /// <summary>
        /// Function which helps the code to be more understandable by calculating the value of every bits
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public byte[] From_int_to_byte(int value)
        {
            byte[] tab = new byte[8];
            for (int i = tab.Length - 1; i >= 0; i--)
            {
                int reste = value % Convert.ToInt32(Math.Pow(2, i));
                value = value / Convert.ToInt32(Math.Pow(2, i));
                tab[tab.Length - 1 - i] = Convert.ToByte(value);
                value = reste;
            }
            return tab;
        }
        /// <summary>
        /// Convert bits to int 
        /// </summary>
        /// <param name="tab"></param>
        /// <returns></returns>
        public byte From_byte_To_int(byte[] tab)
        {
            double a = 0;
            for (int i = 0; i < tab.Length; i++)
            {
                a += tab[i] * Math.Pow(2, tab.Length - 1 - i);

            }
            byte b = Convert.ToByte(a);
            return b;
        }
        /// <summary>
        /// Hide a picture in an other, the first one is the one which hide
        /// </summary>
        /// <param name="mat1"></param>
        /// <param name="mat2"></param>
        /// <returns></returns>
        public Pixel2[,] codage(Pixel2[,] mat1, Pixel2[,] mat2)
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
            if (mat1.GetLength(1) <= mat2.GetLength(1))
            {
                largeur = mat2.GetLength(1);
            }
            else
            {
                largeur = mat1.GetLength(1);
            }
            Pixel2[,] mat = new Pixel2[hauteur, largeur];
            for (int i = 0; i < hauteur; i++)
            {
                for (int j = 0; j < largeur; j++)
                {
                    byte b = retrieving(From_int_to_byte(mat1[i, j].Blue), From_int_to_byte(mat2[i, j].Blue));
                    byte g = retrieving(From_int_to_byte(mat1[i, j].Green), From_int_to_byte(mat2[i, j].Green)); ;
                    byte r = retrieving(From_int_to_byte(mat1[i, j].Red), From_int_to_byte(mat2[i, j].Red)); ;
                    mat[i, j] = new Pixel2(b, g, r);
                }
            }
            return mat;
        }
        /// <summary>
        /// Return the two pictures fused before
        /// </summary>
        /// <param name="mat1"></param>
        /// <returns></returns>
        public Pixel2[,] decodage(Pixel2[,] mat1)
        {
            Console.Write("Quelle image voulez vous, 2 possibilites soit 1 soit 2 : ");
            int which_one = Convert.ToInt32(Console.ReadLine());
            Pixel2[,] mat = new Pixel2[mat1.GetLength(0), mat1.GetLength(1)];
            for (int i = 0; i < mat.GetLength(0); i++)
            {
                for (int j = 0; j < mat.GetLength(1); j++)
                {
                    byte b = returning(From_int_to_byte(mat1[i, j].Blue), which_one);
                    byte g = returning(From_int_to_byte(mat1[i, j].Green), which_one);
                    byte r = returning(From_int_to_byte(mat1[i, j].Red), which_one);
                    mat[i, j] = new Pixel2(b, g, r);
                }
            }

            return mat;
        }
        /// <summary>
        /// Take two tab and keep only the first four values of the tabs and put them together 
        /// The first four values of the tab returned is the picture which is hiding and the four last are the value of the hidden picture 
        /// </summary>
        /// <param name="tab1"></param>
        /// <param name="tab2"></param>
        /// <returns></returns>
        public byte retrieving(byte[] tab1, byte[] tab2)
        {
            byte[] tab = new byte[8];
            byte value = 0;
            for (int i = 0; i < 4; i++)
            {
                tab[i] = tab1[i];
                tab[i + 4] = tab2[i];
            }
            value = From_byte_To_int(tab);
            return value;
        }
        /// <summary>
        /// Separate the bytes tab in two tabs and then return one of them depending of the user choice or the computer code
        /// </summary>
        /// <param name="tab"></param>
        /// <param name="which_one"></param>
        /// <returns></returns>
        public byte returning(byte[] tab, int which_one)
        {
            byte value = 0;
            byte[] tab1 = new byte[8];
            if (which_one == 1)
            {
                for (int i = 0; i < 8; i++)
                {
                    if (i < 4)
                    {
                        tab1[i] = tab[i];
                    }
                    else
                    {
                        tab1[i] = 0;
                    }
                }
            }
            else
            {
                for (int i = 0; i < 8; i++)
                {
                    if (i < 4)
                    {
                        tab1[i] = tab[i + 4];
                    }
                    else
                    {
                        tab1[i] = 0;
                    }
                }
            }
            value = From_byte_To_int(tab1);
            return value;
        }

    }
}
