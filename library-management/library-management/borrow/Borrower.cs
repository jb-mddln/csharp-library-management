namespace library_management.borrow
{
    /// <summary>
    /// Représente notre objet Emprunteur
    /// </summary>
    public class Borrower
    {
        public string LastName { get; set; }

        public string FirstName { get; set; }

        public List<int> BorrowedBookIds { get; set; }

        public DateTime RegistrationDate { get; set; }

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        public Borrower() 
        { 
        }
    }
}
