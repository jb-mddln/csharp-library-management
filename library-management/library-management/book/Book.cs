namespace library_management.book
{
    /// <summary>
    /// Représente notre objet livre
    /// </summary>
    public class Book
    {
        public string Author { get; set; }

        public string Genre { get; set; }

        public string Collection { get; set; }

        public int YearOfPublication { get; set; }

        public int StockAvailable { get; set; }

        /// <summary>
        /// Permets de vérifier si notre livre est toujours disponible à l'emprunt
        /// </summary>
        /// <returns>Retourne vrai si le stock est supérieur à 0</returns>
        public bool IsAvailbale()
        {
            return this.StockAvailable > 0;
        }
    }
}
