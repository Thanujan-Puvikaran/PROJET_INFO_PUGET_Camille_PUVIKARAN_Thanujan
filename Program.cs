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
        static void bitmap_function_test()
        {
            
                Bitmap a = new Bitmap("Images\\lena.bmp");
                Graphics h = Graphics.FromImage(a);
                h.TranslateTransform((float)a.Width / 2, (float)a.Height / 2);
                h.RotateTransform(90);

                h.TranslateTransform(-(float)a.Width / 2, -(float)a.Height / 2);
                h.DrawImage(a, new Point(0, 0));
                RectangleF rec = new RectangleF(10.0f, 10.0f, 100.0f, 100.0f);
                Bitmap Image2 = a.Clone(rec, PixelFormat.DontCare);
                Image2.Save("./Images/Test002_1.bmp");
                for (int i = 0; i < a.Width; i++)
                    for (int j = 0; j < a.Height; j++)
                    {
                        Color mycolor = a.GetPixel(i, j);
                        a.SetPixel(i, j, Color.FromArgb(255 - mycolor.R, 255 - mycolor.R, 255 - mycolor.R));

                        //Image2.SetPixel(i, j, Color.FromArgb(255 - mycolor.R, 255 - mycolor.R, 255 - mycolor.R));
                    }

                //Image2.MakeTransparent(Color.Gray);
                //Image2.MakeTransparent();
                a.Save("./Images/lenasortie3.bmp");

                Bitmap b = new Bitmap(a.Height, a.Width);
                Graphics g = Graphics.FromImage(b);
                g.TranslateTransform((float)a.Width / 2, (float)a.Height / 2);
                g.RotateTransform(90);

                g.TranslateTransform(-(float)a.Width / 2, -(float)a.Height / 2);
                g.DrawImage(a, new Point(0, 0));


                for (int i = 0; i < a.Width; i++)
                {
                    for (int j = 0; j < a.Height; j++)
                    {
                        a.GetPixel(i, j);
                    }


                }
                Console.WriteLine();
                for (int i = 0; i < b.Width; i++)
                {
                    for (int j = 0; j < b.Height; j++)
                    {
                        b.GetPixel(i, j);
                    }


                }
                Console.Write(b.GetPixel(6, 7));

                //b.Save("Images\\Test002_1.bmp");
                //Process.Start("Images\\lenasortie3.bmp");
                //Process.Start("Images\\Test002_1.bmp");
                Console.ReadLine();
            
        }
        static void Main(string[] args)
        {
            MyImage a = new MyImage("./Images/lena.bmp");
            //Pixel2[,] Mat3 = a.negatif();
            //Pixel2[,] Mat4 = a.Rotation180();
            //Pixel2[,] Mat5 = a.Inverser_Image_axe_ordonnee();
            //Pixel2[,] Mat6 = a.Rotation90();
            //Pixel2[,] Mat7 = a.Inverser_Image_axe_abscisse();
            //Pixel2[,] Mat8 = a.Rotation270();
            Pixel2[,] Mat9 = a.Aggrandissement(2);

            Pixel2[,] Mat10 = a.rotate_with_angles(0);
            Pixel2[,] Mat11 = a.reduction(2) ;
            Pixel2[,] mat=new Pixel2[a.M.Matrix.GetLength(0), a.M.Matrix.GetLength(1)];
            byte[,] convolution = new byte[3,3]{ { 0, 1, 0 }, { 0, 0, 0 }, { 0, 0, 0 } } ;
            
            Pixel2[,] Mat12 = a.bleutage();

            for (int i=0;i< a.M.Matrix.GetLength(0); i++)
            {
                for(int j = 0; j < a.M.Matrix.GetLength(1); j++)
                {
                    mat[i,j] = new Pixel2(a.M.Matrix[i, j].Blue, a.M.Matrix[i, j].Green, a.M.Matrix[i, j].Red);
                }
            }

            a.From_Image_To_File(Mat9, "./Images/Test027.bmp");
            
            Console.ReadLine();
        }
    }
}
