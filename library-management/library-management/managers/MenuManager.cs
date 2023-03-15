namespace library_management.managers
{
    /// <summary>
    /// Gère la partie menu de notre console et l'affichage des données
    /// </summary>
    public class MenuManager
    {
        /// <summary>
        /// Constructeur par défaut, affiche notre menu directement lors de son initialisation
        /// </summary>
        public MenuManager()
        {
            Console.WriteLine(@"
> Entrez '1, 2, 3, 4' pour sélectionner rapidement une option ...
----
1) Afficher tous les membres de la bibliothèque
2) Afficher tous les livres de la bibliothèque
3) Afficher tous les livres encore disponibles à l'emprunt
4) Afficher tous les livres indisponibles à l'emprunt
----
----
> Type 'book' to display the sub-menu for book (add, edit, delete)
> Type 'borrow' to display the sub-menu for borrowing book (add, edit, delete)");
        }

        /// <summary>
        /// Gère notre menu, son affichage, les différentes options et actions sur nos managers
        /// </summary>
        /// <param name="line"></param>
        /// <param name="stock"></param>
        public void HandleMenu(string line, MemberManager member, StockManager stock)
        {
            if (string.IsNullOrEmpty(line))
            {
                Console.WriteLine("> Entrez d'abord '1, 2, 3, 4' pour sélectionner une option ...");
                return;
            }

            // Gère la sélection d'options rapide
            if (line.Length == 1)
            {
                switch (line[0])
                {
                    case '1':
                        Console.WriteLine("> Liste des membres de la bibliothèque: \n");
                        Console.WriteLine(member.GetMembersDetails() + "\n");
                        break;
                    case '2':
                        Console.WriteLine("> Liste des livres de la bibliothèque: \n");
                        Console.WriteLine(stock.GetBooksDetails() + "\n");
                        break;
                    case '3':
                        Console.WriteLine("> Liste des livres encore disponibles à l'emprunt: \n");
                        Console.WriteLine(stock.GetAvailableBooks());
                        break;
                    case '4':
                        Console.WriteLine("> Liste des livres indisponibles à l'emprunt: \n");
                        Console.WriteLine(stock.GetNotAvailableBooks());
                        break;
                    default:
                        Console.WriteLine("> Entrez d'abord une option valide, les options valides sont '1, 2, 3, 4' ...");
                        break;
                }

                // Retour pour stopper l'exécution du code, pas besoin d'aller plus loin notre condition est remplie
                return;
            }

            if (line.ToLower().Contains("book"))
            {
                // Sous-menu livre
            }
        }
    }
}