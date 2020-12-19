using System;

namespace PROJET_INFO_PUGET_Camille_PUVIKARAN_Thanujan
{
    class Program
    {
        /*/// <summary>
        /// function which contains the first batch of test of bitmap
        /// it is here to verify the result of every functions
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

        //}*/



        /// <summary>
        /// Function which gives an approximation of pi within a gap 
        /// </summary>
        /// <param name="taille"></param>
        /// <returns></returns>
        static Pixel2[,] Approximation_PI(int taille)
        {
            int compteur_noir = 0;
            int compteur_noir_cercle = 0;
            Console.Write("Donner le nombre de points voulu : ");
            int nombre_iterations = verification(Console.ReadLine());
            int px = taille / 2;
            Pixel2[,] mat = new Pixel2[taille, taille];
            Random r = new Random();
            int compteur = 1;
            for (int i = 0; i < 100; i++)
            {
                while (compteur <= nombre_iterations)
                {
                    compteur++;
                    int x = r.Next(0, taille);
                    int y = r.Next(0, taille);
                    if (Math.Pow(px - x, 2) + Math.Pow(px - y, 2) <= 40000)
                    {
                        mat[x, y] = new Pixel2(0, 0, 0);
                        compteur_noir_cercle++;
                    }
                    else
                    {
                        mat[x, y] = new Pixel2(255, 255, 255);
                    }
                    compteur_noir++;

                }
                compteur = 0;
            }
            for (int i = 0; i < taille; i++)
            {
                for (int j = 0; j < taille; j++)
                {
                    if (i == 0 || j == 0 || i == taille - 1 || j == taille - 1) mat[i, j] = new Pixel2(255, 0, 0);
                    else
                    {
                        if (mat[i, j] == null)
                        {
                            mat[i, j] = new Pixel2(255, 255, 255);
                        }

                    }
                }
            }
            float a = 4 * ((float)(compteur_noir_cercle) / (compteur_noir));
            Console.WriteLine("Nombre de points noirs total : " + compteur_noir);
            Console.WriteLine("Nombre de points noirs dans cercle : " + compteur_noir_cercle);
            Console.WriteLine("PI fist estimation : " + a);
            return mat;

        }
        static void Main(string[] args)
        {
            MyImage a = new MyImage("./Images/lena.bmp");
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
                            int zoom = verification(Console.ReadLine());
                            Pixel2[,] Mat9 = a.Aggrandissement(zoom);
                            mat = Mat9;
                            a.From_Image_To_File(mat, "./Images/TEST202.bmp");
                            break;
                        case 8:
                            Console.Write("Donner la valeur de zoom attendu, si voulez une image de taille divisé par 2 mettez 2 : ");
                            int zoom2 = verification(Console.ReadLine());
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
                            int value = verification(Console.ReadLine());
                            Pixel2[,] Mat13 = a.Rotation(value);
                            mat = Mat13;
                            a.From_Image_To_File(mat, "./Images/TEST202.bmp");
                            break;
                        case 11:
                            Console.WriteLine("Donner la taille de la matrice de convolution : ");
                            int taille = verification(Console.ReadLine());
                            int[,] convolution = new int[taille, taille];
                            Console.WriteLine("donner les valeurs de cette matrice :");
                            for (int i = 0; i < convolution.GetLength(0); i++)
                            {
                                for (int j = 0; j < convolution.GetLength(1); j++)
                                {
                                    convolution[i, j] = Convert.ToInt32(Console.ReadLine());
                                }
                            }
                            Pixel2[,] Mat19 = a.Floutage_etc(convolution);
                            mat = Mat19;
                            a.From_Image_To_File(mat, "./Images/Detection_de_bord.bmp");
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
                                     + "Fractale de Newton pour le polynôme P(z)=z^3-1 , taper 5:\n"
                                     + "approximation de pi , taper 6:\n"
                                     + "Fractale de Koch, qui ne fonctionne pas , taper 7:\n"
                                     + "Sélectionnez l'exercice désiré ");
                    int exo = SaisieNombre();
                    switch (exo)
                    {
                        #region
                        case 1:
                            Console.Write("Donner le nombre d'itération avec un minimum de 100 iterations ");
                            int nombre_iteration = verification(Console.ReadLine());
                            Pixel2[,] Mat16 = sup.Mandelbrot(nombre_iteration);
                            Console.WriteLine();
                            sup.From_fractale_toFile(Mat16, "./Images/TEST200.bmp");
                            break;
                        case 2:
                            Console.Write("Donner le nombre d'itération, 100 étant la meilleur valeur : ");
                            int nombre_iteration2 = verification(Console.ReadLine());
                            Console.Write("Donner la partie reelle du complexe, une bonne valeur est -0,7927 : ");
                            double pr = Convert.ToDouble(Console.ReadLine());
                            Console.WriteLine();
                            Console.Write("Donner la partie imaginaire du complexe, une bonne valeur est 0,1609  : ");
                            double pi = verification2(Console.ReadLine());
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

                            MyImage b = new MyImage("./Images/fairy3.bmp");
                            Pixel2[,] cache = sup.codage(a.M.Matrix, b.M.Matrix);
                            Pixel2[,] r = sup.decodage(cache);
                            Pixel2[,] r2 = sup.decodage(cache);
                            sup.From_fractale_toFile(cache, "./Images/TESTcache.bmp");
                            sup.From_fractale_toFile(r, "./Images/TESTretour1.bmp");
                            sup.From_fractale_toFile(r2, "./Images/TESTretour2.bmp");
                            break;
                        case 5:
                            Console.Write("Donner la valeur minimale sur l'axe des abscisses, sachant qu'une bonne valeur est -1,33, posX =");
                            double posX = verification2(Console.ReadLine());
                            Console.Write("Donner la valeur minimale sur l'axe des abscisses, sachant qu'une bonne valeur est -1,5, posY =");
                            double posY = verification2(Console.ReadLine());
                            Console.Write("Donne le nombre d'itération maximal, une bonne valeur est 100, n=");
                            int n = verification(Console.ReadLine());
                            Console.Write("Donne la taille de la matrice, une bonne valeur est 500, taille=");
                            int taille = verification(Console.ReadLine());
                            Pixel2[,] Mat18 = sup.Fractale_Newton(posX, posY, n, taille);
                            sup.From_fractale_toFile(Mat18, "./Images/TESTNewton.bmp");
                            break;
                        case 6:
                            Pixel2[,] Mat19 = Approximation_PI(400);
                            sup.From_fractale_toFile(Mat19, "./Images/TESTPI_4.bmp");
                            break;
                        case 7:
                            Console.WriteLine("La fractale de koch qui suit ne fonctionne pas car il nous manque des notions de calcul, si vous avez des idées veuillez nous en faire part :");
                            Console.Write("Position de début de ligne, x1=");
                            int x1 = verification(Console.ReadLine());
                            Console.Write("y1=");
                            int y1 = verification(Console.ReadLine());
                            Console.Write("Position de fin de ligne, x2=");
                            int x2 = verification(Console.ReadLine());
                            Console.Write("y2=");
                            int y2 = verification(Console.ReadLine());
                            int[] debut = new int[2] { x1, y1 };
                            int[] fin = new int[2] { x2, y2 };
                            Segment L = new Segment(debut, fin);
                            Pixel2[,] Mat21 = L.traçage(debut, fin);
                            Segment l = L.Division();
                            int[] tab = L.calcul_position(l.Point_debut, l.Point_fin);
                            Console.WriteLine("tab[0]=" + tab[0]);
                            Console.WriteLine("tab[1]=" + tab[1]);
                            Console.WriteLine("L=" + L.L);
                            //Console.WriteLine();
                            //Console.WriteLine("Division=" + L.Division().Point_debut[0] + " ; "+ L.Division().Point_debut[1]);
                            //Console.Write("Division2=" + L.Division().Point_fin[0] + " ; "+ L.Division().Point_fin[1]);
                            //Console.WriteLine();
                            //Console.Write("l.Point_debut[0] =" + l.Point_debut[0] + " ; ");
                            //Console.WriteLine("l.Point_debut[1] =" + l.Point_debut[1]);
                            //Console.Write("l.Point_fin[0] =" + l.Point_fin[0] + " ; ");
                            //Console.WriteLine("l.Point_fin[1] =" + l.Point_fin[1]);
                            Mat21 = L.effaçage(l.Point_debut, l.Point_fin);
                            sup.From_fractale_toFile(Mat21, "./Images/TESTTraçage.bmp");
                            break;

                            #endregion
                    }
                    Console.WriteLine();
                    Console.WriteLine("Tapez Escape pour sortir ou un numero d exo");
                    cki = Console.ReadKey();
                    while (cki.Key != ConsoleKey.Escape) ;
                }

