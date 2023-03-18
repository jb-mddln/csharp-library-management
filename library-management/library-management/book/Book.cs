using library_management.borrow;

namespace library_management.book
{
    /// <summary>
    /// Représente notre objet livre
    /// </summary>
    public class Book
    {
        /// <summary>
        /// Id, utile pour identifier clairement notre livre lors des différentes opérations
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Titre
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Auteur
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// Genre
        /// </summary>
        public string Genre { get; set; }

        /// <summary>
        /// Collection
        /// </summary>
        public string Collection { get; set; }

        /// <summary>
        /// Année de publication
        /// </summary>
        public int YearOfPublication { get; set; }

        /// <summary>
        /// Stock dispnible
        /// </summary>
        public int StockAvailable { get; set; }

        /// <summary>
        /// Stock maximum
        /// </summary>
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
        /// <param name="id"></param>
        /// <param name="title"></param>
        /// <param name="author"></param>
        /// <param name="genre"></param>
        /// <param name="collection"></param>
        /// <param name="yearOfPublication"></param>
        /// <param name="stockAvailable"></param>
        /// <param name="maxStock"></param>
        public Book(int id, string title, string author, string genre, string collection, int yearOfPublication, int stockAvailable, int maxStock)
        {
            this.Id = id;
            this.Title = title;
            this.Author = author;
            this.Genre = genre;
            this.Collection = collection;
            this.YearOfPublication = yearOfPublication;
            this.StockAvailable = stockAvailable;
            this.MaxStock = maxStock;
        }


        /// <summary>
        /// Permets de vérifier si notre livre est toujours disponible à l'emprunt
        /// </summary>
        /// <returns>Retourne vrai si le stock du livre est supérieur à 0</returns>
        public bool IsAvailbale()
        {
            return this.StockAvailable > 0;
        }

        /// <summary>
        /// Retourne uniquement l'id du livre et son titre sous forme de chaine de caractères
        /// </summary>
        /// <returns>Id et titre du livre</returns>
        public string GetIdAndName()
        {
            return this.Id + " " + this.Title;
        }

        /// <summary>
        /// Retourne les informations du livre sous forme de chaine de caractères
        /// </summary>
        /// <returns>Infos du livre</returns>
        public string GetDetails()
        {
            return "Id: " + this.Id
                + "\n" + "Titre: " + this.Title
                + "\n" + "Auteur: " + this.Author
                + "\n" + "Genre: " + this.Genre
                + "\n" + "Collection: " + this.Collection
                + "\n" + "Année de publication: " + this.YearOfPublication
                + "\n" + "Stock: " + this.StockAvailable + " / " + this.MaxStock
                + "\n" + "Disponible: " + (IsAvailbale() ? "Oui" : "Non");
        }

        /// <summary>
        /// Retourne notre livre au format CSV (comma-separated values) sous forme de chaine de caractères
        /// </summary>
        /// <returns>Infos du livre au format CSV</returns>
        public string GetCSV()
        {
            return this.Id + ","
                + this.Title + ","
                + this.Author + ","
                + this.Genre + ","
                + this.Collection + ","
                + this.YearOfPublication + ","
                + this.StockAvailable + ","
                + this.MaxStock;
        }
    }
}