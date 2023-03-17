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

        // Tous les Ids de livres emprunter depuis la création du compte
        // todo Mettre dans une classe historique ? (Date d'emprunt, date de retour ...)
        public List<int> BorrowedBookIds { get; set; }

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
        public Member(int id, string lastName, string firstName, DateTime registrationDate)
        {
            this.Id = id;
            this.LastName = lastName;
            this.FirstName = firstName;
            this.RegistrationDate = registrationDate;
        }

        /// <summary>
        /// Retourne uniquement l'id du membre et son nom, prénom
        /// </summary>
        /// <returns>Id du membre et son nom, prénom</returns>
        public string GetIdAndName()
        {
            return Id + " " + LastName + " " + FirstName;
        }

        /// <summary>
        /// Retourne les informations du membre sous forme de chaine de caractères
        /// </summary>
        /// <returns>Infos du membre</returns>
        public string GetDetails()
        {
            // DateTime.ToString avec le format pour ne garder que le jour/mois/année heure:minute
            return "Id: " + Id
                + "\n" + "Nom, Prénom: " + LastName + ", " + FirstName
                + "\n" + "Nombre de livres empruntés: " + BorrowedBookIds.Count
                + "\n" + "Date d'inscription: " + RegistrationDate.ToString("dd/MM/yyyy HH:mm");
        }

        /// <summary>
        /// Retourne notre membre au format CSV (comma-separated values)
        /// </summary>
        /// <returns>Infos du membre au format CSV</returns>
        public string GetCSV()
        {
            string borrowedBookId = string.Join(" ", BorrowedBookIds);
            return Id + ","
                + LastName + ","
                + FirstName + ","
                + "[" + (string.IsNullOrEmpty(borrowedBookId) ? "" : borrowedBookId) + "],"
                + RegistrationDate.ToString("dd/MM/yyyy HH:mm");
        }

        public string GetBorrowedBooksDetails(BookManager bookManager)
        {
            return bookManager.GetBooksDetailsById(BorrowedBookIds);
        }
    }
}
