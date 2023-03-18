using library_management.borrow;
using library_management.managers;
using System.Text;

namespace library_management.member
{
    /// <summary>
    /// Représente notre objet membre
    /// </summary>
    public class Member
    {
        /// <summary>
        /// Id, utile pour identifier clairement notre livre lors des différentes opérations
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nom
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Prénom
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Liste des emprunts
        /// </summary>
        public IEnumerable<BorrowingRecord> BorrowingRecords { get; set; }

        /// <summary>
        /// Date d'inscription
        /// </summary>
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
        /// Retourne uniquement l'id du membre et son nom, prénom sous forme de chaine de caractères
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
                + "\n" + "Nombre de livres empruntés: " +   this.BorrowingRecords.Count()
                + "\n" + "Date d'inscription: " + this.RegistrationDate.ToString("dd/MM/yyyy HH:mm");
        }

        /// <summary>
        /// Retourne notre membre au format CSV (comma-separated values) sous forme de chaine de caractères
        /// </summary>
        /// <returns>Infos du membre au format CSV</returns>
        public string GetCSV()
        {
            return this.Id + ","
                + this.LastName + ","
                + this.FirstName + ","
                + this.RegistrationDate.ToString("dd/MM/yyyy HH:mm");
        }

        /// <summary>
        /// Retourne les informations liées aux emprunts sous forme de chaine de caractères
        /// </summary>
        /// <param name="bookManager">Instance de notre class BookManager utile à la récupération des infos concernant au livre emprunté</param>
        /// <returns>Les informations liées aux emprunts</returns>
        public string GetBorrowedBooksDetails(BookManager bookManager)
        {
            string infos = string.Empty;
            foreach (BorrowingRecord record in BorrowingRecords) 
            {
                string infoReturn = record.HasReturned() ? "Rendu" : "Emprunt en cours depuis: " + DateTime.Now.Subtract(record.DateOfBorrow).Days + " Jour(s)";
                infos += "\n" 
                    + bookManager.GetBookIdAndNameById(record.BookId)
                    + ", " + infoReturn
                    + "\n" + record.GetDetails()
                    + "\n";
            }
            return infos;
        }
    }
}