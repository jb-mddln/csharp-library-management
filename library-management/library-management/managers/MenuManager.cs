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
        private void DisplayMenu()
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
> Entrez 'livre' pour afficher le sous-menu de gestion des livres (ajouter, supprimer, modifier)
> Entrez 'membre' pour afficher le sous-menu de gestion des membres (ajouter, supprimer, modifier)");
        }

        /// <summary>
        /// Affiche notre sous menu pour livre et membre
        /// </summary>
        private void DisplaySubMenu()
        {
            Console.WriteLine(@"----
> Entrez 'ajouter, supprimer, modifier' ...
----");
        }

        /// <summary>
        /// Demande à l'utilisateur de taper sur sa touche 'Entrée' pour retourner au menu principal
        /// </summary>
        private void DisplayAndHandleEnterKey()
        {
            Console.WriteLine("> Tapez sur 'Entrée' pour revenir au menu principal");

            // Boucle infinie, récupère la touche pressée par l'utilisateur dans la console tant que ce n'est pas la touche 'Entrée' on reste dans la boucle et on affiche le message
            while (Console.ReadKey().Key != ConsoleKey.Enter)
            {
                Console.WriteLine("\n> Tapez sur 'Entrée' pour revenir au menu principal");
            }

            // La touche pressée est égale à la touche entrée on a quitté la boucle alors on affiche le menu
            Console.Clear();
            DisplayMenu();
        }

        /// <summary>
        /// Gère notre menu, son affichage, les différentes options et actions sur nos managers
        /// </summary>
        /// <param name="line"></param>
        /// <param name="stock"></param>
        public void HandleMenu(string line, MemberManager member, StockManager stock)
        {
            // Condition if, si notre ligne est "null" ou vide alors on affiche un message d'erreur
            if (string.IsNullOrEmpty(line))
            {
                Console.WriteLine("> Erreur ligne vide ...");
                return;
            }

            // Efface le texte de notre console pour plus de clarté
            Console.Clear();

            // Condition if, si notre ligne ne contient qu'un caractère il s'agit surement d'une option rapide
            if (line.Length == 1)
            {
                // Gère la sélection d'options rapide, 1er caractère de notre ligne
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

                // Gestion de la touche 'Entrée' pour retourner au menu principale
                DisplayAndHandleEnterKey();

                // Retour pour stopper l'exécution du code, pas besoin d'aller plus loin notre condition est remplie
                return;
            }

            DisplaySubMenu();

            // Variable string? 'null', utile plus tard dans notre switch pour récupérer l'option secondaire de l'utilisateur
            string? subOption = null;
            bool exitSubMenuOption = false;

            // Découpe notre ligne de texte après chaque espace vide, sous forme de tableau Array
            string[] subMenu = line.Split(" ");
            // Switch uniquement sur notre 1er element string de notre tableau, ToLower pour gérer les mots entrés en majuscule/miniscule par l'utilisateur
            switch (subMenu[0].ToLower())
            {
                case "livre":
                    // Boucle while tant que la variable exitSubMenuOption est false, gère le texte entré par l'utilisateur dans notre sous menu
                    while (!exitSubMenuOption)
                    {
                        subOption = Console.ReadLine();

                        // Condition if, si notre ligne est "null" ou vide alors on affiche un message invitant l'utilisateur à taper une option valide
                        if (string.IsNullOrEmpty(subOption))
                        {
                            Console.WriteLine("> Entrez d'abord une option valide, les options valides sont 'ajouter, supprimer, modifier' ...");
                            // Indique de ne pas sortir de notre boucle, reviens au début de celle-ci et évite de continuer l'exécution avec le code plus bas
                            continue;
                        }

                        string[] subMenuOption = subOption.Split(" ");
                        switch (subMenuOption[0].ToLower())
                        {
                            case "ajouter":

                                string[] parameters = new string[6];

                                Console.WriteLine("> Entrez un nom de livre: ");
                                string? bookTitle = Console.ReadLine();

                                while (string.IsNullOrEmpty(bookTitle))
                                {
                                    Console.WriteLine("> Erreur le nom du livre ne peut pas être vide ...");
                                    Console.WriteLine("> Entrez un nom de livre: ");
                                    bookTitle = Console.ReadLine();
                                }

                                Console.WriteLine("> Entrez un nom d'Auteur: ");
                                string? authorName = Console.ReadLine();

                                while (string.IsNullOrEmpty(authorName))
                                {
                                    Console.WriteLine("> Erreur l'auteur du livre ne peut pas être vide ...");
                                    Console.WriteLine("> Entrez un nom d'Auteur: ");
                                    authorName = Console.ReadLine();
                                }

                                parameters[0] = bookTitle;
                                parameters[1] = authorName;

                                stock.TryAddBook(parameters);

                                exitSubMenuOption = true;
                                break;
                            default:
                                Console.WriteLine("> Entrez d'abord une option valide, les options valides sont 'ajouter, supprimer, modifier' ...");
                                break;
                        }

                    }
                    break;
                default:
                    Console.WriteLine("> Entrez d'abord une option valide, les options valides sont 'livre, membre' ...");
                    break;
            }

            // Gestion de la touche 'Entrée' pour retourner au menu principale
            DisplayAndHandleEnterKey();
        }
    }
}