using library_management.borrow;

namespace library_management.managers
{
    public class BorrowingManager
    {
        public List<BorrowingRecord> BorrowingRecords { get; set; }

        /// <summary>
        /// Constructeur par défaut, récupère les infos via un fichier CSV
        /// </summary>
        public BorrowingManager() 
        {
            this.BorrowingRecords = new List<BorrowingRecord>();
            this.Load();
        }

        /// <summary>
        /// Retourne une instance de notre objet emprunt contenu dans notre liste d'emprunts
        /// </summary>
        /// <param name="recordIdString">Id de l'emprunt</param>
        /// <returns>Null ou notre objet emprunt</returns>
        public BorrowingRecord? TryGetRecord(string recordIdString)
        {
            // recordIdString n'est pas un integer valide retourne false on retourne null
            if (!int.TryParse(recordIdString, out int recordId))
                return null;

            // Linq Any, notre liste BorrowingRecords ne contient pas d'emprunt avec l'id on retourne null
            if (!this.BorrowingRecords.Any(record => record.Id == recordId))
                return null;

            return this.BorrowingRecords.FirstOrDefault(record => record.Id == recordId);
        }

        /// <summary>
        /// Retourne une liste de l'historique d'emprunt d'un membre
        /// </summary>
        /// <param name="memberId">Id du membre</param>
        /// <returns>Liste de l'historique d'emprunt d'un membre</returns>
        public IEnumerable<BorrowingRecord> TryGetMemberRecords(int memberId)
        {
            // Aucun historique d'emprunt pour le membre, on retourne une liste vide
            if (!this.BorrowingRecords.Any(record => record.MemberId == memberId))
                return Enumerable.Empty<BorrowingRecord>();

            return this.BorrowingRecords.Where(record => record.MemberId == memberId);
        }

        /// <summary>
        /// Retourne une liste de l'historique d'emprunt d'un livre
        /// </summary>
        /// <param name="bookId">Id du livre</param>
        /// <returns>Liste de l'historique d'emprunt d'un livre</returns>
        public IEnumerable<BorrowingRecord> TryGetBookRecords(int bookId)
        {
            // Aucun historique d'emprunt pour le membre, on retourne une liste vide
            if (!this.BorrowingRecords.Any(record => record.BookId == bookId))
                return Enumerable.Empty<BorrowingRecord>();

            return this.BorrowingRecords.Where(record => record.BookId == bookId);
        }

        /// <summary>
        /// Retourne les détails de tous les emprunts en cours sous forme de chaine de caractères
        /// </summary>
        /// <param name="bookManager">Instance de notre class de gestion de livre</param>
        /// <param name="memberManager">Instance de notre class de gestion de membre</param>
        /// <returns>Les détails de tous les emprunts en cours</returns>
        public string GetBorrowingsInProgress(BookManager bookManager, MemberManager memberManager)
        {
            string infos = string.Empty;
            foreach (BorrowingRecord record in this.BorrowingRecords) 
            {
                // L'emprunt est finis
                if (record.HasReturned())
                    continue;

                infos += "\n" + "Id de l'emprunt: " + record.Id 
                    + "\n" + "> Livre: " + bookManager.GetBookIdAndNameById(record.BookId)
                    + "> Emprunté par: " + memberManager.GetMemberIdAndNameById(record.MemberId)
                    + " depuis: " + DateTime.Now.Subtract(record.DateOfBorrow).Days + " jour(s)"
                    + "\n" + record.GetDetails() 
                    + "\n";
            }

            return infos;
        }


        /// <summary>
        /// Retourne les détails de tous les emprunts finis sous forme de chaine de caractères
        /// </summary>
        /// <param name="bookManager">Instance de notre class de gestion de livre</param>
        /// <param name="memberManager">Instance de notre class de gestion de membre</param>
        /// <returns>Les détails de tous les emprunts finis</returns>
        public string GetBorrowingsDone(BookManager bookManager, MemberManager memberManager)
        {
            string infos = string.Empty;
            foreach (BorrowingRecord record in this.BorrowingRecords)
            {
                // L'emprunt n'est pas finis
                if (!record.HasReturned())
                    continue;

                infos += "\n" + bookManager.GetBookIdAndNameById(record.BookId)
                    + "> Emprunté par: " + memberManager.GetMemberIdAndNameById(record.MemberId)
                    + " durée de l'emprunt: " + record.DateOfReturn.Value.Subtract(record.DateOfBorrow).Days + " jour(s)"
                    + "\n" + record.GetDetails()
                    + "\n";
            }

            return infos;
        }

        /// <summary>
        /// Ajoute un emprunt de livre à notre membre
        /// </summary>
        /// <param name="memberId">Id du membre</param>
        /// <param name="bookId">Id du livre</param>
        public void AddRecord(int memberId, int bookId)
        {
            // Utilisation de Linq Select pour récupérer l'id max de notre liste d'emprunt
            int id = BorrowingRecords.Select(record => record.Id).Max();

            this.BorrowingRecords.Add(new BorrowingRecord
            {
                Id = id + 1,
                MemberId = memberId,
                BookId = bookId,
                DateOfBorrow = DateTime.Now,
            });
        }

        /// <summary>
        /// Charge nos données depuis le CSV si disponible, sinon créer le CSV vide
        /// </summary>
        private void Load()
        {
            if (File.Exists("borrowingRecords.csv"))
            {
                foreach (string line in File.ReadAllLines("borrowingRecords.csv"))
                {
                    string[] recordInfos = line.Split(",");

                    BorrowingRecord record = new BorrowingRecord();

                    record.Id = int.Parse(recordInfos[0]);
                    record.MemberId = int.Parse(recordInfos[1]);
                    record.BookId = int.Parse(recordInfos[2]);
                    record.DateOfBorrow = DateTime.Parse(recordInfos[3]);
                    string dateOfReturnString = recordInfos[4];
                    if (dateOfReturnString != "null") 
                    {
                        record.DateOfReturn = DateTime.Parse(dateOfReturnString);
                    }

                    this.BorrowingRecords.Add(record);
                }

                return;
            }

            File.Create("borrowingRecords.csv");
        }

        /// <summary>
        /// Sauvegarde la liste BorrowingRecords dans un fichier borrowingRecords.csv
        /// </summary>
        public void Save()
        {
            File.WriteAllLines("borrowingRecords.csv", this.BorrowingRecords.Select(record => record.GetCSV()));
        }
    }
}
