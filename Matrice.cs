namespace PROJET_INFO_PUGET_Camille_PUVIKARAN_Thanujan
{
    /// <summary>
    /// No comments because it's obvious
    /// </summary>
    public class Matrice
    {
        // attributes
        private Pixel2[,] matrix;

        // constructor
        public Matrice(Pixel2[,] matrix)
        {
            this.matrix = matrix;
        }

        // properties
        public Pixel2[,] Matrix
        {
            get => this.matrix;
            set
            {
                matrix = value;
            }
        }
    }
}
