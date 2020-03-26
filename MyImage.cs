using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace PROJET_INFO_PUGET_Camille_PUVIKARAN_Thanujan
{
    class MyImage
    {

        // attributes
        private int[] type;
        private int length;
        private int offset;
        private int height;
        private byte[] header;
        private int width;
        private int color;
        private bool flag_modification = false;
        private bool flag_QR_code = false;
        private static int compteur=1;
        //private string Myfile;
        private Matrice m;//afin de faire les modifications a partir des fonction de MyImage

        // constructor
        public MyImage(string Myfile)
        {

            ReadFile(Myfile);
            compteur++;
        }

        // properties
        public bool Flag_QR_code
        {
            get => this.flag_QR_code;
            set => this.flag_QR_code = value;
        }
        public bool Flag_modification
        {
            get => this.flag_modification;
            set => this.flag_modification = value;
        }
        public int[] Type
        {
            get => this.type;
        }
        public int Length
        {
            get => this.length;
        }   
        public int Offset
        {
            get => this.offset;
        }
        public int Width
        {
            get => this.width;
        }
        public int Color
        {
            get => this.color;
            set
            {
                this.color = value;
            }
        }
        public Matrice M
        {
            get => this.m;
            set
            {
                this.m = value;
            }
        }
        public byte[] Header
        {
            get => this.header;
            set => this.header = value;
        }
        public int[] finding_type(byte[] file)
        {
            this.type = new int[3];
            if ((file[0] == 66) && (file[1] == 77))
            {
                this.type[0] = 18;
                this.type[1] = 22;
                this.type[2] = 14;
                this.offset = 54;
            }
            return this.type;
        }
        /// <summary>
        /// Take a tab
        /// </summary>
        /// <param name="tab"></param>
        /// <returns></returns>
        public int Taille(int[] tab)
        {
            int taille = 0;
            for (int i = 0; i < tab.Length; i++)
            {
                taille += tab[i] * Convert.ToInt32(Math.Pow(256, i));

            }
            return taille;
        }   
        public int[] newLength(byte[] file, int debut)
        {
            int[] tab = new int[4];
            for (int i = debut; i < debut + 4; i++)
            {
                tab[i - debut] = file[i];
            }
            return tab;
        }
        /// <summary>
        /// from file to image
        /// </summary>
        /// <param name="filename"></param>
        public void ReadFile(string filename)
        {
           
            //Console.WriteLine();
            byte[] file = File.ReadAllBytes(filename);
            Console.WriteLine(file.Length);
            this.type = finding_type(file);
            this.header = new byte[this.offset];
            //Console.WriteLine("\n Header \n");
            for (int i = 0; i < 14; i++)
            {
                //Console.Write(file[i] + " ");
                this.header[i] = file[i];
                
            }
            Console.WriteLine("Metadonnees de l image de base :");
            for (int i = 0; i < 54; i++)
            {
                Console.Write(file[i] + " ");
                if (i == 13) Console.WriteLine();
            }
            Console.WriteLine();
            //Métadonnées de l'image
            //Console.WriteLine("\n HEADER INFO \n");
            for (int i = 14; i < this.offset; i++)
            {
                //Console.Write(file[i] + " ");
                this.header[i] = file[i];
            }

            this.width = Taille(newLength(file, this.type[0]));
            this.height = Taille(newLength(file, this.type[1]));

            Pixel2[,] Matrix = new Pixel2[this.width, this.height];
            this.m = new Matrice(Matrix);
            Console.WriteLine("this.Width = " + this.width);
            Console.WriteLine("this.Height = " + this.height);






            //L'image elle-même
            //Console.WriteLine("\n IMAGE \n");

            //for (int i = this.offset; i < file.Length; i = i + 3 * this.width)
            //{
            //    for (int j = i; j < i + 3 * this.height; j = j + 3)
            //    {
            //        this.m.Matrix[(i - this.offset) / (3 * height), ((j - i) / 3)] = new Pixel2(file[j], file[j + 1], file[j + 2]);
            //        //Console.Write(this.m.Matrix[(i - this.offset) / (3 * height), ((j - i) / 3)].Blue +" ");
            //        //Console.Write(m.Matrix[(i - this.offset) / (3 * height), ((j - i) / 3)].Green + " ");
            //        //Console.Write(m.Matrix[(i - this.offset) / (3 * height), ((j - i) / 3)].Red + " ");
            //        //(i - this.offset) / (3 * height)
            //        //((j - i) / 3)
            //    }
            //    // Console.WriteLine();
            //}
            //int compteur1 = 0;
            //for (int i = 0; i < m.Matrix.GetLength(0); i++)
            //{
            //    for (int j = 0; j < m.Matrix.GetLength(1); j++)
            //    {

            //        if (m.Matrix[i, j] == null) compteur1++;
            //    }
            //}
            //m.Matrix[36, 0] = new Pixel2(255,255,0);
            //Console.WriteLine(compteur1);
            if (compteur != 2)
            {
                Console.Write("Que voulez vous faire, voulez vous utiliser des fonctions du type modification ou juste afficher l'image " + "\n" + "d image pre-existante ou voulez une fractale ou encore un histogramme,");
                Console.WriteLine();
                Console.Write("si  vous voulez une modification d'image (modification d'image n'inclut pas caché image) veuillez taper modification sinon tapez autre : ");
                string verification = Console.ReadLine();
                if (verification == "modification")
                {
                    this.flag_modification = true;
                    int compteur = 0;
                    for (int i = 0; i < m.Matrix.GetLength(1); i++)
                    {
                        for (int j = 0; j < m.Matrix.GetLength(0); j++)
                        {

                            m.Matrix[j, i] = new Pixel2(file[this.header.Length + compteur], file[this.header.Length + compteur + 1], file[this.header.Length + compteur + 2]);
                            compteur = compteur + 3;
                        }

                    }
                    m.Matrix = Remodelage();
                }
                else
                {
                    Console.Write("Qr_code ? si oui tapez true sinon false  :");
                    string a = Console.ReadLine();
                    if (a == "true")
                    {
                        flag_QR_code = true;
                    }
                    else
                    {
                        int compteur = 0;
                        for (int i = 0; i < m.Matrix.GetLength(0); i++)
                        {
                            for (int j = 0; j < m.Matrix.GetLength(1); j++)
                            {

                                m.Matrix[i, j] = new Pixel2(file[this.header.Length + compteur], file[this.header.Length + compteur + 1], file[this.header.Length + compteur + 2]);
                                compteur = compteur + 3;
                            }

                        }
                    }
                }
            }
            else
            {
                int compteur = 0;
                for (int i = 0; i < m.Matrix.GetLength(0); i++)
                {
                    for (int j = 0; j < m.Matrix.GetLength(1); j++)
                    {

                       m.Matrix[i, j] = new Pixel2(file[this.header.Length + compteur], file[this.header.Length + compteur + 1], file[this.header.Length + compteur + 2]);
                       compteur = compteur + 3;
                    }

                }
                
            }
            Console.WriteLine("Clear");

        }
        public Pixel2[,] Remodelage()
        {
            Pixel2[,] mat = m.Matrix;
            int a = 0;
            int b = m.Matrix.GetLength(1);
            int c = m.Matrix.GetLength(0);
            Console.WriteLine("m.Matrix.GetLength(0)=" + c);
            Console.WriteLine("m.Matrix.GetLength(1)=" + b);
            Pixel2[,] Mat5;
            if (m.Matrix.GetLength(0) < m.Matrix.GetLength(1))
            {
                a = m.Matrix.GetLength(1);
            }
            else
            {
                a = m.Matrix.GetLength(0);
            }
            int taille = 0;
            if (a % 4 == 0)
            {
                taille = a;
            }
            else if (a % 4 == 1)
            {
                taille = a + 3;
            }
            else if (a % 4 == 2)
            {
                taille = a + 2;
            }
            else
            {
                taille = a + 1;
            }
            Mat5 = new Pixel2[taille, taille];
            Console.WriteLine("Mat5.GetLength(1)=" + a);


            for (int i = 0; i < Mat5.GetLength(1); i++)
            {
                for (int j = 0; j < Mat5.GetLength(0); j++)
                {
                    if (i >= m.Matrix.GetLength(1) || j >= m.Matrix.GetLength(0))
                    {
                        Mat5[i, j] = new Pixel2(0, 0, 0);
                    }
                    else
                    {
                        Mat5[i, j] = new Pixel2(m.Matrix[j, i].Blue, m.Matrix[j, i].Green, m.Matrix[j, i].Red);
                    }
                }
                
            }
            return Mat5;
        }

        public void From_Image_To_File(Pixel2[,] Mat, string file)
        {

            byte[] returned = new byte[this.header.Length + (Mat.GetLength(0) * Mat.GetLength(1) * 3)];
            this.width = Mat.GetLength(0);
            this.height = Mat.GetLength(1);
            //Console.Write("sdkdhksdhf");
            Console.WriteLine("Mat.GetLength(0) = " + Mat.GetLength(0));
            Console.WriteLine("Mat.GetLength(1) = " + Mat.GetLength(1));
            Console.WriteLine("Poid = " + this.height * this.width * 3);
            byte[] a = Convertir_Int_To_Endian(Mat.GetLength(0));//type[0]
            byte[] b = Convertir_Int_To_Endian(Mat.GetLength(1));//type[1]
            byte[] tab = Convertir_Int_To_Endian(this.height * this.width * 3 + 54);//taille totale
            byte[] tab3 = Convertir_Int_To_Endian(this.height * this.width * 3);
            Console.WriteLine("height=" + this.height);
            Console.WriteLine("width=" + this.width);
            for (int i = 0; i < this.header.Length; i++)
            {
                if (i == 2 || i == 3 || i == 4 || i == 5)
                {
                    returned[i] = tab[i-2];
                }
                else if (i == 34 || i == 35 || i == 36 || i == 37)
                {
                    returned[i] =tab3[i-34];
                }
                else if (i == type[0]|| i == type[0]+1|| i == type[0]+2|| i == type[0]+3)
                {
                    returned[i] = a[i - type[0]];
                    //Console.Write(returned[i] + " ");
                }
                else if (i == type[1] || i == type[1] + 1 || i == type[1] + 2 || i == type[1] + 3)
                {
                    returned[i] = b[i - type[1]];
                    //Console.Write(returned[i] + " ");
                }
                else
                {
                    returned[i] = this.header[i];
                    //Console.Write(returned[i] + " ");
                }
                //returned[28] = 24;
                //returned[10] = 54;
                //returned[14] = 40;
                //Console.Write(returned[i] + " ");
                //if (i == 13) Console.WriteLine();
            }


            //Console.WriteLine(Mat.GetLength(1));

            //Mat = new Pixel2[this.m.Matrix.GetLength(0), this.m.Matrix.GetLength(1)];

            int compteur = 0;
            for (int i = 0; i < Mat.GetLength(0); i++)
            { 
                for (int j = 0; j < Mat.GetLength(1); j++)
                {

                    returned[this.header.Length + compteur] = Mat[i, j].Blue;
                    returned[this.header.Length + compteur + 1] = Mat[i, j].Green;
                    returned[this.header.Length + compteur + 2] = Mat[i, j].Red;
                    compteur = compteur + 3;
                    //Console.Write(this.m.Matrix[i, j].Blue+" ");
                    //Console.Write(this.m.Matrix[i, j].Green+" ");
                    //Console.Write(this.m.Matrix[i, j].Red+" ");


                }
                
            }
            Console.WriteLine("Metadonees pour la nouvelle image");
            for (int i = 0; i < 54; i++)
            {
                Console.Write(returned[i] + " ");
                if (i == 13) Console.WriteLine();
            }
            //for(int i = 18; i < 22; i++)
            //{
            //    Console.Write(returned[i]+" ");
            //    Console.Write(header[i] + " ");
            //}
            //Console.WriteLine();

            Console.WriteLine("ARRIVEE !");
            File.WriteAllBytes(file, returned);
        }
        public byte[] Convertir_Int_To_Endian(int value)
        {
            byte[] tab = new byte[4];
            //int value2 = 0;
            Console.WriteLine("value=" + value);
            for (int i = tab.Length - 1; i >= 0; i--)
            {
                int reste = value % Convert.ToInt32(Math.Pow(256, i));
                //Console.Write("reste=" + reste);
                value = value / Convert.ToInt32(Math.Pow(256, i));
                //Console.WriteLine();
                //Console.Write("value="+value);
                tab[i] = Convert.ToByte(value);
                //Console.Write(tab[i]+" ");
                value = reste;
                Console.Write(tab[i] + " ");
            }
            Console.WriteLine();
            return tab;
        }
        //public int verification_angle(int angle)
        //{
        //    while ((angle < 0) || (angle > 360))
        //    {
        //        if (angle < 0)
        //        {
        //            angle += 360;
        //        }
        //        else if (angle > 360)
        //        {
        //            angle -= 360;
        //        }
        //    }
        //    return angle;

        //}
        //public bool verification(Pixel2[,] mat, int i, int j)
        //{
        //    bool flag = true;
        //    if (mat[i, j] == null)
        //    {
        //        flag = false;
        //    }
        //    return flag;
        //}
        //public Pixel2[,] rotate_with_angles(int angle)
        //{
        //    //int[] centre = new int[2];
        //    //centre[0] = this.width / 2;
        //    //centre[1] = this.height / 2;
        //    Pixel2[,] Mat2 = new Pixel2[10000, 10000];//Convert.ToInt32(Math.Sqrt(Math.Pow(height, 2) + Math.Pow(width, 2)))];
        //    angle = verification_angle(angle);
        //    for (double i = 0; i < Mat2.GetLength(0); i++)
        //    {
        //        for (double j = 0; j < Mat2.GetLength(1); j++)
        //        {
        //            byte b = 0;
        //            byte g = 0;
        //            byte r = 0;
        //            int x = Convert.ToInt32((i) * (Math.Cos(angle)) + (j) * Math.Sin(angle));
        //            int y = Convert.ToInt32(-(i) * Math.Sin(angle) + (j) * (Math.Cos(angle)));
        //            if (((x >= 0) && (x < m.Matrix.GetLength(0))) && (y >= 0) && (y < m.Matrix.GetLength(1)))
        //            {

        //                b = m.Matrix[x, y].Blue;
        //                g = m.Matrix[x, y].Green;
        //                r = m.Matrix[x, y].Red;
        //                Mat2[(int)i, (int)j] = new Pixel2(b, g, r);
        //            }
                    
        //            if (Mat2[x, y]== null)
        //            {
        //                //if (((x > 0) && (x < m.Matrix.GetLength(0)-1))&&(((y > 0) && (y < m.Matrix.GetLength(1)-1))))
        //                // {
        //                if ((x == 0) || (y == 0) || (x == Mat2.GetLength(0)) || (y == Mat2.GetLength(1)))
        //                {
        //                    Mat2[x, y] = new Pixel2(0, 0, 0);
        //                }
        //                else
        //                {
        //                    if ((verification(Mat2, x - 1, y) == true) && (verification(Mat2, x, y - 1) == true))
        //                    {

        //                        b = Convert.ToByte((m.Matrix[x - 1, y].Blue + m.Matrix[x, y - 1].Blue) / 2);
        //                        g = Convert.ToByte((m.Matrix[x - 1, y].Green + m.Matrix[x, y - 1].Green) / 2);
        //                        r = Convert.ToByte((m.Matrix[x - 1, y].Red + m.Matrix[x, y - 1].Red) / 2);
        //                        Mat2[x, y] = new Pixel2(b, g, r);
        //                    }
        //                    else if ((verification(Mat2, x - 1, y) == true) && (verification(Mat2, x, y - 1) == false))
        //                    {

        //                        b = Convert.ToByte(m.Matrix[x - 1, y].Blue);
        //                        g = Convert.ToByte(m.Matrix[x - 1, y].Green);
        //                        r = Convert.ToByte(m.Matrix[x - 1, y].Red);
        //                        Mat2[x, y] = new Pixel2(b, g, r);
        //                    }
        //                    else if ((verification(Mat2, x - 1, y) == false) && (verification(Mat2, x, y - 1) == true))
        //                    {

        //                        b = Convert.ToByte(m.Matrix[x, y - 1].Blue);
        //                        g = Convert.ToByte(m.Matrix[x, y - 1].Green);
        //                        r = Convert.ToByte(m.Matrix[x, y - 1].Red);
        //                        Mat2[x, y] = new Pixel2(b, g, r);
        //                    }
        //                    else
        //                    {
        //                        Mat2[x, y] = new Pixel2(0, 0, 0);
        //                    }
        //                }
                        
        //                //}
                        
        //            }

        //            //Console.WriteLine(j);

        //        }
        //        //Console.WriteLine(i);
        //    }
            
        //    return Mat2;
        //}

        public Pixel2[,] negatif()
        {
            Pixel2[,] Mat3 = new Pixel2[m.Matrix.GetLength(0), m.Matrix.GetLength(1)];
            for (int i = 0; i < Mat3.GetLength(0); i++)
            {
                for (int j = 0; j < Mat3.GetLength(1); j++)
                {
                    int b = 255 - Convert.ToInt32(m.Matrix[i, j].Blue);
                    int g = 255 - Convert.ToInt32(m.Matrix[i, j].Green);
                    int r = 255 - Convert.ToInt32(m.Matrix[i, j].Red);

                    Mat3[i, j] = new Pixel2(Convert.ToByte(b), Convert.ToByte(g), Convert.ToByte(r));
                }
            }
            return Mat3;
        }
        ///uint pour les entiers non signés
        //public byte convert()
        public Pixel2[,] Rotation180()
        {
            int[] centre = new int[2];
            centre[0] = this.width / 2;
            centre[1] = this.height / 2;
            Pixel2[,] Mat4 = new Pixel2[m.Matrix.GetLength(0), m.Matrix.GetLength(1)];
            int a = Mat4.GetLength(0);
            for (int i = 0; i < m.Matrix.GetLength(0); i++)
            {
                for (int j = 0; j < m.Matrix.GetLength(1); j++)
                {
                    if (((i <= a / 2) && (j >= a / 2)) || ((i >= a/ 2) && (j <= a / 2)))
                    {
                        Mat4[i, j] = new Pixel2(m.Matrix[a - i - 1, a - 1 - j].Blue, m.Matrix[a - i - 1, a - 1 - j].Green, m.Matrix[a - i - 1, a - 1 - j].Red);
                    }
                    else if (((i < a / 2) && (j < a / 2)) || ((i > a / 2) && (j > a / 2)))
                    {

                        Mat4[i, j] = new Pixel2(m.Matrix[a - i - 1, a - 1 - j].Blue, m.Matrix[a - i - 1, a - 1 - j].Green, m.Matrix[a - i - 1, a - 1 - j].Red);

                    }
                }
            }
            return Mat4;
        }
        public Pixel2[,] Rotation90()
        {

            Pixel2[,] Mat6 = new Pixel2[m.Matrix.GetLength(1), m.Matrix.GetLength(0)];
            Pixel2[,] MatP = new Pixel2[m.Matrix.GetLength(1), m.Matrix.GetLength(0)];
            int b = m.Matrix.GetLength(0);
            for (int i = 0; i < m.Matrix.GetLength(1); i++)
            {
                for (int j = 0; j < m.Matrix.GetLength(0); j++)
                {
                    if (((i <= b / 2) && (j >= b/ 2)) || ((i >= b / 2) && (j <= b / 2)))
                    {
                        MatP[i, j] = new Pixel2(m.Matrix[b - j - 1, b - 1 - i].Blue, m.Matrix[b - j - 1, b - 1 - i].Green, m.Matrix[b - j - 1,b - 1 - i].Red);
                    }
                    else if (((i < b / 2) && (j < b / 2)) || ((i > b / 2) && (j > b / 2)))
                    {

                        MatP[i, j] = new Pixel2(m.Matrix[b - j - 1,b - 1 - i].Blue, m.Matrix[b - j - 1, b - 1 - i].Green, m.Matrix[b - j - 1, b - 1 - i].Red);

                    }
                }
            }

            for (int i = 0; i < m.Matrix.GetLength(1); i++)
            {
                for (int j = 0; j < m.Matrix.GetLength(0); j++)
                {
                    Mat6[i, j] = new Pixel2(MatP[b - 1 - i, j].Blue, MatP[b - 1 - i, j].Green, MatP[b - 1 - i, j].Red);
                }
            }

            return Mat6;

        }

        public Pixel2[,] Rotation270()
        {
            Pixel2[,] M = new Pixel2[m.Matrix.GetLength(1), m.Matrix.GetLength(0)];
            Pixel2[,] M2 = new Pixel2[m.Matrix.GetLength(0), m.Matrix.GetLength(1)];
            Pixel2[,] Mat7 = new Pixel2[m.Matrix.GetLength(1), m.Matrix.GetLength(0)];
            int b = m.Matrix.GetLength(0);
            M = Rotation90();
            M2 = Rotation90();
            Mat7 = Rotation90();
            for (int i = 0; i < m.Matrix.GetLength(0); i++)
            {
                for (int j = 0; j < m.Matrix.GetLength(1); j++)
                {
                    Mat7[i, j] = new Pixel2(m.Matrix[b - 1 - i, j].Blue, m.Matrix[b - 1 - i, j].Green, m.Matrix[b - 1 - i, j].Red);
                }
            }
            for (int i = 0; i < m.Matrix.GetLength(0); i++)
            {
                for (int j = 0; j < m.Matrix.GetLength(1); j++)
                {
                    if (((i <= b / 2) && (j >= b / 2)) || ((i >= b / 2) && (j <= b / 2)))
                    {
                        M[i, j] = new Pixel2(Mat7[b - j - 1, b - 1 - i].Blue, Mat7[b - j - 1, b - 1 - i].Green, Mat7[b - j - 1, b - 1 - i].Red);
                    }
                    else if (((i < b / 2) && (j < b / 2)) || ((i > b / 2) && (j > b / 2)))
                    {

                        M[i, j] = new Pixel2(Mat7[b - j - 1, b - 1 - i].Blue, Mat7[b - j - 1, b - 1 - i].Green, Mat7[b - j - 1, b - 1 - i].Red);

                    }
                }
            }


            return M;
        }

        public Pixel2[,] Inverser_Image_axe_ordonnee()
        {

            ////for (int i = 0; i < Mat5.GetLength(1); i++) Mat5[0, i] = new Pixel2(0, 255, 0);
            ////for (int i = 0; i < b; i++) mat[0, i] = new Pixel2(255, 0, 0);
            Pixel2[,] Mat6 = new Pixel2[m.Matrix.GetLength(0), m.Matrix.GetLength(1)];
            int a = Mat6.GetLength(0);
            for (int i = 0; i < a; i++)
            {
                for (int j = 0; j < a; j++)
                {
                    Mat6[i, j] = new Pixel2(m.Matrix[i, a - j - 1].Blue, m.Matrix[i, a - j - 1].Green, m.Matrix[i, a - j - 1].Red);
                }
            }
            return Mat6;
        }
        public Pixel2[,] Inverser_Image_axe_abscisse()
        {
            int a = m.Matrix.GetLength(0);
            Pixel2[,] Mat5 = new Pixel2[m.Matrix.GetLength(0), m.Matrix.GetLength(1)];
            for (int i = 0; i < m.Matrix.GetLength(0); i++)
            {
                for (int j = 0; j < m.Matrix.GetLength(1); j++)
                {
                    Mat5[i, j] = new Pixel2(m.Matrix[a - 1 - i, j].Blue, m.Matrix[a - 1 - i, j].Green, m.Matrix[a - 1 - i, j].Red);
                }
            }
            return Mat5;
        }
        public Pixel2[,] Aggrandissement(float zoom)
        {
            int c = m.Matrix.GetLength(0);
            Pixel2[,] returned2 = new Pixel2[Convert.ToInt32(c * zoom), Convert.ToInt32(c * zoom)];
            int a = Convert.ToInt32(zoom);

            for (int i = 0; i < this.m.Matrix.GetLength(0); i++)
            {

                for (int j = 0; j < this.m.Matrix.GetLength(1); j++)
                {
                    byte b = m.Matrix[i, j].Blue;
                    byte g = m.Matrix[i, j].Green;
                    byte r = m.Matrix[i, j].Red;
                    returned2[i * a, j * a] = new Pixel2(b, g, r);
                    for (int k = 1; k < a; k++)
                    {
                        for(int l = 0; l < a; l++)
                        {
                            returned2[i * a, j * a + k] = new Pixel2(b, g, r);
                            returned2[i * a + k, j * a] = new Pixel2(b, g, r);
                            returned2[i * a + k, j * a + k] = new Pixel2(b, g, r);
                            returned2[i * a + l, j * a + k] = new Pixel2(b, g, r);
                            returned2[i * a + k, j * a + l] = new Pixel2(b, g, r);
                        }
                    }



                }
            }
            //for(int i = 0; i < returned2.GetLength(0); i++)
            //{
            //    for (int j= 0; j < returned2.GetLength(1); j++)
            //    {
            //        Console.Write(returned2[i, j].Blue + " ");
            //    }
            //    Console.WriteLine();
            //}
            return returned2;
        }
        public Pixel2[,] reduction(float zoom)
        {
            int c = m.Matrix.GetLength(0);

            Pixel2[,] returned2 = new Pixel2[Convert.ToInt32(c / zoom), Convert.ToInt32(c / zoom)];
            int a = Convert.ToInt32(zoom);

            for (int i = 0; i < returned2.GetLength(0); i++)
            {

                for (int j = 0; j < returned2.GetLength(1); j++)
                {
                    byte b = m.Matrix[i * a, j * a].Blue;
                    byte g = m.Matrix[i * a, j * a].Green;
                    byte r = m.Matrix[i * a, j * a].Red;
                    returned2[i, j] = new Pixel2(b, g, r);
                    //Console.Write(m.Matrix[i, j].Blue + " ");
                    //Console.Write(returned2[i, j].Blue+" ");



                }
                Console.WriteLine();
            }
            //Console.Write(returned2.GetLength(0) + "dsfsg " + returned2.GetLength(1));
            return returned2; 
        }
        public Pixel2 Somme(int i,int j)
        {
            int somme = 0;
            int somme1 = 0;
            int somme2 = 0;
            
            somme =m.Matrix[i-1,j-1].Blue+ m.Matrix[i, j - 1].Blue+ m.Matrix[i - 1, j].Blue+ m.Matrix[i, j].Blue+ m.Matrix[i+1, j].Blue+ m.Matrix[i, j + 1].Blue+ m.Matrix[i+1, j +1].Blue+ m.Matrix[i - 1, j + 1].Blue+ m.Matrix[i + 1, j - 1].Blue;
            somme1 = m.Matrix[i - 1, j - 1].Green + m.Matrix[i, j - 1].Green + m.Matrix[i - 1, j].Green + m.Matrix[i, j].Green + m.Matrix[i + 1, j].Green + m.Matrix[i, j + 1].Green + m.Matrix[i + 1, j + 1].Green + m.Matrix[i - 1, j + 1].Green + m.Matrix[i + 1, j - 1].Green;
            somme1 = m.Matrix[i - 1, j - 1].Red + m.Matrix[i, j - 1].Red + m.Matrix[i - 1, j].Red + m.Matrix[i, j].Red + m.Matrix[i + 1, j].Red + m.Matrix[i, j + 1].Red + m.Matrix[i + 1, j + 1].Red + m.Matrix[i - 1, j + 1].Red + m.Matrix[i + 1, j - 1].Red;
            Pixel2 a = new Pixel2(Convert.ToByte(somme / 9), Convert.ToByte(somme1 / 9), Convert.ToByte(somme2 / 9));
            return a;
        }
        public Pixel2[,] bleutage()
        {
            
            Pixel2[,] Mat = new Pixel2[m.Matrix.GetLength(0), m.Matrix.GetLength(1)] ;

            for (int i = 0; i < m.Matrix.GetLength(0); i++)
            {

                for (int j = 0; j < m.Matrix.GetLength(1); j++)
                {
                    //byte a = m.Matrix[i + k, j + k]+
                    //Mat[i,j]=
                    if (i == 0 || j == 0 || i == m.Matrix.GetLength(0) - 1 || j == m.Matrix.GetLength(1) - 1)
                    {
                        Mat[i, j] = new Pixel2(m.Matrix[i, j].Blue, m.Matrix[i, j].Green, m.Matrix[i, j].Red);
                    }
                    else
                    {
                        Mat[i, j] = Somme(i, j);
                    }
                   
                }
            }
            return Mat;
        }
        
        public Pixel2 Multiplication(int x, int y, int[,] convolution,string type)
        {
            int b = 0;
            int g = 0;
            int r = 0;
            double w = 1.0;
            if (type == "oui")
            {
                if (convolution.GetLength(0) == 3)
                {
                    w = 0.11;
                }
                else if(convolution.GetLength(0) == 5)
                {
                    w = 0.004;
                }
            }
            //{
            //    for (int i = 0; i < convolution.GetLength(0); i++)
            //    {
            //        for (int j = 0; j < convolution.GetLength(1); j++)
            //        {
            //            somme += Math.Abs(convolution[i, j]);
            //        }
            //    }
            //}
            int k = -convolution.GetLength(0)/2;
            //int h = -convolution.GetLength(0)/2;
            int l= convolution.GetLength(0); 
            for (int i = 0; i < l; i++)
            {
                for(int j = 0; j < l; j++)
                {
                    //if (i == j)
                    //{
                    b += (int)(w*convolution[i, j] * m.Matrix[x + k + i, y + k + j].Blue);
                    //Console.Write(convolution[i, j] * m.Matrix[x + k + i, y + k + j].Blue);
                    g += (int)(w*convolution[i, j] * m.Matrix[x + k + i, y + k + j].Green);
                    r += (int)(w*convolution[i, j] * m.Matrix[x + k + i, y + k + j].Red);
                    //}
                    //else if (i < j)
                    //{

                    //    b += (convolution[i, j] * m.Matrix[x + k + i, y + k + j].Blue);
                    //    g += (convolution[i, j] * m.Matrix[x + k + i, y + k + j].Green);
                    //    r += (convolution[i, j] * m.Matrix[x + k + i, y + k + j].Red);
                    //}
                    //else//i>j
                    //{
                        
                    //    b += (convolution[i, j] * m.Matrix[x + k+i, y + k + j].Blue);
                    //    g += (convolution[i, j] * m.Matrix[x + k + i, y + k + j].Green);
                    //    r += (convolution[i, j] * m.Matrix[x + k + i, y + k + j].Red);
                    //}
                    
                }
                //Console.WriteLine(" ");
            }
            //Console.WriteLine(b);
            byte blue = 0;
            byte green = 0;
            byte red = 0;
            if (b > 255)
            {
                blue = 255;
            }
            else
            {
                blue = (byte)(Math.Abs(b));
            }
            if(g > 255)
            {
                green = 255;
            }
            else
            {
                green = (byte)(Math.Abs(g));
            }
            if (r > 255)
            {
                red = 255;
            }
            else
            {
                red = (byte)(Math.Abs(r));
            }
            Pixel2 ab = new Pixel2(blue, green, red);
            return ab;
        }

        public Pixel2[,] Floutage_etc(int[,] convolution)
        {
            Console.Write("est ce floutage?");
            string type = Console.ReadLine();
            //|| j == 0 || i == m.Matrix.GetLength(0) - 1 || j == m.Matrix.GetLength(1) - 1
            Pixel2[,]Mat= new Pixel2[m.Matrix.GetLength(0), m.Matrix.GetLength(1)];
            int v = convolution.GetLength(0)/2;
            for (int i = 0; i < m.Matrix.GetLength(0); i++)
            {

                for (int j = 0; j < m.Matrix.GetLength(1); j++)
                {
                    if (i < v)
                    {
                        for (int k = 0; k < v; k++)
                        {
                            Mat[i + k, j] = new Pixel2(m.Matrix[i + k, j].Blue, m.Matrix[i + k, j].Green, m.Matrix[i + k, j].Red);
                        }
                    }
                    else if(j<v)
                    {
                        for (int k = 0; k < v; k++)
                        {
                            Mat[i, j + k] = new Pixel2(m.Matrix[i, j + k].Blue, m.Matrix[i, j + k].Green, m.Matrix[i, j + k].Red);
                        }
                    }
                    else if (i> m.Matrix.GetLength(0)-1 - v)
                    {
                        for (int k = 0; k < v; k++)
                        {
                            Mat[i - k, j] = new Pixel2(m.Matrix[i - k, j].Blue, m.Matrix[i - k, j].Green, m.Matrix[i - k, j].Red);
                        }
                    }
                    else if(j > m.Matrix.GetLength(1) - 1-v)
                    {
                        for (int k = 0; k < v; k++)
                        {
                            Mat[i, j - k] = new Pixel2(m.Matrix[i, j - k].Blue, m.Matrix[i, j - k].Green, m.Matrix[i, j - k].Red);
                        }
                    }
                    else
                    {
                        Mat[i, j] = Multiplication(i, j, convolution,type);
                    }
                    
                    

                }
            }
            return Mat;
        }
        //AUCUNE UTILITE CAR CE N ETAIT QU UN TEST
        //public Pixel2 [,] convol_3(int[,] convolution)
        //{
        //    Pixel2[,] returned = new Pixel2[m.Matrix.GetLength(0), m.Matrix.GetLength(0)];
        //    int l = returned.GetLength(0);
        //    int L = returned.GetLength(1);
        //    int somme = 0;
        //    for(int i = 0; i < convolution.GetLength(0); i++)
        //    {
        //        for(int j = 0; j < convolution.GetLength(1); j++)
        //        {
        //            somme += Math.Abs(convolution[i, j]);
        //        }
        //    }
        //    if (somme == 0) somme = 9;
        //    for (int i = 0; i < l; i++)
        //    {
        //        for(int j = 0; j < L; j++)
        //        {
        //            if (i == 0 || j == 0||i==l-1|j==L-1)
        //            {
        //                returned[i, j] = m.Matrix[i, j];
        //            }
        //            else
        //            {
        //                byte blue = 0;
        //                byte green = 0;
        //                byte red = 0;
        //                blue = (byte)((convolution[0, 0] *m.Matrix[i-1,j-1].Blue+convolution[1,0]*m.Matrix[i,j-1].Blue+ convolution[2, 0] * m.Matrix[i+1, j - 1].Blue+ convolution[0, 1] * m.Matrix[i-1, j].Blue+ convolution[0, 2] * m.Matrix[i-1, j+1].Blue+ convolution[1, 1] * m.Matrix[i, j].Blue+ convolution[2, 2] * m.Matrix[i+1, j + 1].Blue+ convolution[2, 1] * m.Matrix[i+1, j].Blue+ convolution[2, 1] * m.Matrix[i, j+1].Blue));
        //                green = (byte)((convolution[0, 0] * m.Matrix[i - 1, j - 1].Green + convolution[1, 0] * m.Matrix[i, j - 1].Green + convolution[2, 0] * m.Matrix[i + 1, j - 1].Green + convolution[0, 1] * m.Matrix[i - 1, j].Green + convolution[0, 2] * m.Matrix[i - 1, j + 1].Green + convolution[1, 1] * m.Matrix[i, j].Green + convolution[2, 2] * m.Matrix[i + 1, j + 1].Green + convolution[2, 1] * m.Matrix[i + 1, j].Green + convolution[2, 1] * m.Matrix[i, j + 1].Green));
        //                red = (byte)((convolution[0, 0] * m.Matrix[i - 1, j - 1].Red + convolution[1, 0] * m.Matrix[i, j - 1].Red + convolution[2, 0] * m.Matrix[i + 1, j - 1].Red + convolution[0, 1] * m.Matrix[i - 1, j].Red + convolution[0, 2] * m.Matrix[i - 1, j + 1].Red + convolution[1, 1] * m.Matrix[i, j].Red + convolution[2, 2] * m.Matrix[i + 1, j + 1].Red + convolution[2, 1] * m.Matrix[i + 1, j].Red + convolution[2, 1] * m.Matrix[i, j + 1].Red));
        //                if (blue > 255)
        //                {
        //                    blue = 255;
        //                }
        //                else if (blue < 0)
        //                {
        //                    blue = 0;
        //                }
        //                if (green > 255)
        //                {
        //                    green = 255;
        //                }
        //                else if (green < 0)
        //                {
        //                    green = 0;
        //                }
        //                if (red > 255)
        //                {
        //                    red = 255;
        //                }
        //                else if (red < 0)
        //                {
        //                    red = 0;
        //                }
        //                returned[i, j]=new Pixel2(blue,green,red);
        //            }
        //        }
        //    }
        //    return returned;
        //}
        public Pixel2[,] Rotation(float angle)
        {
            float angle_in_radian = Convert.ToSingle((angle * Math.PI)/180);
            int a = (int)Math.Sqrt(Math.Pow(m.Matrix.GetLength(0), 2)+ Math.Pow(m.Matrix.GetLength(1), 2));
            Pixel2[,] Mat =new Pixel2[2*a,2*a];
            int px = (Mat.GetLength(0) - m.Matrix.GetLength(0)) / 2;
            int py = (Mat.GetLength(1) - m.Matrix.GetLength(1)) / 2;


            for (int i = 0; i < m.Matrix.GetLength(0); i++)
            {
                for(int j = 0; j < m.Matrix.GetLength(1); j++)
                {
                    int x = Convert.ToInt32((i) * (Math.Cos(angle_in_radian)) + (j) * Math.Sin(angle_in_radian));
                    int y = Convert.ToInt32(-(i) * Math.Sin(angle_in_radian) + (j) * (Math.Cos(angle_in_radian)));
                    byte b = m.Matrix[i, j].Blue;
                    byte g = m.Matrix[i, j].Green;
                    byte r = m.Matrix[i, j].Red;

                    if (angle==0||angle==360)
                    {
                        Mat[Math.Abs(x+px), Math.Abs(y)+py] = new Pixel2(b, g, r);
                    }                    
                    else if (angle < 90 && angle>0)
                    {
                        Mat[Math.Abs(x + px), Math.Abs(y + py)] = new Pixel2(b, g, r);
                    }
                    else if (angle < 180 && angle >= 90)
                    {
                        Mat[x + px, y + 2 * py] = new Pixel2(b, g, r);
                    }
                    else if (angle < 270 && angle >= 180)
                    {
                        Mat[x + 2 * px, y + 2* py] = new Pixel2(b, g, r);
                    }
                    else if (angle < 360 && angle >= 270)
                    {
                        Mat[x+2*px, y+py] = new Pixel2(b, g, r);
                    }
                }
            }
            for (int i = 0; i < Mat.GetLength(0); i++)
            {
                for (int j = 0; j < Mat.GetLength(1); j++)
                {

                    if (Mat[i, j] == null)
                    {
                        Mat[i, j] = new Pixel2(0, 0,0);
                    }
                }
            }
            for(int i = 1; i < Mat.GetLength(0)-1; i++)
            {
                for(int j = 1; j < Mat.GetLength(1)-1; j++)
                {
                    if ((Mat[i, j].Blue == 0) && (Mat[i, j].Green==0) && (Mat[i, j].Red==0))
                    {
                        byte b = Convert.ToByte((Mat[i - 1, j - 1].Blue + Mat[i, j - 1].Blue + Mat[i - 1, j].Blue + Mat[i + 1, j - 1].Blue + Mat[i - 1, j + 1].Blue + Mat[i, j + 1].Blue + Mat[i + 1, j].Blue + Mat[i + 1, j + 1].Blue) / 8);
                        byte g = Convert.ToByte((Mat[i - 1, j - 1].Green + Mat[i, j - 1].Green + Mat[i - 1, j].Green + Mat[i + 1, j - 1].Green + Mat[i - 1, j + 1].Green + Mat[i, j + 1].Green + Mat[i + 1, j].Green + Mat[i + 1, j + 1].Green)/8);
                        byte r = Convert.ToByte((Mat[i - 1, j - 1].Red + Mat[i, j - 1].Red + Mat[i - 1, j].Red + Mat[i + 1, j - 1].Red + Mat[i - 1, j + 1].Red + Mat[i, j + 1].Red + Mat[i + 1, j].Red + Mat[i + 1, j + 1].Red)/8);
                        Mat[i, j] = new Pixel2(b, g, r);
                    }
                }
            }

            return Mat;
        }
        
    }
}

