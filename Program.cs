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
            
            MyImage a = new MyImage("./Images/lena.bmp");
            //MyImage b = new MyImage("./Images/lena.bmp");
            //Complexe c = new Complexe(0.3,0.4);
            //Console.WriteLine(b.M.Matrix.GetLength(0));
            //Fractale_image_cache sup = new Fractale_image_cache();
            //Pixel2[,] cache=sup.codage(b.M.Matrix,a.M.Matrix);
            //Pixel2[,] r=sup.decodage(cache);
            //Pixel2[,] r2=sup.decodage(cache);
            //byte[] tab=sup.From_int_to_byte(120);
            //Pixel2[,] Mat16 = sup.Mandelbrot(100);
            //Pixel2[,] Mat17 = sup.Julia(100);
            //Pixel2[,] Mat18 = sup.Newton(10);
            //Pixel2[,] Mat19 = Approximation_PI(400,400);
            //Pixel2[,] Mat20 = sup.Histogramme(a.M.Matrix);
            //sup.From_fractale_toFile(Mat20, "./Images/TEST200.bmp");
            //sup.From_fractale_toFile(Mat17, "./Images/TEST170.bmp");
            //for (int i = 0; i < sup.Header.Length; i++)
            //{
            //    Console.Write(sup.Header[i] + " ");
            //    if (i == 13) Console.WriteLine();
            //}
            //Pixel2[,] Mat3 = a.negatif();
            //Pixel2[,] Mat4 = a.Rotation180();
            //Pixel2[,] Mat5 = a.Inverser_Image_axe_ordonnee(); 
            //Pixel2[,] Mat6 = a.Rotation90();
            //Pixel2[,] Mat7 = a.Inverser_Image_axe_abscisse();//doesnt
            //Pixel2[,] Mat8 = a.Rotation270();
            //Pixel2[,] Mat9 = a.Aggrandissement(2);
            //Console.WriteLine(a.M.Matrix.GetLength(0));
            //Console.Write(Mat9.GetLength(0));
            
            //Pixel2[,] Mat11 = a.reduction(8);
            //Pixel2[,] mat=new Pixel2[a.M.Matrix.GetLength(0), a.M.Matrix.GetLength(1)];
            //int[,] convolution = new int[5, 5] { {1,4,6,4,1 }, {4,16, 24, 16, 4 }, {6,24,36,24,6 }, { 4, 16, 24, 16, 4 }, { 1, 4, 6, 4, 1 } };
            //Pixel2[,] test = a.Rotation(34);

            //Pixel2[,] Mat12 = a.bleutage();
            //Pixel2[,] Mat13 = a.Rotation(359);
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
            //Pixel2[,] Mat19 = a.Floutage_etc(convolution);
            //byte[] tab = new byte[4];
            //a.Convertir_Int_To_Endian(512);
            //for(int i=0; i < 4; i++)
            //{
            //    tab[i] = a.Convertir_Int_To_Endian(512)[i];
            //    Console.Write(tab[i] + " ");
            //}

            a.From_Image_To_File(a.M.Matrix, "./Images/TEST202.bmp");
            //sup.From_fractale_toFile(Mat5, "./Images/TEST130.bmp");
            Console.ReadLine();
        }
    }
}
