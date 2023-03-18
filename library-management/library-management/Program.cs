using library_management.managers;

namespace library_management
{
    internal class Program
    {
        /// <summary>
        /// Point d'entrée de notre programme
        /// </summary>
        /// <param name="args"></param>
        private static void Main(string[] args)
        {
            // Initialisation de nos différentes class permettant de gérer l'ensemble de notre programme
            
            // Gestion des emprunts
            BorrowingManager borrowingManager = new BorrowingManager();

            // Gestion des membres
            MemberManager memberManager = new MemberManager(borrowingManager);

            // Gestion des livres
            BookManager bookManager = new BookManager();

            // Gestion du menu
            MenuManager menuManager = new MenuManager();

            // Enregiste l'événement "ProcessExit" déclenche le code lors de la fermeture de notre console et le gère directement depuis notre Main 
            AppDomain.CurrentDomain.ProcessExit += (object? sender, EventArgs e) =>
            {
                borrowingManager.Save();
                memberManager.Save();
                bookManager.Save();
            };

            // Boucle pour forcer l'état ouvert de la console et gérer le texte entré dessus, touche échappe pour fermer la console
            while (Console.ReadKey().Key != ConsoleKey.Escape)
            {
                // Récupère notre texte dans une variable de type string? nullable
                string? line = Console.ReadLine();

                // Appelle notre méthode de gestion de menu avec nos différents managers
                menuManager.HandleMenu(line, borrowingManager, memberManager, bookManager);
            }
        }
    }
}