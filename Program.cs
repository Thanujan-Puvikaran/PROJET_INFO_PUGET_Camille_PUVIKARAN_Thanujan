using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Media;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Diagnostics;

namespace PROJET_INFO_PUGET_Camille_PUVIKARAN_Thanujan
{
    class Program
    {
        /// <summary>
        /// function which contains the firs batch of test of bitmap
        /// it doesn't have to be there !!!
        /// </summary>
        //static void bitmap_function_test()
        //{
            
        //        Bitmap a = new Bitmap("Images\\coco.bmp");
        //        Graphics h = Graphics.FromImage(a);
        //        h.TranslateTransform((float)a.Width / 2, (float)a.Height / 2);
        //        h.RotateTransform(90);

        //        h.TranslateTransform(-(float)a.Width / 2, -(float)a.Height / 2);
        //        h.DrawImage(a, new Point(0, 0));
        //        RectangleF rec = new RectangleF(10.0f, 10.0f, 100.0f, 100.0f);
        //        Bitmap Image2 = a.Clone(rec, PixelFormat.DontCare);
        //        Image2.Save("./Images/Test002_1.bmp");
        //        for (int i = 0; i < a.Width; i++)
        //            for (int j = 0; j < a.Height; j++)
        //            {
        //                Color mycolor = a.GetPixel(i, j);
        //                a.SetPixel(i, j, Color.FromArgb(255 - mycolor.R, 255 - mycolor.R, 255 - mycolor.R));

        //                //Image2.SetPixel(i, j, Color.FromArgb(255 - mycolor.R, 255 - mycolor.R, 255 - mycolor.R));
        //            }

        //        //Image2.MakeTransparent(Color.Gray);
        //        //Image2.MakeTransparent();
        //        a.Save("./Images/lenasortie3.bmp");

        //        Bitmap b = new Bitmap(a.Height, a.Width);
        //        Graphics g = Graphics.FromImage(b);
        //        g.TranslateTransform((float)a.Width / 2, (float)a.Height / 2);
        //        g.RotateTransform(90);

        //        g.TranslateTransform(-(float)a.Width / 2, -(float)a.Height / 2);
        //        g.DrawImage(a, new Point(0, 0));


        //        for (int i = 0; i < a.Width; i++)
        //        {
        //            for (int j = 0; j < a.Height; j++)
        //            {
        //                a.GetPixel(i, j);
        //            }


        //        }
        //        Console.WriteLine();
        //        for (int i = 0; i < b.Width; i++)
        //        {
        //            for (int j = 0; j < b.Height; j++)
        //            {
        //                b.GetPixel(i, j);
        //            }


        //        }
        //        Console.Write(b.GetPixel(6, 7));

        //        //b.Save("Images\\Test002_1.bmp");
        //        //Process.Start("Images\\lenasortie3.bmp");
        //        //Process.Start("Images\\Test002_1.bmp");
        //        Console.ReadLine();
            
        //}
        //public static Pixel2 [,] Approximation_PI(int taille1,int taille2)
        //{
        //    int compteur_noir = 0;
        //    int compteur_rouge = 0;
        //    int compteur_noir_cercle = 0;
        //    int compteur_rouge_cercle = 0;
        //    int px = taille1/2;
        //    int py = taille2/2;
        //    Pixel2[,] mat = new Pixel2[taille1, taille2];
        //    Random r = new Random();
        //    for (int i = 0; i < taille1; i++)
        //    {
        //        int y = r.Next(1, taille1-1);
        //        int z = r.Next(1, taille1-1);
        //        for (int j = 0; j < taille2; j++)
        //        {
        //            if (mat[i, j] == null)
        //            {
        //                if ((j == y || j == z) && (y != j))
        //                {
        //                    mat[i, j] = new Pixel2(0, 0, 0);
        //                    compteur_noir++;
        //                    if (Math.Pow(i,2) + Math.Pow(j,2) > 160000)
        //                    {
        //                        compteur_noir_cercle++;
        //                    }
                            
