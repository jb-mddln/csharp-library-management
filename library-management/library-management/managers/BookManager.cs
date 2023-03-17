using library_management.book;

namespace library_management.managers
{
    /// <summary>
    /// Récupère les données de nos livres et gère toutes les actions les concernant
    /// </summary>
    public class BookManager
    {
        public List<Book> Books { get; set; }

        /// <summary>
        /// Constructeur par défaut, récupère les infos via un fichier CSV
        /// </summary>
        public BookManager() 
        {
            // Initialise notre variable Books en tant que liste vide
            Books = new List<Book>();

            // Enregistre l'événement "ProcessExit" et déclenche la méthode "OnProcessExit" lors de la fermeture de notre console
            AppDomain.CurrentDomain.ProcessExit += OnProcessExit;

            // Charge notre fichier CSV
            Load();
        }

        /// <summary>
        /// Retourne une instance de notre objet livre contenu dans notre liste de livres
        /// </summary>
        /// <param name="bookIdString">Id du livre</param>
        /// <returns>Null ou notre objet livre</returns>
        public Book? TryGetBook(string bookIdString)
        {
            // N'est pas un integer valide retourne false on retourne null
            if (!int.TryParse(bookIdString, out int bookId))
                return null;

            // Linq Any, notre liste Books ne contient pas de livre avec l'id on retourne null
            if (!Books.Any(book => book.Id == bookId))
                return null;

            // Retourne 'null' si aucun livre trouvé, mais ne dois pas arrivé grace a notre condition avant
            return Books.FirstOrDefault(book => book.Id == bookId);
        }

        /// <summary>
        /// Essaye d'ajouter un livre à notre liste de livres
        /// </summary>
        /// <param name="parameters">Paramètres entrés par l'utilisateur</param>
        /// <returns>Vrai ou faux selon si l'opération d'ajout est un succès ou non</returns>
        public bool TryAddBook(string[] parameters)
        {
            Book newBook = new Book();

            string title = parameters[0];
            string author = parameters[1];
            string genre = parameters[2];
            string collection = parameters[3];
            string yearOfPublicationString = parameters[4];
            string maxStockString = parameters[5];

            // Titre 'null' ou vide on retourne false et stop l'ajout (ne dois normalement jamais arrivée)
            if (string.IsNullOrEmpty(title))
                return false;

            // Auteur 'null' ou vide on retourne false et stop l'ajout (ne dois normalement jamais arrivée)
            if (string.IsNullOrEmpty(author))
                return false;

            // Genre 'null' ou vide on retourne false et stop l'ajout (ne dois normalement jamais arrivée)
            if (string.IsNullOrEmpty(genre))
                return false;

            // Collection 'null' ou vide on retourne false et stop l'ajout (ne dois normalement jamais arrivée)
            if (string.IsNullOrEmpty(collection))
                return false;

            // Année de publication n'est pas un integer valide retourne false on stop l'ajout
            if (!int.TryParse(yearOfPublicationString, out int yearOfPublication))
                return false;

            // Stock n'est pas un integer valide retourne false on stop l'ajout
            if (!int.TryParse(maxStockString, out int maxStock))
                return false;

            // La date de publication ou le stock ne peut être inférieur ou égale à 0 on stop l'ajout
            if (yearOfPublication <= 0 || maxStock <= 0) 
                return false;

            // Utilisation de Linq Select pour récupérer l'id max de notre liste Books
            int id = Books.Select(book => book.Id).Max();

            newBook.Id = id + 1; // Id max de notre liste + 1 pour un id libre
            newBook.Title = title;
            newBook.Author= author;
            newBook.Genre = genre;
            newBook.Collection = collection;
            newBook.YearOfPublication = yearOfPublication;
            newBook.StockAvailable = maxStock;
            newBook.MaxStock = maxStock;

            // Linq Any, notre liste Books contient déjà un livre avec l'id générer on stop l'ajout retourne false (ne dois normalement jamais arrivée)
            if (Books.Any(book => book.Id == newBook.Id)) 
                return false;

            // Ajout du nouveau livre dans notre liste
            Books.Add(newBook);
            // Retourne true succès
            return true;
        }