                else if (a.Flag_QR_code != false)
                {
                    //QR code function
                    Program.LancementQRCode();
                }
            }
            Console.ReadLine();
        }
        static int SaisieNombre()
        {
            int saisie = verification(Console.ReadLine());
            return saisie;
        }
        /// <summary>
        /// verify wheter the value is an int
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        static int verification(string value)
        {
            int val = 0;
            bool flag = false;
            try
            {
                val = Convert.ToInt32(value);
            }
            catch (Exception)
            {
                Console.WriteLine("une erreur s'est produite, recommencez ");
                flag = true;
            }
            finally
            {
                if (flag == true)
                {
                    Console.Write("Donnez à nouveau une valeur, cela doit être un int : ");
                    value = Console.ReadLine();
                    val = verification(value);
                }
            }
            return val;

        }
        /// <summary>
        /// verify wheter it's a double 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        static double verification2(string value)
        {
            double val = 0;
            bool flag = false;
            try
            {
                val = Convert.ToDouble(value);
            }
            catch (Exception)
            {
                Console.WriteLine("une erreur s'est produite, recommencez ");
                flag = true;
            }
            finally
            {
                if (flag == true)
                {
                    Console.Write("Donnez à nouveau une valeur, cela doit être un double : ");
                    value = Console.ReadLine();
                    val = verification2(value);
                }
            }
            return val;
        }
        /// <summary>
        /// verify wheter it's a boolean
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        static bool verification3(string value)
        {
            bool val = false;
            bool flag = false;
            try
            {
                val = Convert.ToBoolean(value);
            }
            catch (Exception)
            {
                Console.WriteLine("une erreur s'est produite, recommencez ");
                flag = true;
            }
            finally
            {
                if (flag == true)
                {
                    Console.Write("Donnez à nouveau une valeur, cela doit être un boolean : ");
                    value = Console.ReadLine();
                    val = verification3(value);
                }
            }
            return val;

        }
        /// <summary>
        /// This method launches the creation of a QR Code.
        /// </summary>
        static public void LancementQRCode()
        {
            Fractale_image_cache testoune = new Fractale_image_cache();

            Console.Write("Entrez votre message :");
            string message = Console.ReadLine();

            while (message.Length > 25)
            {
                Console.WriteLine();
                Console.Write("C'est trop long ! Entrez votre message :");
                message = Console.ReadLine();
            }

            QRCode Test1QRCode = new QRCode("HELLO WORLD");
            Pixel2[,] recupTest = Test1QRCode.TestFonctionnement();

            Pixel2[,] recupTemp = new Pixel2[recupTest.GetLength(0), recupTest.GetLength(1)];

            int a = recupTest.GetLength(0);
            for (int i = 0; i < recupTest.GetLength(0); i++)
            {
                for (int j = 0; j < recupTest.GetLength(1); j++)
                {
                    recupTemp[i, j] = new Pixel2(recupTest[a - 1 - i, j].Blue, recupTest[a - 1 - i, j].Green, recupTest[a - 1 - i, j].Red);
                }
            }

            testoune.From_fractale_toFile(recupTemp, "./Images/TEST211.bmp");

            Console.ReadKey();
        }
    }
}
