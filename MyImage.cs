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

        //private string Myfile;
        private Matrice m;

        // constructor
        public MyImage(string Myfile)
        {

            ReadFile(Myfile);

        }

        // properties
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

            this.type = finding_type(file);
            this.header = new byte[this.offset];
            Console.WriteLine("\n Header \n");
            for (int i = 0; i < 14; i++)
            {
                Console.Write(file[i] + " ");
                this.header[i] = file[i];
            }
            for (int i = 0; i < 54; i++)
            {
                Console.Write(header[i]+" ");
            }

            //Métadonnées de l'image
            Console.WriteLine("\n HEADER INFO \n");
            for (int i = 14; i < this.offset; i++)
            {
                Console.Write(file[i] + " ");
                this.header[i] = file[i];
            }

            this.width = Taille(newLength(file, this.type[0]));
            this.height = Taille(newLength(file, this.type[1]));

            Pixel2[,] Matrix = new Pixel2[this.height, this.width];
            this.m = new Matrice(Matrix);


	




            //L'image elle-même
            //Console.WriteLine("\n IMAGE \n");

            for (int i = this.offset; i < file.Length; i = i + 3 * this.height)
            {
                for (int j = i; j < i + 3 * this.height; j = j + 3)
                {
                    this.m.Matrix[ (i - this.offset) / (3 * height), ((j - i) / 3)] = new Pixel2(file[j], file[j + 1], file[j + 2]);
                    //Console.Write(this.m.Matrix[(i - this.offset) / (3 * height), ((j - i) / 3)].Blue +" ");
                    //Console.Write(m.Matrix[(i - this.offset) / (3 * height), ((j - i) / 3)].Green + " ");
                    //Console.Write(m.Matrix[(i - this.offset) / (3 * height), ((j - i) / 3)].Red + " ");
                    //(i - this.offset) / (3 * height)
                    //((j - i) / 3)
                }
                Console.WriteLine();
            }



        }

        public void From_Image_To_File(Pixel2[,] Mat, string file)
        {

            byte[] returned = new byte[this.header.Length + (Mat.GetLength(0) * Mat.GetLength(1) * 3)];
            //Console.Write("sdkdhksdhf");
            byte[] a = Convertir_Int_To_Endian(Mat.GetLength(0));//type[1]
            byte[] b = Convertir_Int_To_Endian(Mat.GetLength(1));//type[0]
            for (int i = 0; i < this.header.Length; i++)
            {
                if (i == type[0]|| i == type[0]+1|| i == type[0]+2|| i == type[0]+3)
                {
                    returned[i] = a[i - type[0]];
                    //Console.Write(returned[i] + " ");
                }
                else if (i == type[1] || i == type[1] + 1 || i == type[1] + 2 || i == type[1] + 3)
                {
                    returned[i] = a[i - type[1]];
                    //Console.Write(returned[i] + " ");
                }
                else
                {
                    returned[i] = this.header[i];
                    //Console.Write(returned[i] + " ");
                }
                
            }
            



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

            //for (int i = 0; i < returned.Length; i++)
            //{
            //    //Console.Write(returned[i]+" ");
            //}
            for(int i = 18; i < 22; i++)
            {
                Console.Write(returned[i]+" ");
                Console.Write(header[i] + " ");
            }
            Console.WriteLine();
            

            File.WriteAllBytes(file, returned);
        }
        public byte[] Convertir_Int_To_Endian(int value)
        {
            byte[] tab = new byte[4];
            //int value2 = 0;
            for (int i = tab.Length - 1; i >= 0; i--)
            {
                int reste = value % Convert.ToInt32(Math.Pow(256, i));
                value = value / Convert.ToInt32(Math.Pow(256, i));
                //Console.Write(value);
                tab[i] = Convert.ToByte(value);
                //Console.Write(tab[i]+" ");
                value = reste;
            }
            return tab;
        }
        public int verification_angle(int angle)
        {
            while ((angle < 0) || (angle > 360))
            {
                if (angle < 0)
                {
                    angle += 360;
                }
                else if (angle > 360)
                {
                    angle -= 360;
                }
            }
            return angle;

        }
        public bool verification(Pixel2[,] mat, int i, int j)
        {
            bool flag = true;
            if (mat[i, j] == null)
            {
                flag = false;
            }
            return flag;
        }
        public Pixel2[,] rotate_with_angles(int angle)
        {
            //int[] centre = new int[2];
            //centre[0] = this.width / 2;
            //centre[1] = this.height / 2;
            Pixel2[,] Mat2 = new Pixel2[10000, 10000];//Convert.ToInt32(Math.Sqrt(Math.Pow(height, 2) + Math.Pow(width, 2)))];
            angle = verification_angle(angle);
            for (double i = 0; i < Mat2.GetLength(0); i++)
            {
                for (double j = 0; j < Mat2.GetLength(1); j++)
                {
                    byte b = 0;
                    byte g = 0;
                    byte r = 0;
                    int x = Convert.ToInt32((i) * (Math.Cos(angle)) + (j) * Math.Sin(angle));
                    int y = Convert.ToInt32(-(i) * Math.Sin(angle) + (j) * (Math.Cos(angle)));
                    if (((x >= 0) && (x < m.Matrix.GetLength(0))) && (y >= 0) && (y < m.Matrix.GetLength(1)))
                    {

                        b = m.Matrix[x, y].Blue;
                        g = m.Matrix[x, y].Green;
                        r = m.Matrix[x, y].Red;
                        Mat2[(int)i, (int)j] = new Pixel2(b, g, r);
                    }
                    
                    if (Mat2[x, y]== null)
                    {
                        //if (((x > 0) && (x < m.Matrix.GetLength(0)-1))&&(((y > 0) && (y < m.Matrix.GetLength(1)-1))))
                        // {
                        if ((x == 0) || (y == 0) || (x == Mat2.GetLength(0)) || (y == Mat2.GetLength(1)))
                        {
                            Mat2[x, y] = new Pixel2(0, 0, 0);
                        }
                        else
                        {
                            if ((verification(Mat2, x - 1, y) == true) && (verification(Mat2, x, y - 1) == true))
                            {

                                b = Convert.ToByte((m.Matrix[x - 1, y].Blue + m.Matrix[x, y - 1].Blue) / 2);
                                g = Convert.ToByte((m.Matrix[x - 1, y].Green + m.Matrix[x, y - 1].Green) / 2);
                                r = Convert.ToByte((m.Matrix[x - 1, y].Red + m.Matrix[x, y - 1].Red) / 2);
                                Mat2[x, y] = new Pixel2(b, g, r);
                            }
                            else if ((verification(Mat2, x - 1, y) == true) && (verification(Mat2, x, y - 1) == false))
                            {

                                b = Convert.ToByte(m.Matrix[x - 1, y].Blue);
                                g = Convert.ToByte(m.Matrix[x - 1, y].Green);
                                r = Convert.ToByte(m.Matrix[x - 1, y].Red);
                                Mat2[x, y] = new Pixel2(b, g, r);
                            }
                            else if ((verification(Mat2, x - 1, y) == false) && (verification(Mat2, x, y - 1) == true))
                            {

                                b = Convert.ToByte(m.Matrix[x, y - 1].Blue);
                                g = Convert.ToByte(m.Matrix[x, y - 1].Green);
                                r = Convert.ToByte(m.Matrix[x, y - 1].Red);
                                Mat2[x, y] = new Pixel2(b, g, r);
                            }
                            else
                            {
                                Mat2[x, y] = new Pixel2(0, 0, 0);
                            }
                        }
                        
                        //}
                        
                    }

                    //Console.WriteLine(j);

                }
                //Console.WriteLine(i);
            }
            
            return Mat2;
        }

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
            Pixel2[,] Mat4 = new Pixel2[m.Matrix.GetLength(1), m.Matrix.GetLength(0)];
            for (int i = 0; i < m.Matrix.GetLength(0); i++)
            {
                for (int j = 0; j < m.Matrix.GetLength(1); j++)
                {
                    if (((i <= this.height / 2) && (j >= this.width / 2)) || ((i >= this.height / 2) && (j <= this.width / 2)))
                    {
                        Mat4[i, j] = new Pixel2(m.Matrix[this.height - 1 - i, this.width - j - 1].Blue, m.Matrix[this.height - 1 - i, this.width - j - 1].Green, m.Matrix[this.height - 1 - i, this.width - j - 1].Red);
                    }
                    else if (((i < this.height / 2) && (j < this.width / 2)) || ((i > this.height / 2) && (j > this.width / 2)))
                    {

                        Mat4[i, j] = new Pixel2(m.Matrix[this.height - 1 - i, this.width - j - 1].Blue, m.Matrix[this.height - 1 - i, this.width - j - 1].Green, m.Matrix[this.height - 1 - i, this.width - j - 1].Red);

                    }
                }
            }
            return Mat4;
        }
        public Pixel2[,] Rotation90()
        {

            Pixel2[,] Mat6 = new Pixel2[m.Matrix.GetLength(1), m.Matrix.GetLength(0)];
            Pixel2[,] MatP = new Pixel2[m.Matrix.GetLength(1), m.Matrix.GetLength(0)];

            for (int i = 0; i < m.Matrix.GetLength(0); i++)
            {
                for (int j = 0; j < m.Matrix.GetLength(1); j++)
                {
                    if (((i <= this.height / 2) && (j >= this.width / 2)) || ((i >= this.height / 2) && (j <= this.width / 2)))
                    {
                        MatP[i, j] = new Pixel2(m.Matrix[this.width - j - 1, this.height - 1 - i].Blue, m.Matrix[this.width - j - 1, this.height - 1 - i].Green, m.Matrix[this.width - j - 1, this.height - 1 - i].Red);
                    }
                    else if (((i < this.height / 2) && (j < this.width / 2)) || ((i > this.height / 2) && (j > this.width / 2)))
                    {

                        MatP[i, j] = new Pixel2(m.Matrix[this.width - j - 1, this.height - 1 - i].Blue, m.Matrix[this.width - j - 1, this.height - 1 - i].Green, m.Matrix[this.width - j - 1, this.height - 1 - i].Red);

                    }
                }
            }

            for (int i = 0; i < m.Matrix.GetLength(0); i++)
            {
                for (int j = 0; j < m.Matrix.GetLength(1); j++)
                {
                    Mat6[i, j] = new Pixel2(MatP[this.height - 1 - i, j].Blue, MatP[this.height - 1 - i, j].Green, MatP[this.height - 1 - i, j].Red);
                }
            }

            return Mat6;

        }

        public Pixel2[,] Rotation270()
        {
            Pixel2[,] M = new Pixel2[m.Matrix.GetLength(1), m.Matrix.GetLength(0)];
            Pixel2[,] M2 = new Pixel2[m.Matrix.GetLength(0), m.Matrix.GetLength(1)];
            Pixel2[,] Mat7 = new Pixel2[m.Matrix.GetLength(1), m.Matrix.GetLength(0)];
            M = Rotation90();
            M2 = Rotation90();
            Mat7 = Rotation90();
            for (int i = 0; i < m.Matrix.GetLength(0); i++)
            {
                for (int j = 0; j < m.Matrix.GetLength(1); j++)
                {
                    Mat7[i, j] = new Pixel2(m.Matrix[this.height - 1 - i, j].Blue, m.Matrix[this.height - 1 - i, j].Green, m.Matrix[this.height - 1 - i, j].Red);
                }
            }
            for (int i = 0; i < m.Matrix.GetLength(0); i++)
            {
                for (int j = 0; j < m.Matrix.GetLength(1); j++)
                {
                    if (((i <= this.height / 2) && (j >= this.width / 2)) || ((i >= this.height / 2) && (j <= this.width / 2)))
                    {
                        M[i, j] = new Pixel2(Mat7[this.width - j - 1, this.height - 1 - i].Blue, Mat7[this.width - j - 1, this.height - 1 - i].Green, Mat7[this.width - j - 1, this.height - 1 - i].Red);
                    }
                    else if (((i < this.height / 2) && (j < this.width / 2)) || ((i > this.height / 2) && (j > this.width / 2)))
                    {

                        M[i, j] = new Pixel2(Mat7[this.width - j - 1, this.height - 1 - i].Blue, Mat7[this.width - j - 1, this.height - 1 - i].Green, Mat7[this.width - j - 1, this.height - 1 - i].Red);

                    }
                }
            }


            return M;
        }

        public Pixel2[,] Inverser_Image_axe_ordonnee()
        {
            Pixel2[,] Mat5 = new Pixel2[m.Matrix.GetLength(0), m.Matrix.GetLength(1)];
            for (int i = 0; i < m.Matrix.GetLength(0); i++)
            {
                for (int j = 0; j < m.Matrix.GetLength(1); j++)
                {
                    Mat5[i, j] = new Pixel2(m.Matrix[i, this.width - j - 1].Blue, m.Matrix[i, this.width - j - 1].Green, m.Matrix[i, this.width - j - 1].Red);
                }
            }
            return Mat5;
        }
        public Pixel2[,] Inverser_Image_axe_abscisse()
        {
            Pixel2[,] Mat5 = new Pixel2[m.Matrix.GetLength(0), m.Matrix.GetLength(1)];
            for (int i = 0; i < m.Matrix.GetLength(0); i++)
            {
                for (int j = 0; j < m.Matrix.GetLength(1); j++)
                {
                    Mat5[i, j] = new Pixel2(m.Matrix[this.height - 1 - i, j].Blue, m.Matrix[this.height - 1 - i, j].Green, m.Matrix[this.height - 1 - i, j].Red);
                }
            }
            return Mat5;
        }
        public Pixel2[,] Aggrandissement(float zoom)
        {

            Pixel2[,] returned2 = new Pixel2[Convert.ToInt32(this.width * zoom), Convert.ToInt32(this.height * zoom)];
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
                        returned2[i * a, j * a + k] = new Pixel2(b, g, r);
                        returned2[i * a + k, j * a] = new Pixel2(b, g, r);
                        returned2[i * a + k, j * a + k] = new Pixel2(b, g, r);
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

            Pixel2[,] returned2 = new Pixel2[Convert.ToInt32(this.width / zoom), Convert.ToInt32(this.height / zoom)];
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
        /// <summary>
        /// les matrices de convolution utilisees sont de taille 3*3 pour l'instant
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="convolution"></param>
        /// <returns></returns>
        public Pixel2 Multiplication(int x, int y, byte[,] convolution)
        {
            byte b = 0;
            byte g = 0;
            byte r = 0;

            for(int i = 0; i < convolution.GetLength(0); i++)
            {
                for(int j = 0; j < convolution.GetLength(1); j++)
                {
                    //b+=
                }
            }
            
            Pixel2 ab = new Pixel2(b, g, r);
            return ab;
        }

        public Pixel2[,] Floutage(byte[,] convolution)
        {
            
            //|| j == 0 || i == m.Matrix.GetLength(0) - 1 || j == m.Matrix.GetLength(1) - 1
            Pixel2[,]Mat= new Pixel2[m.Matrix.GetLength(0), m.Matrix.GetLength(1)];

            int v = convolution.GetLength(0)/2;
            for (int i = 0; i < m.Matrix.GetLength(0); i++)
            {

                for (int j = 0; j < m.Matrix.GetLength(1); j++)
                {
                    if (i == 0)
                    {
                        for (int k = 1; k < v; k++)
                        {
                            Mat[i+k, j] = new Pixel2(m.Matrix[i+k, j].Blue, m.Matrix[i+k, j].Green, m.Matrix[i+k, j].Red);
                        }
                    }
                    else if(j==0)
                    {
                        for (int k = 1; k < v; k++)
                        {
                            Mat[i, j+k] = new Pixel2(m.Matrix[i, j+k].Blue, m.Matrix[i , j+k].Green, m.Matrix[i , j+k].Red);
                        }
                    }
                    else if (i == m.Matrix.GetLength(0) - 1)
                    {
                        for (int k = 1; k < v; k++)
                        {
                            Mat[i - k, j] = new Pixel2(m.Matrix[i - k, j].Blue, m.Matrix[i - k, j].Green, m.Matrix[i - k, j].Red);
                        }
                    }
                    else if(j == m.Matrix.GetLength(1) - 1)
                    {
                        for (int k = 1; k < v; k++)
                        {
                            Mat[i, j - k] = new Pixel2(m.Matrix[i, j - k].Blue, m.Matrix[i, j - k].Green, m.Matrix[i, j - k].Red);
                        }
                    }
                    else
                    {
                        Mat[i, j] = Multiplication(i, j, convolution);
                    }
                    
                    

                }
            }
            return Mat;
        }
        public Pixel2[,] Rotation(float angle)
        {
            int a = (int)Math.Sqrt(Math.Pow(m.Matrix.GetLength(0), 2)+ Math.Pow(m.Matrix.GetLength(1), 2));
            int px = (3*a-m.Matrix.GetLength(0))/2;
            int py =(3*a-m.Matrix.GetLength(1))/2;
            Pixel2[,] Mat =new Pixel2[3*a,3*a];
            for(int i = 0; i < m.Matrix.GetLength(0); i++)
            {
                for(int j = 0; j < m.Matrix.GetLength(1); j++)
                {
                    int x = Convert.ToInt32((i) * (Math.Cos(angle)) + (j) * Math.Sin(angle));
                    int y = Convert.ToInt32(-(i) * Math.Sin(angle) + (j) * (Math.Cos(angle)));
                    byte b = m.Matrix[i, j].Blue;
                    byte g = m.Matrix[i, j].Green;
                    byte r = m.Matrix[i, j].Red;
                    if(angle==0||angle==360)
                    {
                        Mat[Math.Abs(x+px), Math.Abs(y+py)] = new Pixel2(b, g, r);
                    }                    
                    else if (angle < 90 && angle>0)
                    {
                        Mat[Math.Abs(x+px), Math.Abs(y+py)] = new Pixel2(b, g, r);
                    }
                    else
                    {
                        /*py/8-*/
                        Mat[Math.Abs(x+px), Math.Abs(y+py)] = new Pixel2(b, g, r);
                    }
                   
                }
            }
            for (int i = 0; i < Mat.GetLength(0); i++)
            {
                for (int j = 0; j < Mat.GetLength(1); j++)
                {

                    if (Mat[i, j] == null)
                    {
                        
                        byte b = m.Matrix[i, j].Blue;
                        byte g = m.Matrix[i, j].Green;
                        byte r = m.Matrix[i, j].Red;
                        Mat[i, j] = new Pixel2(0, 0,0);
                    }
                }
            }

            return Mat;
        }
    }
}
