using library_management.stock;

namespace library_management.menu
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
> Type '1, 2, 3, 4' to select quickly an option ...
----
1) Display all books, allow you after to select, edit, add, delete
2) Display all borrowers, allow you to select, edit, add, delete
3) Display all books still available for borrowing
4) Display all books not available for borrowing
----
----
> Type 'book' to display the sub-menu for book (add, edit, delete)
> Type 'borrow' to display the sub-menu for borrowing book (add, edit, delete)");
        }

        /// <summary>
        /// Gère notre menu, son affichage et les différentes options
        /// </summary>
        /// <param name="line"></param>
        /// <param name="stock"></param>
        public void HandleMenu(string line, StockManager stock)
        {
            if (string.IsNullOrEmpty(line))
            {
                Console.WriteLine("> Type '1, 2, 3, 4' first to select an option ...");
                return;
            }

            // Gère la sélection d'options rapide
            if (line.Length == 1)
            {
                switch (line[0])
                {
                    case '1':
                        Console.WriteLine("> List of books in the library: \n");
                        Console.WriteLine(stock.GetBooksDetails() + "\n");

                        // Code sous-menu
                        Console.WriteLine("> Type 'select, edit, delete + book id' for action or 'add' for adding a new book to the collection");
                        var subOption = Console.ReadLine();
                        if (string.IsNullOrEmpty(subOption))
                        {
                            Console.WriteLine("Type a command first: 'select, edit, delete, add'");
                            return;
                        }

                        switch (subOption)
                        {

                        }

                        break;
                    case '2':

                        break;
                    case '3':
                        Console.WriteLine("List of books still available: \n");
                        Console.WriteLine(stock.GetAvailableBooks());
                        break;
                    case '4':
                        Console.WriteLine("List of books not available: \n");
                        Console.WriteLine(stock.GetTakenBooks());
                        break;
                    default:
                        Console.WriteLine("Type a valid option first, valid options are 1, 2, 3, 4 ...");
                        break;
                }
            }
        }
    }
}