using library_management.book;

namespace library_management.stock
{
    /// <summary>
    /// Récupère les données de nos livres et gère toutes les actions les concernant
    /// </summary>
    public class StockManager
    {
        public List<Book> Books { get; set; }

        /// <summary>
        /// Constructeur par défaut, récupère les infos via un fichier CSV
        /// </summary>
        public StockManager() 
        {
            // Initialise notre variable Books en tant que liste vide
            Books = new List<Book>();

            // Enregistre l'événement "ProcessExit" et déclenche la méthode "OnProcessExit" lors de la fermeture de notre console
            AppDomain.CurrentDomain.ProcessExit += OnProcessExit;

            if (File.Exists("books.csv"))
            {
                // Boucle foreach, chaque ligne de notre fichier books.csv représente un objet Book
                foreach (string line in File.ReadAllLines("books.csv"))
                {
                    // Les infos concernant un livre sont séparées par un ',' l'ordre des infos/données et le même que notre classe Book
                    string[] bookInfos = line.Split(",");

                    // Initialise un nouveau livre avec le constructeur par défaut
                    Book book = new Book();
                    /* On aurait pu utiliser le constructeur avec paramètres également
                     * Book book = new Book(int.Parse(bookInfos[0]), bookInfos[1], bookInfos[2], bookInfos[3], bookInfos[4], int.Parse(bookInfos[5]), int.Parse(bookInfos[6]), int.Parse(bookInfos[7])); */

                    // On passe nos infos dans les différents attributs
                    book.Id = int.Parse(bookInfos[0]);
                    book.Author = bookInfos[1];
                    book.Title = bookInfos[2];
                    book.Genre = bookInfos[3];
                    book.Collection = bookInfos[4];
                    book.YearOfPublication = int.Parse(bookInfos[5]);
                    book.StockAvailable = int.Parse(bookInfos[6]);
                    book.MaxStock = int.Parse(bookInfos[7]);

                    // Ajout du livre dans notre liste
                    Books.Add(book);
                }

                // Retour pour stopper l'exécution du code, pas besoin d'aller plus loin
                return;
            }

            // Aucune donnée sur les livres, ont créé notre fichier
            File.Create("books.csv");
        }

        /// <summary>
        /// Retourne les détails de tous les livres encore disponibles à l'emprunt sous forme de chaine de caractères
        /// </summary>
        /// <returns>Les détails de tous les livres encore disponibles à l'emprunt</returns>
        public string GetAvailableBooks()
        {
            // Utilisation de Linq Where avec la condition de ne prendre que les livres toujours disponibles
            // Utilisation de string.Join pour joindre notre liste de détails et ajouter deux retours à la ligne pour la clarté lors de l'affichage
            return string.Join("\n\n", Books.Where(book => book.IsAvailbale()).Select(book => book.GetDetails()));
        }

        /// <summary>
        /// Retourne les détails de tous les livres indisponibles à l'emprunt sous forme de chaine de caractères
        /// </summary>
        /// <returns>Les détails de tous les livres indisponibles à l'emprunt</returns>
        public string GetTakenBooks()
        {
            // Utilisation de Linq Where avec la condition de ne prendre que les livres toujours disponibles
            // Utilisation de string.Join pour joindre notre liste de détails et ajouter deux retours à la ligne pour la clarté lors de l'affichage
            return string.Join("\n\n", Books.Where(book => book.IsAvailbale()).Select(book => book.GetDetails()));
        }

        /// <summary>
        /// Retourne les détails de tous les livres sous forme de chaine de caractères
        /// </summary>
        /// <returns>Les détails de tous les livres</returns>
        public string GetBooksDetails()
        {
            // Utilisation de Linq Select pour sélectionner et retourner les infos de chaque livre
            // Utilisation de string.Join pour joindre notre liste de détails et ajouter deux retours à la ligne pour la clarté lors de l'affichage
            return string.Join("\n\n", Books.Select(book => book.GetDetails()));

            /* Liste vide pour stocker nos string contenant les infos des livres
             * var booksDetails = new List<string>();
             * foreach(Book book in Books)
             * {
             * booksDetails.Add(book.GetDetails());
             * }            
             * return string.Join("\n\n", booksDetails); */
        }

        /// <summary>
        /// Sauvegarde la liste Books dans un fichier books.csv
        /// </summary>
        public void Save()
        {

        }

        /// <summary>
        /// Méthode de retour pour notre événement, se déclenche à la fermeture de notre console
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnProcessExit(object? sender, EventArgs e)
        {
            Save();
        }
    }
}
