using library_management.borrow;

namespace library_management.managers
{
    public class BorrowingManager
    {
        public List<BorrowingRecord> BorrowingRecords { get; set; }

        public BorrowingManager() 
        {
            BorrowingRecords = new List<BorrowingRecord>();
            Load();
        }

        public IEnumerable<BorrowingRecord> TryGetMemberRecords(int memberId)
        {
            // Aucun historique d'emprunt pour le membre, on retourne une liste vide
            if (!BorrowingRecords.Any(member => member.MemberId == memberId))
                return Enumerable.Empty<BorrowingRecord>();

            return BorrowingRecords.Where(record => record.MemberId == memberId);
        }

        public string GetBorrowingsInProgress(BookManager bookManager, MemberManager memberManager)
        {
            string infos = string.Empty;
            foreach (BorrowingRecord record in BorrowingRecords) 
            {
                // L'emprunt est finis
                if (record.HasReturned())
                    continue;

                infos += "\n" + bookManager.GetBookIdAndNameById(record.BookId)
                    + "> Emprunté par: " + memberManager.GetMemberIdAndNameById(record.MemberId)
                    + " depuis: " + DateTime.Now.Subtract(record.DateOfBorrow).Days + " jour(s)"
                    + "\n" + record.GetDetails() 
                    + "\n";
            }

            return infos;
        }

        public string GetBorrowingsDone(BookManager bookManager, MemberManager memberManager)
        {
            string infos = string.Empty;
            foreach (BorrowingRecord record in BorrowingRecords)
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

                    record.MemberId = int.Parse(recordInfos[0]);
                    record.BookId = int.Parse(recordInfos[1]);
                    record.DateOfBorrow = DateTime.Parse(recordInfos[2]);
                    string dateOfReturnString = recordInfos[3];
                    if (dateOfReturnString != "null") 
                    {
                        record.DateOfReturn = DateTime.Parse(dateOfReturnString);
                    }

                    BorrowingRecords.Add(record);
                }

                return;
            }

            File.Create("borrowingRecords.csv");
        }

        /// <summary>
        /// Sauvegarde la liste Members dans un fichier members.csv
        /// </summary>
        public void Save()
        {
            File.WriteAllLines("borrowingRecords.csv", BorrowingRecords.Select(record => record.GetCSV()));
        }
    }
}
