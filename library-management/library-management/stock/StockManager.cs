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
            if (File.Exists("books.csv"))
            {
                // Code de lecture

                // Retour pour stopper l'exécution du code, pas besoin d'aller plus loin
                return;
            }

            // Aucune donnée sur les livres, ont créé notre fichier
            File.Create("books.csv");

            // Initialise notre variable Books en tant que liste vide
            Books = new List<Book>();
        }
    }
}