        /// <summary>
        /// Modifie l'instance de notre livre contenu dans notre liste Books
        /// </summary>
        /// <param name="bookToEdit">Instance de notre objet livre</param>
        /// <param name="parameters">Paramètres entrés par l'utilisateur</param>
        /// <returns>Vrai ou faux selon si l'opération de modification est un succès ou non</returns>
        public bool TryEditBook(Book bookToEdit, string[] parameters)
        {
            string title = parameters[0];
            string author = parameters[1];
            string genre = parameters[2];
            string collection = parameters[3];
            string yearOfPublicationString = parameters[4];
            string maxStockString = parameters[5];

            // Titre n'est pas 'null' ou vide on modifie donc le titre
            if (!string.IsNullOrEmpty(title))
            {
                bookToEdit.Title = title;
            }

            // Auteur n'est pas 'null' ou vide on modifie donc l'auteur
            if (!string.IsNullOrEmpty(author))
            {
                bookToEdit.Author = author;
            }


            // Genre n'est pas 'null' ou vide on modifie donc le genre
            if (!string.IsNullOrEmpty(genre))
            {
                bookToEdit.Genre = genre;
            }

            // Collection n'est pas 'null' ou vide on modifie donc la collection
            if (!string.IsNullOrEmpty(collection))
            {
                bookToEdit.Collection = collection;
            }

            // Année de publication n'est pas 'null' ou vide on modifie donc la collection
            if (!string.IsNullOrEmpty(yearOfPublicationString))
            {
                // Année de publication n'est pas un integer valide retourne false on stop la modification
                if (!int.TryParse(yearOfPublicationString, out int yearOfPublication))
                    return false;

                // L'année de publication ne peut être inférieur ou égale à 0 on stop la modification
                if (yearOfPublication <= 0)
                    return false;

                bookToEdit.YearOfPublication = yearOfPublication;
            }

            // Stock n'est pas 'null' ou vide on modifie donc la collection
            if (!string.IsNullOrEmpty(maxStockString))
            {
                // Stock n'est pas un integer valide retourne false on stop la modification
                if (!int.TryParse(maxStockString, out int maxStock))
                    return false;

                // Stock ne peut être inférieur ou égale à 0 on stop la modification
                if (maxStock <= 0)
                    return false;

                bookToEdit.MaxStock = maxStock;
            }

            // Retourne true succès
            return true;
        }

        /// <summary>
        /// Essaye de supprimer un livre à notre liste de livres
        /// </summary>
        /// <param name="bookIdString">Id du livre</param>
        /// <returns>Vrai ou faux selon si l'opération de suppression est un succès ou non</returns>
        public bool TryDeleteBook(string bookIdString)
        {
            // bookIdString n'est pas un integer valide retourne false on stop la suppression
            if (!int.TryParse(bookIdString, out int bookId))
                return false;

            // Linq Any, notre liste Books ne contient pas de livre avec l'id on stop la suppression retourne false
            if (!Books.Any(book => book.Id == bookId))
                return false;

            Books.RemoveAll(book => book.Id == bookId);

            return true;
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
        public string GetNotAvailableBooks()
        {
            // Utilisation de Linq Where avec la condition de ne prendre que les livres toujours disponibles
            // Utilisation de string.Join pour joindre notre liste de détails et ajouter deux retours à la ligne pour la clarté lors de l'affichage
            return string.Join("\n\n", Books.Where(book => !book.IsAvailbale()).Select(book => book.GetDetails()));
        }

        /// <summary>
        /// Retourne l'id et titre de tous les livres sous forme de chaine de caractères
        /// </summary>
        /// <returns>L'id et titre de tous les livres</returns>
        public string GetBooksIdAndName()
        {
            return string.Join("", Books.Select(book => book.GetIdAndName() + "\n"));
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

        public string GetBooksDetailsById(List<int> bookIds)
        {
            return string.Join("\n", Books.Where(book => bookIds.Contains(book.Id)).Select(book => book.GetIdAndName()));
        }

        /// <summary>
        /// Charge nos données depuis le CSV si disponible, sinon créer le CSV vide
        /// </summary>
        public void Load()
        {
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
                    book.Title = bookInfos[1];
                    book.Author = bookInfos[2];
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
        /// Sauvegarde la liste Books dans un fichier books.csv
        /// </summary>
        public void Save()
        {
            // Linq, Select
            File.WriteAllLines("books.csv", Books.Select(book => book.GetCSV()));

            /* using StreamWriter sw = new StreamWriter("books.csv");
             * foreach (Book book in Books)
             * {
             * sw.WriteLine(book.GetBookCSV());
             * } */
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
