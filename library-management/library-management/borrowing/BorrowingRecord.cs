namespace library_management.borrow
{
    /// <summary>
    /// Représente notre objet emprunt d'un membre
    /// </summary>
    public class BorrowingRecord
    {
        /// <summary>
        /// Id, utile pour identifier clairement notre livre lors des différentes opérations
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Id du livre emprunté
        /// </summary>
        public int BookId { get; set; }

        /// <summary>
        /// Id du membre qui a emprunté le livre
        /// </summary>
        public int MemberId { get; set; }

        /// <summary>
        /// Emprunté le
        /// </summary>
        public DateTime DateOfBorrow { get; set; }

        /// <summary>
        /// Date de retour, si aucun retour alors vaut 'null'
        /// </summary>
        public DateTime? DateOfReturn { get; set; }

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        public BorrowingRecord() 
        { 
        }
        /// <summary>
        /// Permets de vérifier si l'emprunt a été rendu
        /// </summary>
        /// <returns>Retourne vrai si la date de retour possède une valeur</returns>
        public bool HasReturned()
        {
            return this.DateOfReturn.HasValue;
        }

        /// <summary>
        /// Retourne les informations de l'emprunt sous forme de chaine de caractères
        /// </summary>
        /// <returns>Informations de l'emprunt</returns>
        public string GetDetails()
        {
            return "> Date de l'emprunt: " + this.DateOfBorrow.ToString("dd/MM/yyyy HH:mm")
                + (this.HasReturned() ? "\n" + "> Date du retour: " + this.DateOfReturn.Value.ToString("dd/MM/yyyy HH:mm") : "");
        }

        /// <summary>
        /// Retourne notre historique au format CSV (comma-separated values) sous forme de chaine de caractères
        /// </summary>
        /// <returns>Infos de l'historique au format CSV</returns>
        public string GetCSV()
        {
            return + this.Id + "," 
                + this.MemberId + ","
                + this.BookId + ","
                + this.DateOfBorrow.ToString("dd/MM/yyyy HH:mm") + ","
                + (this.DateOfReturn.HasValue ? this.DateOfReturn.Value.ToString("dd/MM/yyyy HH:mm") : "null");
        }
    }
}
