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

        // public List<int> BorrowedBookIds { get; set; }

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
        /// Retourne les informations du membre sous forme de chaine de caractères
        /// </summary>
        /// <returns>Infos du membre</returns>
        public string GetDetails()
        {
            // DateTime.ToString avec le format pour ne garder que le jour/mois/année heure:minute
            return "Id: " + Id
                + "\n" + "Nom, Prénom: " + LastName + ", " + FirstName
                + "\n" + "Date d'Inscription: " + RegistrationDate.ToString("dd/MM/yyyy HH:mm");
        }
    }
}
