using library_management.menu;
using library_management.stock;

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
            // Enregistre l'événement "ProcessExit" et déclenche la méthode "OnProcessExit" lors de la fermeture de notre console
            AppDomain.CurrentDomain.ProcessExit += OnProcessExit;

            // Initialisation de nos différentes class permettant de gérer les actions de l'utilisateur
            StockManager stockManager = new StockManager();
            MenuManager menuManager = new MenuManager();

            // Boucle pour forcer l'état ouvert de la console et gérer le texte entré dessus
            while (true)
            {
                // Récupère notre texte dans une variable de type string? nullable
                string? line = Console.ReadLine();

                // Appelle notre méthode de gestion de menu
                menuManager.HandleMenu(line, stockManager);
            }
        }

        /// <summary>
        /// Méthode de retour pour notre événement, se déclenche à la fermeture de notre console
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        private static void OnProcessExit(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}