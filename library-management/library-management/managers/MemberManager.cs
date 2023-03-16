using library_management.member;

namespace library_management.managers
{
    /// <summary>
    /// Récupère les données de nos membres et gère toutes les actions les concernant
    /// </summary>
    public class MemberManager
    {
        public List<Member> Members { get; set; }

        /// <summary>
        /// Constructeur par défaut, récupère les infos via un fichier CSV
        /// </summary>
        public MemberManager()
        {
            // Initialise notre variable Membres en tant que liste vide
            Members = new List<Member>();

            // Enregistre l'événement "ProcessExit" et déclenche la méthode "OnProcessExit" lors de la fermeture de notre console
            AppDomain.CurrentDomain.ProcessExit += OnProcessExit;

            if (File.Exists("members.csv"))
            {
                // Boucle foreach, chaque ligne de notre fichier members.csv représente un objet Member
                foreach (string line in File.ReadAllLines("members.csv"))
                {
                    // Les infos concernant un membres sont séparées par un ',' l'ordre des infos/données et le même que notre classe Member
                    string[] membersInfos = line.Split(",");

                    // Initialise un nouveau membre avec le constructeur par défaut
                    Member member = new Member();

                    // On passe nos infos dans les différents attributs
                    member.Id = int.Parse(membersInfos[0]);
                    member.LastName = membersInfos[1];
                    member.FirstName = membersInfos[2];
                    member.RegistrationDate = DateTime.Parse(membersInfos[3]);

                    // Ajout du membre dans notre liste
                    Members.Add(member);
                }

                // Retour pour stopper l'exécution du code, pas besoin d'aller plus loin
                return;
            }

            // Aucune donnée sur les membres, ont créé notre fichier
            File.Create("members.csv");
        }

        public bool TryAddMember(string[] parameters)
        {
            Member newMember = new Member();

            string lastName = parameters[0];
            string firstName = parameters[1];

            // Titre 'null' ou vide on retourne false et stop l'ajout (ne dois normalement jamais arrivée)
            if (string.IsNullOrEmpty(lastName))
                return false;

            // Auteur 'null' ou vide on retourne false et stop l'ajout (ne dois normalement jamais arrivée)
            if (string.IsNullOrEmpty(firstName))
                return false;

            // Utilisation de Linq Select pour récupérer l'id max de notre liste membre
            int id = Members.Select(member => member.Id).Max();

            newMember.Id = id + 1; // Id max de notre liste + 1 pour un id libre
            newMember.LastName = lastName;
            newMember.FirstName = firstName;
            newMember.RegistrationDate = DateTime.Now;

            // Linq Any, notre liste Books contient déjà un livre avec l'id générer on stop l'ajout retourne false (ne dois normalement jamais arrivée)
            if (Members.Any(member => member.Id == newMember.Id))
                return false;

            // Ajout du nouveau membre dans notre liste
            Members.Add(newMember);
            // Retourne true succès
            return true;
        }

        /// <summary>
        /// Retourne les détails de tous les livres sous forme de chaine de caractères
        /// </summary>
        /// <returns>Les détails de tous les livres</returns>
        public string GetMembersDetails()
        {
            // Utilisation de Linq Select pour sélectionner et retourner les infos de chaque membre
            // Utilisation de string.Join pour joindre notre liste de détails et ajouter deux retours à la ligne pour la clarté lors de l'affichage
            return string.Join("\n\n", Members.Select(member => member.GetDetails()));

            /* Liste vide pour stocker nos string contenant les infos des membres
             * var memberDetails = new List<string>();
             * foreach(Member member in Members)
             * {
             * memberDetails.Add(member.GetDetails());
             * }            
             * return string.Join("\n\n", memberDetails); */
        }

        /// <summary>
        /// Sauvegarde la liste Members dans un fichier members.csv
        /// </summary>
        public void Save()
        {
            // Linq, Select
            File.WriteAllLines("members.csv", Members.Select(member => member.GetCSV()));

            /* using StreamWriter sw = new StreamWriter("members.csv");
             * foreach (Member member in Members)
             * {
             * sw.WriteLine(member.GetBookCSV());
             * } */
        }

        /// <summary>
        /// Méthode de retour pour notre événement, se déclenche à la fermeture de notre console
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnProcessExit(object? sender, EventArgs e)
        {
            Save();
        }
    }
}
