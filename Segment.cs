using System;

namespace PROJET_INFO_PUGET_Camille_PUVIKARAN_Thanujan
{
    public class Segment
    {
        private int[] point_debut = new int[2];//point_debut[0] correspond à la position sur l'axe des abscisses et point_debut[1] correspond à la position sur l'axe des ordonnées  
        private int[] point_fin = new int[2];//point_fin[0] correspond à la position sur l'axe des abscisses et point_fin[1] correspond à la position sur l'axe des ordonnées  
        private double[] step = new double[2];
        private int length;
        private Pixel2[,] graph;
        static bool flag = false;
        private double l;//taille
        public Segment(int[] point_debut, int[] point_fin)
        {
            this.point_debut = point_debut;
            this.point_fin = point_fin;
            this.step = new double[] { (this.point_debut[0] + this.point_fin[0]) / 3, (this.point_debut[1] + this.point_fin[1]) / 3 };
            if (flag == false)
            {
                Console.Write("Donner la taille de la matrice, length=");
                this.length = Convert.ToInt32(Console.ReadLine());
                this.graph = new Pixel2[length, length];
                flag = true;
            }
            this.L = Distance(point_debut, point_fin);
        }
        public int[] Point_debut
        {
            get => this.point_debut;
            set => this.point_debut = value;
        }
        public double L
        {
            get => this.l;
            set => this.l = value;
        }
        public int[] Point_fin
        {
            get => this.point_fin;
            set => this.point_fin = value;
        }
        public double[] Step
        {
            get => this.step;
            set => this.step = value;
        }
        public Pixel2[,] Graph
        {
            get => this.graph;
            set => this.graph = value;
        }
        public double Distance(int[] pos1, int[] pos2)
        {
            return Math.Sqrt((pos1[0] - pos2[0]) * (pos1[0] - pos2[0]) + (pos1[1] - pos2[1]) * (pos1[1] - pos2[1]));
        }
        public Segment Division()
        {
            int[] pos1 = new int[2] { (int)this.step[0], (int)this.step[1] };
            int[] pos2 = new int[2] { (int)(2 * this.step[0]), (int)(2 * this.step[1]) };
            //Console.WriteLine("début="+pos1[0]+";"+pos1[1]);
            //Console.Write("fin="+pos2[0]+";"+pos2[1]);
            Segment part = new Segment(pos1, pos2);
            return part;

        }
        public Pixel2[,] traçage(int[] pos1, int[] pos2)
        {
            int x1 = pos1[0];
            int y1 = pos1[1];
            int x2 = pos2[0];
            int y2 = pos2[1];
            for (int i = 0; i < graph.GetLength(0); i++)
            {
                for (int j = 0; j < graph.GetLength(1); j++)
                {
                    double value = ((double)(y2 - y1) / (x2 - x1)) * i + y1 - ((double)(y2 - y1) / (x2 - x1)) * x1;
                    if (i == 0 || j == 0 || i == graph.GetLength(0) - 1 || j == graph.GetLength(1) - 1) graph[i, j] = new Pixel2(0, 0, 0);
                    if (graph[i, j] == null)
                    {
                        if (Math.Truncate(value) == j)
                        {
                            if ((x1 <= i && x2 >= i) || (x1 >= i && x2 <= i))
                            {
                                graph[i, j] = new Pixel2(0, 0, 0);
                            }
                            else
                            {
                                graph[i, j] = new Pixel2(255, 255, 255);
                            }
                        }
                        else
                        {
                            graph[i, j] = new Pixel2(255, 255, 255);
                        }
                    }
                }
            }
            return graph;
        }
        public Pixel2[,] effaçage(int[] pos1, int[] pos2)
        {
            int x1 = pos1[0];
            int y1 = pos1[1];
            int x2 = pos2[0];
            int y2 = pos2[1];
            for (int i = 0; i < graph.GetLength(0); i++)
            {
                for (int j = 0; j < graph.GetLength(1); j++)
                {
                    double value = ((double)(y2 - y1) / (x2 - x1)) * i + y1 - ((double)(y2 - y1) / (x2 - x1)) * x1;
                    if (Math.Truncate(value) == j)
                    {
                        if ((x1 <= i && x2 >= i) || (x1 >= i && x2 <= i))
                        {
                            graph[i, j] = new Pixel2(255, 255, 255);
                        }
                    }

                }
            }
            return graph;
        }
        public int[] calcul_position(int[] pos1, int[] pos2)
        {
            int[] position = new int[2];
            for (int i = 0; i < graph.GetLength(0); i++)
            {
                for (int j = 0; j < graph.GetLength(1); j++)
                {
                    int[] tab = new int[2] { i, j };
                    if (i == 243 && j == 95) Console.WriteLine("distance=" + Distance(tab, pos1));
                    if ((Distance(tab, pos1) - ((double)L / 3) <= 0.1) && (Distance(tab, pos2) - ((double)L / 3) <= 0.1))
                    {
                        position[0] = i;
                        position[1] = j;
                    }
                }
            }

            return position;
        }
        public Pixel2[,] Koch()
        {
            return graph;
        }
    }
}