        //                }
        //                else if ((j == y || j == z) && (y == j))
        //                {
        //                    mat[i, j] = new Pixel2(0, 0, 255);
        //                    compteur_rouge++;
        //                    if (Math.Pow(px - i, 2) + Math.Pow(py - j, 2) > 40000)
        //                    {
        //                        compteur_rouge_cercle++;
        //                    }
        //                }
        //                else
        //                {
        //                    mat[i, j] = new Pixel2(255, 255, 255);
        //                }
        //                if (i == 0 || j == 0 || i == taille1-1 || j == taille2-1) mat[i, j] = new Pixel2(0, 0, 0);
        //            }
        //        }
        //    }
        //    decimal a = 4*((decimal)compteur_noir_cercle) / ((decimal)compteur_noir);
        //    Console.WriteLine("Nombre de points noirs total : " + compteur_noir);
        //    Console.WriteLine("Nombre de points noirs dans cercle : " + compteur_noir_cercle);
        //    Console.WriteLine("PI fist estimation : " + a);
        //    //decimal b = 4 * ((decimal)compteur_rouge_cercle) / ((decimal)compteur_rouge);
        //    Console.WriteLine("Nombre de points rouges total: " + compteur_rouge);
        //    Console.WriteLine("Nombre de points rouges dans cercle : " + compteur_rouge_cercle);
        //    //Console.WriteLine("PI second estimation : " + b);
        //    return mat;

