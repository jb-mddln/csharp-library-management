namespace library_management.book
{
    /// <summary>
    /// Représente notre objet livre
    /// </summary>
    public class Book
    {
        public string Author { get; set; }

        public string Title { get; set; }

        public string Genre { get; set; }

        public string Collection { get; set; }

        public int YearOfPublication { get; set; }

        public int StockAvailable { get; set; }

        public int MaxStock { get; set; }

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        public Book()
        {
        }

        /// <summary>
        /// Constructeur permettant d'initialiser directement nos attributs avec les paramètres
        /// </summary>
        /// <param name="author"></param>
        /// <param name="title"></param>
        /// <param name="genre"></param>
        /// <param name="collection"></param>
        /// <param name="yearOfPublication"></param>
        /// <param name="stockAvailable"></param>
        /// <param name="maxStock"></param>
        public Book(string author, string title, string genre, string collection, int yearOfPublication, int stockAvailable, int maxStock)
        {
            this.Author = author;
            this.Title = title;
            this.Genre = genre;
            this.Collection = collection;
            this.YearOfPublication = yearOfPublication;
            this.StockAvailable = stockAvailable;
            this.MaxStock = maxStock;
        }


        /// <summary>
        /// Permets de vérifier si notre livre est toujours disponible à l'emprunt
        /// </summary>
        /// <returns>Retourne vrai si le livre est toujours disponible</returns>
        public bool IsAvailbale()
        {
            return this.StockAvailable > 0;
        }

        /// <summary>
        /// Retourne les informations du livre sous forme de chaine de caractères
        /// </summary>
        /// <returns>Infos du livre</returns>
        public string GetDetails()
        {
            return "Livre"
                + " Auteur: " + Author
                + " Titre: " + Title
                + " Genre: " + Genre
                + " Collection: " + Collection
                + " Date de publication: " + YearOfPublication
                + " Stock: " + StockAvailable + " / " + MaxStock;
        }
    }
}