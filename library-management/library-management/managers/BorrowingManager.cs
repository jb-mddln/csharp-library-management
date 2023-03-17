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