        //}
        static void Main(string[] args)
        {
            
            MyImage a = new MyImage("./Images/new.bmp");
            Fractale_image_cache sup = new Fractale_image_cache();
            Console.WriteLine("Vous avez accès à un certain nombre de fonction en particulier : ");
            Pixel2[,] mat;
            if (a.Flag_modification == true)
            {
                ConsoleKeyInfo cki;
                Console.WindowHeight = 41;
                Console.WindowWidth = 100;
                do
                {
                    Console.Clear();
                    Console.WriteLine("Menu :\n"
                                     + "Mode négatif, taper 1 :\n"
                                     + "Rotation 180, taper 2 : \n"
                                     + "Inverser l'image par rapport à l'axe des ordonnees, taper 3 : \n"
                                     + "Rotation 90, taper 4 : \n"
                                     + "Inverser l'image par rapport à l'axe des abscisses, taper 5 : \n"
                                     + "Rotation 270, taper 6 : \n"
                                     + "Aggrandissement, taper 7 : \n"
                                     + "Reduction, taper 8 : \n"
                                     + "Bleutage, taper 9 : \n"
                                     + "Rotation pour n'importe qu'elle angle, taper 10 : \n"
                                     + "Convolution, taper 11 : \n"
                                     + "Sélectionnez l'exercice désiré ");
                    int exo = SaisieNombre();
                    switch (exo)
                    {
                        #region
                        case 1:
                            Pixel2[,] Mat3 = a.negatif();
                            mat = Mat3;
                            a.From_Image_To_File(mat, "./Images/TEST202.bmp");
                            break;
                        case 2:
                            Pixel2[,] Mat4 = a.Rotation180();
                            mat = Mat4;
                            a.From_Image_To_File(mat, "./Images/TEST202.bmp");
                            break;
                        case 3:
                            Pixel2[,] Mat5 = a.Inverser_Image_axe_ordonnee();
                            mat = Mat5;
                            a.From_Image_To_File(mat, "./Images/TEST202.bmp");
                            break;
                        case 4:
                            Pixel2[,] Mat6 = a.Rotation90();
                            mat = Mat6;
                            a.From_Image_To_File(mat, "./Images/TEST202.bmp");
                            break;
                        case 5:
                            Pixel2[,] Mat7 = a.Inverser_Image_axe_abscisse();
                            mat = Mat7;
                            a.From_Image_To_File(mat, "./Images/TEST202.bmp");
                            break;
                        case 6:
                            Pixel2[,] Mat8 = a.Rotation270();
                            mat = Mat8;
                            a.From_Image_To_File(mat, "./Images/TEST202.bmp");
                            break;
                        case 7:
                            Console.Write("Donner la valeur de zoom attendu : ");
                            int zoom = Convert.ToInt32(Console.ReadLine());
                            Pixel2[,] Mat9 = a.Aggrandissement(zoom);
                            mat = Mat9;
                            a.From_Image_To_File(mat, "./Images/TEST202.bmp");
                            break;
                        case 8:
                            Console.Write("Donner la valeur de zoom attendu, si voulez une image de taille divisé par 2 mettez 2 : ");
                            int zoom2 = Convert.ToInt32(Console.ReadLine());
                            Pixel2[,] Mat11 = a.reduction(zoom2);
                            mat = Mat11;
                            a.From_Image_To_File(mat, "./Images/TEST202.bmp");
                            break;
                        case 9:
                            Pixel2[,] Mat12 = a.bleutage();
                            mat = Mat12;
                            a.From_Image_To_File(mat, "./Images/TEST202.bmp");
                            break;
                        case 10:
                            int value = Convert.ToInt32(Console.ReadLine());
                            Pixel2[,] Mat13 = a.Rotation(value);
                            mat = Mat13;
                            a.From_Image_To_File(mat, "./Images/TEST202.bmp");
                            break;
                        case 11:
                            Console.WriteLine("Donner la taille de la matrice de convolution : ");
                            int taille = Convert.ToInt32(Console.ReadLine()); 
                            int[,] convolution = new int[taille, taille];
                            Console.WriteLine("donner les valeurs de cette matrice :");
                            for(int i = 0; i < convolution.GetLength(0); i++)
                            {
                                for(int j=0;j< convolution.GetLength(1); j++)
                                {
                                    convolution[i, j] = Convert.ToInt32(Console.ReadLine());
                                }
                            }
                            Pixel2[,] Mat19 = a.Floutage_etc(convolution);
                            mat = Mat19;
                            a.From_Image_To_File(mat, "./Images/TEST202.bmp");
                            break;


                            #endregion
                    }
                    Console.WriteLine();
                    Console.WriteLine("Tapez Escape pour sortir ou un numero d exo");
                    cki = Console.ReadKey();

                } while (cki.Key != ConsoleKey.Escape);
                Console.Read();
            }
            else
            {

                if (a.Flag_QR_code == false)
                {
                    ConsoleKeyInfo cki;
                    Console.WindowHeight = 41;
                    Console.WindowWidth = 100;
                    Console.Clear();
                    Console.WriteLine("Menu :\n"
                                     + "Fractale de Mandelbrot, taper 1 : \n"
                                     + "Fractale de Julia, taper 2 : \n"
                                     + "Codage decodage, taper 3  : \n"
                                     + "Histogramme, taper 4  : \n"
                                     + "Newton , pas encore pret, taper 5:\n"
                                     + "approximation de pi , pas encore pret, taper 6:\n"
                                     + "Sélectionnez l'exercice désiré ");
                    int exo = SaisieNombre();
                    switch (exo)
                    {
                        #region
                        case 1:
                            Console.Write("Donner le nombre d'itération avec au minimum 100 ");
                            int nombre_iteration = Convert.ToInt32(Console.ReadLine());
                            Pixel2[,] Mat16 = sup.Mandelbrot(nombre_iteration);
                            Console.WriteLine();
                            sup.From_fractale_toFile(Mat16, "./Images/TEST200.bmp");
                            break;
                        case 2:
                            Console.Write("Donner le nombre d'itération, 100 étant la meilleur valeur : ");
                            int nombre_iteration2 = Convert.ToInt32(Console.ReadLine());
                            Console.Write("Donner la partie reelle du complexe, une bonne valeur est -0.7927 : ");
                            double pr = Convert.ToDouble(Console.ReadLine());
                            Console.WriteLine();
                            Console.Write("Donner la partie imaginaire du complexe, une bonne valeur est 0.1609  : ");
                            double pi = Convert.ToDouble(Console.ReadLine());
                            //Complexe c = new Complexe(-0.7927, 0.1609);
                            Complexe c = new Complexe(pr, pi);
                            Pixel2[,] Mat17 = sup.Julia(nombre_iteration2, c);
                            sup.From_fractale_toFile(Mat17, "./Images/TEST200.bmp");
                            break;
                        case 4:
                            Pixel2[,] Mat20 = sup.Histogramme(a.M.Matrix);
                            sup.From_fractale_toFile(Mat20, "./Images/TEST200.bmp");
                            break;
                        case 3:

                            MyImage b = new MyImage("./Images/lena.bmp");
                            Pixel2[,] cache = sup.codage(a.M.Matrix, b.M.Matrix);
                            Pixel2[,] r = sup.decodage(cache);
                            Pixel2[,] r2 = sup.decodage(cache);
                            sup.From_fractale_toFile(cache, "./Images/TEST400.bmp");
                            sup.From_fractale_toFile(r, "./Images/TEST401.bmp");
                            sup.From_fractale_toFile(r2, "./Images/TEST402.bmp");
                            break;
                        case 5:
                            
                            break;
                        case 6:
                            
                            break;
                            #endregion
                    }
                    Console.WriteLine();
                    Console.WriteLine("Tapez Escape pour sortir ou un numero d exo");
                    cki = Console.ReadKey();
                    while (cki.Key != ConsoleKey.Escape) ;
                    //Pixel2[,] Mat18 = sup.Newton(10);
                    //Pixel2[,] Mat19 = Approximation_PI(400, 400);
                }

                else
                {
                    //QR code function
                }
            } 

            //for (int i = 0; i < sup.Header.Length; i++)
            //{
            //    Console.Write(sup.Header[i] + " ");
            //    if (i == 13) Console.WriteLine();
            //}
            //Console.WriteLine(a.M.Matrix.GetLength(0));
            //Console.Write(Mat9.GetLength(0));

            //Pixel2[,] mat=new Pixel2[a.M.Matrix.GetLength(0), a.M.Matrix.GetLength(1)];



            //for (int i=0;i< a.M.Matrix.GetLength(0); i++)
            //{
            //    for(int j = 0; j < a.M.Matrix.GetLength(1); j++)
            //    {
            //       // mat[i,j] = new Pixel2(a.M.Matrix[i, j].Blue, a.M.Matrix[i, j].Green, a.M.Matrix[i, j].Red);
            //    }
            //}
            //Console.Write(Math.Cos(Math.PI/3));
            //Pixel2[,] ran = new Pixel2[400,400];
            //int compteur_noir = 0;
            //int compteur_rouge = 0;
            //int px = 0;
            //int py = 0;
            //int k = 0;
            //for(int i = 0; i < 400; i++)
            //{
            //    if (i * i < 400)
            //    {
            //        ran[i*i, i] = new Pixel2(0, 0, 0);
            //    }
            //}
            //for(int i = 0; i < 400; i++)
            //{  
            //    for (int j = 0; (j < 400); j++)
            //    {
            //        if (ran[i, j] == null)
            //        {
            //            ran[i, j] = new Pixel2(0, 255, 255);
            //        }
            //    }
            //}
            //for(int i = 0; i < 400; i++)
            //{
            //    int y = r.Next(1,399);
            //    int z= r.Next(1, 399);
            //    for (int j = 0; j < 400; j++)
            //    {
            //        if (ran[i, j] == null)
            //        {
            //            if ((j == y || j == z) && (y != j))
            //            {
            //                ran[i, j] = new Pixel2(0, 0, 0);
            //                compteur_noir++;
            //                for (int n = px; (n <= i)&&(i!=px); n++)
            //                {
            //                    k = ((j - py) / (i - px)) * n + j - ((j - py) / (i - px)) * i;
            //                    ran[i, k] = new Pixel2(0, 0, 0);
            //                }
            //                px = i;
            //                py = j;
            //            }
            //            else if ((j == y || j == z) && (y == j))
            //            {
            //                ran[i, j] = new Pixel2(0, 0, 255);
            //                compteur_rouge++;

            //            }
            //            else
            //            {
            //                ran[i, j] = new Pixel2(255, 255, 255);
            //            }
            //            if (i == 0 || j == 0 || i == 399 || j == 399) ran[i, j] = new Pixel2(0, 0, 0);
            //        }
            //    }
            //}
            //Console.Write("Donner le type : ");
            //string type = Console.ReadLine();

            //byte[] tab = new byte[4];
            //a.Convertir_Int_To_Endian(512);
            //for(int i=0; i < 4; i++)
            //{
            //    tab[i] = a.Convertir_Int_To_Endian(512)[i];
            //    Console.Write(tab[i] + " ");
            //}

            
            //sup.From_fractale_toFile(Mat5, "./Images/TEST130.bmp");
            Console.ReadLine();
        }
        static int SaisieNombre()
        {
            int saisie = Convert.ToInt32(Console.ReadLine());
            return saisie;
        }

    }
}
    
