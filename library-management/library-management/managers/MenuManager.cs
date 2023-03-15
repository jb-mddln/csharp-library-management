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
            DisplayMenu();
        }

        /// <summary>
        /// Affiche notre menu
        /// </summary>
        public void DisplayMenu()
        {
            Console.WriteLine(@"----
----
> Entrez '1, 2, 3, 4' pour sélectionner rapidement une option ...
----
1) Afficher tous les membres de la bibliothèque
2) Afficher tous les livres de la bibliothèque
3) Afficher tous les livres encore disponibles à l'emprunt
4) Afficher tous les livres indisponibles à l'emprunt
----
----
> Entrez 'livre' pour afficher le sous-menu de gestion des livres (add, edit, delete)
> Entrez 'membre' pour afficher le sous-menu de gestion des membres (add, edit, delete)");
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
                Console.WriteLine("> Erreur ligne vide ...");
                return;
            }

            // Efface le texte de notre console pour plus de clarté
            Console.Clear();

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

                Console.WriteLine("> Tapez sur 'Entrée' pour revenir au menu principal");

                // Récupère la touche pressée par l'utilisateur dans la console, tant que ce n'est pas la touche 'Entrée' on reste dans la boucle et on affiche le message
                while (Console.ReadKey().Key != ConsoleKey.Enter)
                {
                    // Console.Clear();
                    Console.WriteLine("\n> Tapez sur 'Entrée' pour revenir au menu principal");
                }

                // La touche pressée est égale à la touche entrée on a quitté la boucle alors on affiche le menu
                Console.Clear();
                DisplayMenu();

                // Retour pour stopper l'exécution du code, pas besoin d'aller plus loin notre condition est remplie
                return;
            }

            // Condition if, si notre ligne de texte entrée par l'utilisateur contient le mot 'livre'
            // Usage de ToLower pour passer la ligne en minuscule et gère les mots entrés en majuscule/miniscule par l'utilisateur
            if (line.ToLower().Contains("livre"))
            {
                // Sous-menu livre

                // Retour pour stopper l'exécution du code, pas besoin d'aller plus loin notre condition est remplie
                return;
            }
        }
    }
}