namespace library_management.member
{
    /// <summary>
    /// Représente notre objet Membre
    /// </summary>
    public class Member
    {
        public int Id { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public List<int> BorrowedBookIds { get; set; }

        public DateTime RegistrationDate { get; set; }

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        public Member() 
        { 
        }
    }
}
