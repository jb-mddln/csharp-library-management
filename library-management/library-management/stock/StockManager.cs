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
                     * Book book = new Book(bookInfos[0], bookInfos[1], bookInfos[2], bookInfos[3], int.Parse(bookInfos[4]), int.Parse(bookInfos[5])); */

                    // On passe nos infos dans les différents attributs
                    book.Author = bookInfos[0];
                    book.Title = bookInfos[1];
                    book.Genre = bookInfos[2];
                    book.Collection = bookInfos[3];
                    book.YearOfPublication = int.Parse(bookInfos[4]);
                    book.StockAvailable = int.Parse(bookInfos[5]);
                    book.MaxStock = int.Parse(bookInfos[6]);

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
        /// Sauvegarde la liste Books dans un fichier books.csv
        /// </summary>
        public void Save()
        {

        }
    }
}
