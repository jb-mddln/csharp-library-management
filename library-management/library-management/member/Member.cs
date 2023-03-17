using library_management.borrow;
using library_management.managers;

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

        public List<BorrowingRecord> BorrowingRecords { get; set; }

        public DateTime RegistrationDate { get; set; }

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        public Member() 
        { 
        }

        /// <summary>
        /// Constructeur permettant d'initialiser directement nos attributs avec les paramètres
        /// </summary>
        /// <param name="id"></param>
        /// <param name="lastName"></param>
        /// <param name="firstName"></param>
        /// <param name="registrationDate"></param>
        public Member(int id, string lastName, string firstName, List<BorrowingRecord> borrowingRecords, DateTime registrationDate)
        {
            this.Id = id;
            this.LastName = lastName;
            this.FirstName = firstName;
            this.BorrowingRecords = borrowingRecords;
            this.RegistrationDate = registrationDate;
        }

        /// <summary>
        /// Retourne uniquement l'id du membre et son nom, prénom
        /// </summary>
        /// <returns>Id du membre et son nom, prénom</returns>
        public string GetIdAndName()
        {
            return this.Id + " " + this.LastName + " " + this.FirstName;
        }

        /// <summary>
        /// Retourne les informations du membre sous forme de chaine de caractères
        /// </summary>
        /// <returns>Infos du membre</returns>
        public string GetDetails()
        {
            // DateTime.ToString avec le format pour ne garder que le jour/mois/année heure:minute
            return "Id: " + this.Id
                + "\n" + "Nom, Prénom: " + this.LastName + ", " + this.FirstName
                + "\n" + "Nombre de livres empruntés: " +   this.BorrowingRecords.Count
                + "\n" + "Date d'inscription: " + this.RegistrationDate.ToString("dd/MM/yyyy HH:mm");
        }

        /// <summary>
        /// Retourne notre membre au format CSV (comma-separated values)
        /// </summary>
        /// <returns>Infos du membre au format CSV</returns>
        public string GetCSV()
        {
            return "";
            /*string borrowedBookId = string.Join(" ", BorrowedBookIds);
            return this.Id + ","
                + this.LastName + ","
                + this.FirstName + ","
                + "[" + (string.IsNullOrEmpty(borrowedBookId) ? "" : borrowedBookId) + "],"
                + this.RegistrationDate.ToString("dd/MM/yyyy HH:mm");*/
        }

        public string GetBorrowedBooksDetails(BookManager bookManager)
        {
            return "";
            // return bookManager.GetBooksDetailsById(BorrowedBookIds);
        }
    }
}