using System;

namespace PROJET_INFO_PUGET_Camille_PUVIKARAN_Thanujan
{
    /// <summary>
    /// The code below is for the mandelbrot set, newton set and julia set
    /// The code is easily understandable
    /// </summary>
    public class Complexe
    {
        double pr;//partie reelle
        double pi;//partie imaginaire


        public Complexe(double pr, double pi)
        {
            this.pr = pr;
            this.pi = pi;
        }

        public double Pr
        {
            get => pr;
            set
            {
                this.pr = value;
            }
        }
        public double Pi
        {
            get => this.pi;
            set => this.pi = value;
        }
        public double Module()
        {
            return (double)(Math.Sqrt((Math.Pow(this.pr, 2)) + (Math.Pow(this.pi, 2))));
        }
        public double Argument()
        {
            double norme = Module();
            double cos = pr / norme;
            double arcos = (double)Math.Acos(cos);
            return arcos;
        }
        public Complexe Somme(Complexe z)
        {
            Complexe a = new Complexe(this.pr + z.pr, this.pi + z.pi);
            return a;
        }
        public Complexe Somme_reel(double k)
        {
            Complexe a = new Complexe(k + this.pr, this.pi);
            return a;
        }
        public Complexe Somme_Imaginaire(float k)
        {
            Complexe a = new Complexe(this.pr, k + this.Pi);
            return a;
        }
        public Complexe Multiplication(Complexe z)
        {
            Complexe a = new Complexe(this.pr * z.pr - this.pi * z.pi, this.pr * z.pi + this.pi * z.pr);
            return a;
        }
        public Complexe Carre()
        {
            Complexe a = new Complexe(this.pr * this.pr - this.pi * this.pi, 2 * this.pi * this.pr);
            return a;
        }
        public Complexe Cube()
        {
            Complexe a = new Complexe(Math.Pow(this.Pr, 3) - 3 * this.Pr * Math.Pow(this.pi, 2), 3 * this.Pi * Math.Pow(this.pr, 2) - Math.Pow(this.Pi, 3));
            return a;
        }
        public static Complexe somme(Complexe a, Complexe b)
        {
            Complexe h = new Complexe(a.pr + b.pr, a.pi + b.pi);
            return h;
        }
        public Complexe multiplication_constante(float k)
        {
            Complexe a = new Complexe(k * this.Pr, k * this.Pi);
            return a;
        }
        public Complexe division(Complexe a)
        {
            Complexe b = new Complexe((this.pr * a.pr - this.pi * a.pi) / (Math.Pow(a.Module(), 2)), (this.pr * a.pi + this.pi * a.pr) / (Math.Pow(a.Module(), 2)));
            return b;
        }
        public Complexe Negatif()
        {
            Complexe a = new Complexe(-this.Pr, -this.Pi);
            return a;
        }
        public static Complexe Division(Complexe a, Complexe b)
        {
            Complexe az = new Complexe((a.pr * b.pr - a.pi * b.pi) / (Math.Pow(b.Module(), 2)), (a.pr * b.pi + a.pi * b.pr) / (Math.Pow(b.Module(), 2)));
            return az;
        }
    }
}
