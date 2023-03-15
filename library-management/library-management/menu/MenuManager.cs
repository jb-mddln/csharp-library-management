using library_management.book;
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
            Console.WriteLine("Type 1, 2, 3, 4 to select an option ...");
            Console.WriteLine(@"
1) Book management :
    - Display all books, allow you to select, edit and add
2) Borrower management
    - Display all borrowers, allow you to select, edit and add
3) Display all books still available for borrowing
4) Display all books not available for borrowing
");
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
                Console.WriteLine("> Type 1, 2, 3, 4 first to select an option ...");
                return;
            }

            switch (line[0]) 
            {
                case '1':
                    Console.WriteLine("List of books in the library: \n");
                    Console.WriteLine(stock.GetBooksDetails());

                    // Code sous-menu
                    var subOption = Console.ReadLine();

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
