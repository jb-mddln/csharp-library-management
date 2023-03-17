using library_management.member;
using System.Text;

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
> Entrez 'livre' pour afficher le sous-menu de gestion des livres
> Entrez 'membre' pour afficher le sous-menu de gestion des membres");
        }

        /// <summary>
        /// Affiche nos différentes options de notre menu (livre, membre)
        /// </summary>
        /// <param name="options">Les options valident pour notre menu</param>
        private void DisplayMenuOptions(string options)
        {
            Console.WriteLine(@$"----
> Entrez '{options}' ...
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

            // La touche pressée est égale à la touche 'Entrée' on a quitté la boucle alors on affiche le menu principale
            Console.Clear();
            DisplayMenu();
        }

        /// <summary>
        /// Gère notre menu, son affichage, les différentes options et actions sur nos managers
        /// </summary>
        /// <param name="line">Texte entré par l'utilisateur dans notre méthode Main</param>
        /// <param name="memberManager">Passe une instance de notre class de gestion de membre</param>
        /// <param name="bookManager">Passe une instance de notre class de gestion de livre</param>
        public void HandleMenu(string line, MemberManager memberManager, BookManager bookManager)
        {
            // Condition if, si notre ligne est "null" ou vide alors on affiche un message d'erreur
            if (string.IsNullOrEmpty(line))
            {
                Console.WriteLine("> Erreur ligne vide ...");
                // Condition remplie, on effectue un retour pour ne pas exécuter le code plus bas
                return;
            }

            // Efface le texte de notre console pour plus de clarté
            Console.Clear();

            // Condition if, si notre ligne ne contient qu'un caractère il s'agit surement d'une option rapide
            if (line.Length == 1)
            {
                HandleQuickOptionsMenu(line[0], memberManager, bookManager);
                // Condition remplie, on effectue un retour pour ne pas exécuter le code plus bas
                return;
            }

            string otherOption = line.ToLower();
            if (otherOption == "livre")
            {
                DisplayMenuOptions("ajouter, supprimer, modifier");

                bool exitBookMenu = false;

                // Boucle while tant que exitBookMenu est égale à false on reste dans notre menu livre
                while (!exitBookMenu)
                {
                    exitBookMenu = HandleBookMenu("ajouter, supprimer, modifier");
                }

                // Gestion de la touche 'Entrée' pour retourner au menu principale
                DisplayAndHandleEnterKey();

                // Condition remplie, on effectue un retour pour ne pas exécuter le code plus bas
                return;
            }

            if (otherOption == "membre")
            {
                DisplayMenuOptions("ajouter, supprimer, modifier, détails");
                // Condition remplie, on effectue un retour pour ne pas exécuter le code plus bas
                return;
            }
            
            Console.WriteLine("> Entrez d'abord une option valide, les options valides sont '1, 2, 3, 4' ou 'livre, membre' ...");
            // Aucune entrée valide, on invite l'utilisateur à faire 'Entrée' pour revenir au menu principal
            DisplayAndHandleEnterKey();
        }

        /// <summary>
        /// Gère la sélection d'options rapide
        /// </summary>
        /// <param name="character">char entré par l'utilisateur (1, 2, 3, 4)</param>
        /// <param name="memberManager">Passe une instance de notre class de gestion de membre</param>
        /// <param name="bookManager">Passe une instance de notre class de gestion de livre</param>
        private void HandleQuickOptionsMenu(char character, MemberManager memberManager, BookManager bookManager)
        {
            // Gère la sélection d'options rapide, 1er caractère de notre ligne
            switch (character)
            {
                case '1':
                    Console.WriteLine("> Liste des membres de la bibliothèque: \n");
                    Console.WriteLine(memberManager.GetMembersDetails() + "\n");
                    break;
                case '2':
                    Console.WriteLine("> Liste des livres de la bibliothèque: \n");
                    Console.WriteLine(bookManager.GetBooksDetails() + "\n");
                    break;
                case '3':
                    Console.WriteLine("> Liste des livres encore disponibles à l'emprunt: \n");
                    Console.WriteLine(bookManager.GetAvailableBooks() + "\n");
                    break;
                case '4':
                    Console.WriteLine("> Liste des livres indisponibles à l'emprunt: \n");
                    Console.WriteLine(bookManager.GetNotAvailableBooks() + "\n");
                    break;
                default:
                    Console.WriteLine("> Entrez d'abord une option valide, les options valides sont '1, 2, 3, 4' ...");
                    break;
            }

            // Gestion de la touche 'Entrée' pour retourner au menu principale
            DisplayAndHandleEnterKey();
        }

        private bool HandleBookMenu(string options)
        {
            string? option = Console.ReadLine();

            // Condition if, si notre ligne est null ou vide alors on affiche un message invitant l'utilisateur à taper une option valide
            if (string.IsNullOrEmpty(option))
            {
                Console.WriteLine($"> Entrez d'abord une option valide, les options valides sont '{options}' ...");
                return false;
            }
        
            // Découpe le string entré par l'utilisateur, on ne veut que récupérer le 1er mot
            string[] subMenuOption = option.Split(" ");

            switch (subMenuOption[0].ToLower())
            {
                case "ajouter":

                    return true;
                case "supprimer":

                    return true;
                case "modifier":

                    return true;
                default:
                    Console.WriteLine($"> Entrez d'abord une option valide, les options valides sont '{options}' ...");
                    return false;
            }
        }

        /// <summary>
        /// Récupère le texte entré par l'utilisateur, ne peut être null ou vide sert à récupérer les paramètres entrés par l'utilisateur
        /// </summary>
        /// <param name="parameterName">Nom du paramètre à récupérer sert uniquement pour l'affichage</param>
        /// <returns>Paramètre entré par l'utilisateur</returns>
        private string HandleStringParameterInput(string parameterName)
        {
            Console.WriteLine($"> Entrez {parameterName}: ");

            string? parameter = Console.ReadLine();

            while (string.IsNullOrEmpty(parameter))
            {
                Console.WriteLine($"> Erreur {parameterName} ne peut pas être vide ...");
                Console.WriteLine($"> Entrez {parameterName}: ");
                parameter = Console.ReadLine();
            }

            return parameter;
        }
    }
}