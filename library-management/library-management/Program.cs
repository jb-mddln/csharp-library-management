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
            // Initialisation de nos différentes class permettant de gérer les actions de l'utilisateur
            BorrowingManager borrowingManager = new BorrowingManager();
            MemberManager memberManager = new MemberManager(borrowingManager);
            BookManager bookManager = new BookManager();
            MenuManager menuManager = new MenuManager();

            // Enregiste l'événement "ProcessExit" déclenche le code lors de la fermeture de notre console et le gère directement depuis notre Main 
            AppDomain.CurrentDomain.ProcessExit += (object? sender, EventArgs e) =>
            {
                borrowingManager.Save();
                memberManager.Save();
                bookManager.Save();
            };

            // Boucle infinie pour forcer l'état ouvert de la console et gérer le texte entré dessus
            while (true)
            {
                // Récupère notre texte dans une variable de type string? nullable
                string? line = Console.ReadLine();

                // Appelle notre méthode de gestion de menu avec nos différents managers
                menuManager.HandleMenu(line, borrowingManager, memberManager, bookManager);
            }
        }
    }
}