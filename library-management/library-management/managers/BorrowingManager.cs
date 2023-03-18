using library_management.borrow;
using library_management.member;

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
            if (!BorrowingRecords.Any(record => record.Id == recordId))
                return null;

            return BorrowingRecords.FirstOrDefault(record => record.Id == recordId);
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

                infos += "\n" + "Id de l'emprunt: " + record.Id 
                    + "\n" + "> Livre: " + bookManager.GetBookIdAndNameById(record.BookId)
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

        public void AddRecord(int memberId, int bookId)
        {
            BorrowingRecords.Add(new BorrowingRecord
            {
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
