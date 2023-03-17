namespace library_management.borrow
{
    /// <summary>
    /// Représente l'historique d'emprunt d'un membre
    /// </summary>
    public class BorrowingRecord
    {
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

        public bool HasReturned()
        {
            return DateOfReturn.HasValue;
        }

        /// <summary>
        /// Retourne les informations de l'emprunt sous forme de chaine de caractères
        /// </summary>
        /// <returns>Informations de l'emprunt</returns>
        public string GetDetails()
        {
            return "> Date de l'emprunt: " + DateOfBorrow.ToString("dd/MM/yyyy HH:mm")
                + (HasReturned() ? "\n" + "> Date du retour: " + DateOfReturn.Value.ToString("dd/MM/yyyy HH:mm") : "");
        }

        /// <summary>
        /// Retourne notre historique au format CSV (comma-separated values)
        /// </summary>
        /// <returns>Infos de l'historique au format CSV</returns>
        public string GetCSV()
        {
            return this.MemberId + ","
                + this.BookId + ","
                + this.DateOfBorrow.ToString("dd/MM/yyyy HH:mm") + ","
                + (this.DateOfReturn.HasValue ? DateOfReturn.Value.ToString("dd/MM/yyyy HH:mm") : "null");
        }
    }
}
