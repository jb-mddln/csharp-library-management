﻿using library_management.book;
using library_management.borrow;
using library_management.member;

namespace library_management.managers
{
    /// <summary>
    /// Gère la partie menu de notre console et l'affichage des données
    /// </summary>
    public class MenuManager
    {
        /// <summary>
        /// Constructeur par défaut, affiche notre menu directement lors de son initialisation
        /// </summary>
        public MenuManager()
        {
            this.DisplayMenu();
        }

        /// <summary>
        /// Affiche notre menu
        /// </summary>
        private void DisplayMenu()
        {
            // Efface le texte de notre console pour plus de clarté
            Console.Clear();

            // @ Pour un string multi ligne
            Console.WriteLine(@"----
----
> Entrez '1, 2, 3, 4, 5, 6' pour sélectionner rapidement une option ...
----
1) Afficher tous les membres de la bibliothèque
2) Afficher tous les livres de la bibliothèque
3) Afficher tous les livres encore disponibles à l'emprunt
4) Afficher tous les livres indisponibles à l'emprunt
5) Afficher tous les emprunts en cours
6) Afficher tous les emprunts passés
----
----
> Entrez 'livre' pour afficher le sous-menu de gestion des livres
> Entrez 'membre' pour afficher le sous-menu de gestion des membres
> Entrez 'emprunt' pour afficher le sous-menu de gestion des emprunts");
        }

        /// <summary>
        /// Affiche nos différentes options de notre menu (livre, membre, emprunt)
        /// </summary>
        /// <param name="options">Les options valident pour notre menu</param>
        private void DisplayMenuOptions(string options)
        {
            // @ Pour un string multi ligne et $ pour la concaténation
            Console.WriteLine(@$"----
> Entrez '{options}' ...
----");
        }

        /// <summary>
        /// Demande à l'utilisateur de taper sur sa touche 'Entrée' pour retourner au menu principal
        /// </summary>
        private void DisplayAndHandleEnterKey()
        {
            Console.WriteLine("> Tapez sur 'Entrée' pour revenir au menu principal");

            // Boucle qui récupère la touche pressée par l'utilisateur dans la console et tant que ce n'est pas la touche 'Entrée' on reste dans la boucle et on affiche le message
            while (Console.ReadKey().Key != ConsoleKey.Enter)
            {
                Console.WriteLine("\n> Tapez sur 'Entrée' pour revenir au menu principal");
            }

            // La touche pressée est égale à la touche 'Entrée' on a quitté la boucle alors on affiche le menu principale
            this.DisplayMenu();
        }

        /// <summary>
        /// Gère notre menu, son affichage, les différentes options et actions sur nos managers
        /// </summary>
        /// <param name="line">Texte entré par l'utilisateur dans notre méthode Main</param>
        /// <param name="borrowingManager">Passe une instance de notre class de gestion d'emprunt</param>
        /// <param name="memberManager">Passe une instance de notre class de gestion de membre</param>
        /// <param name="bookManager">Passe une instance de notre class de gestion de livre</param>
        public void HandleMenu(string line, BorrowingManager borrowingManager, MemberManager memberManager, BookManager bookManager)
        {
            // Condition if, si notre ligne est "null" ou vide alors on affiche un message d'erreur
            if (string.IsNullOrEmpty(line))
            {
                Console.WriteLine("> Erreur ligne vide ...");
                // Condition remplie, on effectue un retour pour ne pas exécuter le code plus bas
                return;
            }

            // Efface le texte de notre console pour plus de clarté
            Console.Clear();

            // Condition if, si notre ligne ne contient qu'un caractère il s'agit surement d'une option rapide
            if (line.Length == 1)
            {
                this.HandleQuickOptionsMenu(line[0], borrowingManager, memberManager, bookManager);
                // Condition remplie, on effectue un retour pour ne pas exécuter le code plus bas
                return;
            }

            // On passe en miniscule pour gérer les entrée du type Livre, LIVRE, LiVrE ...
            string otherOption = line.ToLower();
            if (otherOption == "livre")
            {
                this.DisplayMenuOptions("ajouter, supprimer, modifier, détails, précédent");

                bool exitBookMenu = false;

                // Boucle while tant que exitBookMenu est égale à false on reste dans notre menu livre
                while (!exitBookMenu)
                {
                    exitBookMenu = this.HandleBookMenu("ajouter, supprimer, modifier, détails, précédent", borrowingManager, memberManager, bookManager);
                }

                // Gestion de la touche 'Entrée' pour retourner au menu principale
                this.DisplayAndHandleEnterKey();

                // Condition remplie, on effectue un retour pour ne pas exécuter le code plus bas
                return;
            }

            if (otherOption == "membre")
            {
                this.DisplayMenuOptions("ajouter, supprimer, modifier, détails, précédent");

                bool exitMemberMenu = false;

                // Boucle while tant que exitMemberMenu est égale à false on reste dans notre menu membre
                while (!exitMemberMenu)
                {
                    exitMemberMenu = this.HandleMemberMenu("ajouter, supprimer, modifier, détails, précédent", memberManager, bookManager);
                }

                // Gestion de la touche 'Entrée' pour retourner au menu principale
                this.DisplayAndHandleEnterKey();

                // Condition remplie, on effectue un retour pour ne pas exécuter le code plus bas
                return;
            }

            if (otherOption == "emprunt")
            {
                this.DisplayMenuOptions("ajouter, retour, précédent");

                bool exitBorrowMenu = false;

                // Boucle while tant que exitBorrowMenu est égale à false on reste dans notre menu livre
                while (!exitBorrowMenu)
                {
                    exitBorrowMenu = this.HandleBorrowMenu("ajouter, retour, précédent", borrowingManager, memberManager, bookManager);
                }

                // Gestion de la touche 'Entrée' pour retourner au menu principale
                this.DisplayAndHandleEnterKey();

                // Condition remplie, on effectue un retour pour ne pas exécuter le code plus bas
                return;
            }

            Console.WriteLine("> Entrez d'abord une option valide, les options valides sont '1, 2, 3, 4, 5, 6' ou 'livre, membre, emprunt' ...");
            // Aucune entrée valide, on invite l'utilisateur à faire 'Entrée' pour revenir au menu principal
            this.DisplayAndHandleEnterKey();
        }

        /// <summary>
        /// Gère la sélection d'options rapide
        /// </summary>
        /// <param name="character">char entré par l'utilisateur (1, 2, 3, 4)</param>
        /// <param name="borrowingManager">Passe une instance de notre class de gestion d'emprunt</param>
        /// <param name="memberManager">Passe une instance de notre class de gestion de membre</param>
        /// <param name="bookManager">Passe une instance de notre class de gestion de livre</param>
        private void HandleQuickOptionsMenu(char character, BorrowingManager borrowingManager, MemberManager memberManager, BookManager bookManager)
        {
            // Gère la sélection d'options rapide, 1er caractère de notre ligne
            switch (character)
            {
                case '1':
                    Console.WriteLine("> Liste des membres de la bibliothèque: \n");
                    Console.WriteLine(memberManager.GetMembersDetails() + "\n");
                    break;
                case '2':
                    Console.WriteLine("> Liste des livres de la bibliothèque: \n");
                    Console.WriteLine(bookManager.GetBooksDetails() + "\n");
                    break;
                case '3':
                    Console.WriteLine("> Liste des livres encore disponibles à l'emprunt: \n");
                    Console.WriteLine(bookManager.GetAvailableBooks() + "\n");
                    break;
                case '4':
                    Console.WriteLine("> Liste des livres indisponibles à l'emprunt: \n");
                    Console.WriteLine(bookManager.GetNotAvailableBooks() + "\n");
                    break;
                case '5':
                    Console.WriteLine("> Liste des emprunts en cours:");
                    Console.WriteLine(borrowingManager.GetBorrowingsInProgress(bookManager, memberManager));
                    break;
                case '6':
                    Console.WriteLine("> Liste des emprunts passés:");
                    Console.WriteLine(borrowingManager.GetBorrowingsDone(bookManager, memberManager));
                    break;
                default:
                    Console.WriteLine("> Entrez d'abord une option valide, les options valides sont '1, 2, 3, 4, 5, 6' ...");
                    break;
            }

            // Gestion de la touche 'Entrée' pour retourner au menu principale
            this.DisplayAndHandleEnterKey();
        }

        /// <summary>
        /// Gère nos options pour le menu livre
        /// </summary>
        /// <param name="options">Sert pour l'affichage des options disponibles au sein du menu livre</param>
        /// <param name="borrowingManager">Passe une instance de notre class de gestion d'emprunt</param>
        /// <param name="memberManager">Passe une instance de notre class de gestion de membre</param>
        /// <param name="bookManager">Passe une instance de notre class de gestion de livre</param>
        /// <returns>Retourne vrai ou faux selon la validité de l'action de l'utilisateur</returns>
        private bool HandleBookMenu(string options, BorrowingManager borrowingManager, MemberManager memberManager, BookManager bookManager)
        {
            string? option = Console.ReadLine();

            // Condition if, si notre ligne est null ou vide alors on affiche un message invitant l'utilisateur à taper une option valide
            if (string.IsNullOrEmpty(option))
            {
                Console.WriteLine($"> Entrez d'abord une option valide, les options valides sont '{options}' ...");
                return false;
            }
        
            // Découpe le string entré par l'utilisateur, on ne veut que récupérer le 1er mot
            string[] subMenuOption = option.Split(" ");

            // Utiliser plusieurs fois plus bas dans notre switch
            string bookIdString;
            Book? book = null;
            Member? member = null;
            string[] parameters;

            switch (subMenuOption[0].ToLower())
            {
                case "ajouter":
                    Console.WriteLine("> Tapez sur la touche 'Entrée' pour continuer l'ajout d'un livre");
                    Console.WriteLine("> Pour annuler et revenir au menu principal entrez 'précédent' puis tapez sur la touche 'Entrée' \n");
                    
                    // Quitte le menu et le mode ajout
                    if (this.HandleStringParameterInput("", true) == "précédent")
                        return true;

                    Console.WriteLine("> Ajout d'un livre:");
                    parameters = new string[6];

                    parameters[0] = this.HandleStringParameterInput("Nom du livre");
                    parameters[1] = this.HandleStringParameterInput("Auteur");
                    parameters[2] = this.HandleStringParameterInput("Genre");
                    parameters[3] = this.HandleStringParameterInput("Collection");
                    parameters[4] = this.HandleStringParameterInput("Date de publication");
                    parameters[5] = this.HandleStringParameterInput("Nombre du stock");

                    if (bookManager.TryAddBook(parameters) == true)
                    {
                        Console.WriteLine($"> Succès le livre {parameters[0]} a bien été ajouter aux livres de la bibliothèque");
                    }
                    else
                    {
                        Console.WriteLine("> Une erreur est survenue lors de l'ajout du livre ...");
                    }
                    return true;
                case "supprimer":
                    Console.WriteLine("> Tapez sur la touche 'Entrée' pour continuer la suppression d'un livre");
                    Console.WriteLine("> Pour annuler et revenir au menu principal entrez 'précédent' puis tapez sur la touche 'Entrée' \n");

                    // Quitte le menu et le mode supprimer
                    if (this.HandleStringParameterInput("", true) == "précédent")
                        return true;

                    Console.WriteLine("> Pour supprimer un livre tapez son Id (numéro avant le titre) puis tapez sur la touche 'Entrée':");
                    Console.WriteLine(bookManager.GetBooksIdAndName());

                    bookIdString = this.HandleStringParameterInput("Id du livre");
                    if (bookManager.TryDeleteBook(bookIdString) == true)
                    {
                        Console.WriteLine($"> Succès le livre {bookIdString} a bien été supprimer des livres de la bibliothèque");
                    }
                    else
                    {
                        Console.WriteLine("> Une erreur est survenue lors de la suppression du livre ...");
                    }
                    return true;
                case "modifier":
                    Console.WriteLine("> Tapez sur la touche 'Entrée' pour continuer la modification d'un livre");
                    Console.WriteLine("> Pour annuler et revenir au menu principal entrez 'précédent' puis tapez sur la touche 'Entrée' \n");

                    // Quitte le menu et le mode modifier
                    if (this.HandleStringParameterInput("", true) == "précédent")
                        return true;

                    Console.WriteLine("> Pour modifier un livre tapez son Id (numéro avant le titre) puis tapez sur la touche 'Entrée':");
                    Console.WriteLine("> Pour ne rien modifier laissez vide puis tapez sur la touche 'Entrée' et passer au champ suivant:");

                    Console.WriteLine(bookManager.GetBooksIdAndName());

                    bookIdString = this.HandleStringParameterInput("Id du livre");
                    book = bookManager.TryGetBook(bookIdString);
                    if (book != null)
                    {
                        parameters = new string[6];

                        parameters[0] = this.HandleStringParameterInput("Nom du livre", true);
                        parameters[1] = this.HandleStringParameterInput("Auteur", true);
                        parameters[2] = this.HandleStringParameterInput("Genre", true);
                        parameters[3] = this.HandleStringParameterInput("Collection", true);
                        parameters[4] = this.HandleStringParameterInput("Date de publication", true);
                        parameters[5] = this.HandleStringParameterInput("Nombre du stock", true);

                        if (bookManager.TryEditBook(book, parameters) == true)
                        {
                            Console.WriteLine($"> Succès le livre {bookIdString} a bien été modifier");
                        }
                        else
                        {
                            Console.WriteLine("> Une erreur est survenue lors de la modification du livre ...");
                        }
                    }
                    else
                    {
                        Console.WriteLine("> Une erreur est survenue lors de la sélection du livre ...");
                    }
                    return true;
                case "détails":
                    Console.WriteLine("> Tapez sur la touche 'Entrée' pour continuer l'affichage des détails d'un livre");
                    Console.WriteLine("> Pour annuler et revenir au menu principal entrez 'précédent' puis tapez sur la touche 'Entrée' \n");

                    // Quitte le menu et le mode détails
                    if (this.HandleStringParameterInput("", true) == "précédent")
                        return true;

                    Console.WriteLine("> Pour afficher les détails d'un livre tapez son Id (numéro avant titre) puis tapez sur la touche 'Entrée': \n");
                    Console.WriteLine(bookManager.GetBooksIdAndName());

                    bookIdString = this.HandleStringParameterInput("Id du livre");
                    book = bookManager.TryGetBook(bookIdString);
                    if (book != null)
                    {
                        Console.WriteLine($"> Information du livre: \n");
                        Console.WriteLine(book.GetDetails() + "\n");
                        IEnumerable<BorrowingRecord> bookRecords = borrowingManager.TryGetBookRecords(book.Id);
                        Console.WriteLine("Nombre d'emprunt: " + bookRecords.Count());
                        Console.WriteLine("Nombre d'emprunt en cours: " + bookRecords.Count(record => !record.HasReturned()) + "\n");
                        foreach (BorrowingRecord record in bookRecords)
                        {
                            member = memberManager.TryGetMember(record.MemberId.ToString());

                            // Si membre est null on continue notre boucle en passant au prochain record (ne dois normalement jamais arrivée)
                            if (member == null)
                                continue;

                            Console.WriteLine($"Emprunté par: {member.GetIdAndName()}, depuis {DateTime.Now.Subtract(record.DateOfBorrow).Days} Jour(s)\nDate de l'emprunt {record.DateOfBorrow}\n");
                        }
                    }
                    else
                    {
                        Console.WriteLine("> Une erreur est survenue lors de la sélection du livre ...");
                    }
                    return true;
                case "précédent":
                    return true;
                default:
                    Console.WriteLine($"> Entrez d'abord une option valide, les options valides sont '{options}' ...");
                    return false;
            }
        }

        /// <summary>
        /// Gère nos options pour le menu membre
        /// </summary>
        /// <param name="options">Sert pour l'affichage des options disponibles au sein du menu membre</param>
        /// <param name="memberManager">Passe une instance de notre class de gestion de membre</param>
        /// <param name="bookManager">Passe une instance de notre class de gestion de livre</param>
        /// <returns>Retourne vrai ou faux selon la validité de l'action de l'utilisateur</returns>
        private bool HandleMemberMenu(string options, MemberManager memberManager, BookManager bookManager)
        {
            string? option = Console.ReadLine();

            // Condition if, si notre ligne est null ou vide alors on affiche un message invitant l'utilisateur à taper une option valide
            if (string.IsNullOrEmpty(option))
            {
                Console.WriteLine($"> Entrez d'abord une option valide, les options valides sont '{options}' ...");
                return false;
            }

            // Découpe le string entré par l'utilisateur, on ne veut récupérer que le 1er mot
            string[] subMenuOption = option.Split(" ");

            // Utiliser plusieurs fois plus bas dans notre switch
            string memberIdString = string.Empty;
            Member? member = null;

            switch (subMenuOption[0].ToLower())
            {
                case "ajouter":
                    Console.WriteLine("> Tapez sur la touche 'Entrée' pour continuer l'ajout d'un membre");
                    Console.WriteLine("> Pour annuler et revenir au menu principal entrez 'précédent' puis tapez sur la touche 'Entrée' \n");

                    // Quitte le menu et le mode ajout
                    if (this.HandleStringParameterInput("", true) == "précédent")
                        return true;

                    Console.WriteLine("> Ajout d'un membre:");

                    string[] parameters = new string[2];

                    parameters[0] = this.HandleStringParameterInput("Nom");
                    parameters[1] = this.HandleStringParameterInput("Prénom");

                    if (memberManager.TryAddMember(parameters) == true)
                    {
                        Console.WriteLine($"> Succès le membre {parameters[0]} {parameters[1]} a bien été ajouter aux membres de la bibliothèque");
                    }
                    else
                    {
                        Console.WriteLine("> Une erreur est survenue lors de l'ajout du membre ...");
                    }

                    return true;
                case "supprimer":
                    Console.WriteLine("> Tapez sur la touche 'Entrée' pour continuer la suppression d'un membre");
                    Console.WriteLine("> Pour annuler et revenir au menu principal entrez 'précédent' puis tapez sur la touche 'Entrée' \n");

                    // Quitte le menu et le mode supprimer
                    if (this.HandleStringParameterInput("", true) == "précédent")
                        return true;

                    Console.WriteLine("> Pour supprimer un membre tapez son Id (numéro avant le nom, prénom) puis tapez sur la touche 'Entrée': \n");

                    Console.WriteLine(memberManager.GetMembersIdAndName());

                    memberIdString = this.HandleStringParameterInput("Id du membre");
                    if (memberManager.TryDeleteMember(memberIdString) == true)
                    {
                        Console.WriteLine($"> Succès le membre {memberIdString} a bien été supprimer des membres de la bibliothèque");
                    }
                    else
                    {
                        Console.WriteLine("> Une erreur est survenue lors de la suppression du membre ...");
                    }
                    return true;
                case "modifier":
                    Console.WriteLine("> Tapez sur la touche 'Entrée' pour continuer la modification d'un membre");
                    Console.WriteLine("> Pour annuler et revenir au menu principal entrez 'précédent' puis tapez sur la touche 'Entrée' \n");

                    // Quitte le menu et le mode modifier
                    if (this.HandleStringParameterInput("", true) == "précédent")
                        return true;

                    Console.WriteLine("> Pour modifier un membre tapez son Id (numéro avant le nom, prénom) puis tapez sur la touche 'Entrée':");
                    Console.WriteLine("> Pour ne rien modifier laissez vide puis tapez sur la touche 'Entrée' et passer au champ suivant:");

                    Console.WriteLine(memberManager.GetMembersIdAndName());

                    memberIdString = this.HandleStringParameterInput("Id du membre");
                    member = memberManager.TryGetMember(memberIdString);
                    if (member != null)
                    {
                        parameters = new string[2];

                        parameters[0] = this.HandleStringParameterInput("Nom du membre", true);
                        parameters[1] = this.HandleStringParameterInput("Prénom", true);
                        if (memberManager.TryEditMember(member, parameters) == true)
                        {
                            Console.WriteLine($"> Succès le membre {memberIdString} a bien été modifier");
                        }
                        else
                        {
                            Console.WriteLine("> Une erreur est survenue lors de la modification du membre ...");
                        }
                    }
                    else
                    {
                        Console.WriteLine("> Une erreur est survenue lors de la sélection du membre ...");
                    }
                    return true;
                case "détails":
                    Console.WriteLine("> Tapez sur la touche 'Entrée' pour continuer l'affichage des détails d'un membre");
                    Console.WriteLine("> Pour annuler et revenir au menu principal entrez 'précédent' puis tapez sur la touche 'Entrée' \n");

                    // Quitte le menu et le mode détails
                    if (this.HandleStringParameterInput("", true) == "précédent")
                        return true;

                    Console.WriteLine("> Pour afficher les détails d'un membre tapez son Id (numéro avant le nom, prénom) puis tapez sur la touche 'Entrée': \n");
                    Console.WriteLine(memberManager.GetMembersIdAndName());

                    memberIdString = this.HandleStringParameterInput("Id du membre");
                    member = memberManager.TryGetMember(memberIdString);
                    if (member != null)
                    {
                        Console.WriteLine($"> Information du membre {member.GetIdAndName()} \n");
                        Console.WriteLine(member.GetDetails() + "\n");

                        if (member.BorrowingRecords.Count() > 0)
                        {
                            Console.WriteLine("> Liste des livres empruntés:");
                            Console.WriteLine(member.GetBorrowedBooksDetails(bookManager));
                        }
                    }
                    else
                    {
                        Console.WriteLine("> Une erreur est survenue lors de la sélection du membre ...");
                    }
                    return true;
                case "précédent":
                    return true;
                default:
                    Console.WriteLine($"> Entrez d'abord une option valide, les options valides sont '{options}' ...");
                    return false;
            }
        }

        /// <summary>
        /// Gère nos options pour le menu emprunt
        /// </summary>
        /// <param name="options">Sert pour l'affichage des options disponibles au sein du menu emprunt</param>
        /// <param name="borrowingManager">Passe une instance de notre class de gestion d'emprunt</param>
        /// <param name="memberManager">Passe une instance de notre class de gestion de membre</param>
        /// <param name="bookManager">Passe une instance de notre class de gestion de livre</param>
        /// <returns>Retourne vrai ou faux selon la validité de l'action de l'utilisateur</returns>
        private bool HandleBorrowMenu(string options, BorrowingManager borrowingManager, MemberManager memberManager, BookManager bookManager)
        {
            string? option = Console.ReadLine();

            // Condition if, si notre ligne est null ou vide alors on affiche un message invitant l'utilisateur à taper une option valide
            if (string.IsNullOrEmpty(option))
            {
                Console.WriteLine($"> Entrez d'abord une option valide, les options valides sont '{options}' ...");
                return false;
            }

            // Découpe le string entré par l'utilisateur, on ne veut récupérer que le 1er mot
            string[] subMenuOption = option.Split(" ");

            // Utiliser plusieurs fois plus bas dans notre switch
            Member? member = null;
            Book? book = null;

            switch (subMenuOption[0].ToLower())
            {
                case "ajouter":
                    Console.WriteLine("> Tapez sur la touche 'Entrée' pour continuer l'ajout d'un emprunt");
                    Console.WriteLine("> Pour annuler et revenir au menu principal entrez 'précédent' puis tapez sur la touche 'Entrée' \n");

                    // Quitte le menu et le mode ajout
                    if (HandleStringParameterInput("", true) == "précédent")
                        return true;

                    Console.WriteLine("> Pour ajouter un emprunt à un membre tapez son Id (numéro avant le nom, prénom) puis tapez sur la touche 'Entrée': \n");
                    Console.WriteLine(memberManager.GetMembersIdAndName());

                    string memberIdString = this.HandleStringParameterInput("Id du membre");
                    member = memberManager.TryGetMember(memberIdString);
                    if (member != null)
                    {
                        Console.WriteLine("> Pour choisir le livre à emprunter tapez son Id (numéro avant le titre) puis tapez sur la touche 'Entrée':");
                        Console.WriteLine("> Pour annuler et revenir au menu principal entrez 0 puis tapez sur la touche 'Entrée': \n");

                        Console.WriteLine(bookManager.GetAvailableBooksIdAndName());

                        string bookIdString = this.HandleStringParameterInput("Id du livre");
                        book = bookManager.TryGetBook(bookIdString);
                        if (book != null && book.IsAvailbale())
                        {
                            // On enlève 1 au stock disponible
                            book.StockAvailable -= 1;
                            borrowingManager.AddRecord(member.Id, book.Id);

                            // Mets à jour la liste d'emprunts du membre
                            member.BorrowingRecords = borrowingManager.TryGetMemberRecords(member.Id);

                            Console.WriteLine($"> Succès le membre {member.GetIdAndName()} a bien emprunté le livre {book.GetIdAndName()}");
                        }
                        else
                        {
                            Console.WriteLine("> Une erreur est survenue lors de la sélection du livre ...");
                        }
                    }
                    else
                    {
                        Console.WriteLine("> Une erreur est survenue lors de la sélection du membre ...");
                    }
                    return true;
                case "retour":
                    Console.WriteLine("> Tapez sur la touche 'Entrée' pour continuer le retour d'un emprunt");
                    Console.WriteLine("> Pour annuler et revenir au menu principal entrez 'précédent' puis tapez sur la touche 'Entrée' \n");

                    // Quitte le menu et le mode retour
                    if (this.HandleStringParameterInput("", true) == "précédent")
                        return true;

                    Console.WriteLine("> Pour retourner un livre tapez l'Id de l'emprunt puis tapez sur la touche 'Entrée': \n");
                    Console.WriteLine("> Liste des emprunts en cours:");
                    Console.WriteLine(borrowingManager.GetBorrowingsInProgress(bookManager, memberManager));

                    string recordIdString = this.HandleStringParameterInput("Id de l'emprunt");
                    BorrowingRecord? borrowingRecord = borrowingManager.TryGetRecord(recordIdString);
                    if (borrowingRecord != null)
                    {
                        member = memberManager.TryGetMember(borrowingRecord.MemberId.ToString());
                        if (member != null)
                        {
                            book = bookManager.TryGetBook(borrowingRecord.BookId.ToString());
                            if (book != null)
                            {
                                // On ajoute 1 au stock disponible
                                book.StockAvailable += 1;
                                // On ajoute la date actuelle à la date de retour
                                borrowingRecord.DateOfReturn = DateTime.Now;

                                // Mets à jour la liste d'emprunts du membre
                                member.BorrowingRecords = borrowingManager.TryGetMemberRecords(member.Id);

                                Console.WriteLine($"> Succès le membre {member.GetIdAndName()} a bien rendu le livre {book.GetIdAndName()}");
                            }
                            else
                            {
                                Console.WriteLine($"> Une erreur est survenue lors du retour du livre, le livre Id {borrowingRecord.BookId} ne semble pas exister ...");
                            }
                        }
                        else
                        {
                            Console.WriteLine($"> Une erreur est survenue lors du retour du livre, le membre Id {borrowingRecord.MemberId} ne semble pas exister ...");
                        }
                    }
                    else
                    {
                        Console.WriteLine("> Une erreur est survenue lors de la sélection de l'emprunt ...");
                    }
                    return true;
                case "précédent":
                    return true;
                default:
                    Console.WriteLine($"> Entrez d'abord une option valide, les options valides sont '{options}' ...");
                    return false;
            }
        }

        /// <summary>
        /// Récupère le texte entré par l'utilisateur dans notre console
        /// </summary>
        /// <param name="parameterName">Nom du paramètre à récupérer sert uniquement pour l'affichage</param>
        /// <param name="allowEmpty">Autorise ou non l'entrée d'un paramètre vide</param>
        /// <returns>Paramètre entré par l'utilisateur</returns>
        private string HandleStringParameterInput(string parameterName, bool allowEmpty = false)
        {
            Console.WriteLine($"> Entrez {parameterName}: ");

            // Récupère le texte entré par l'utilisateur
            string? parameter = Console.ReadLine();

            // Boucle si n'accepte pas un texte vide et que le texte est vide ou null on boucle jusqu'à ce que l'utilisateur entre quelque chose
            while (!allowEmpty && string.IsNullOrEmpty(parameter))
            {
                Console.WriteLine($"> Erreur {parameterName} ne peut pas être vide ...");
                Console.WriteLine($"> Entrez {parameterName}: ");
                parameter = Console.ReadLine();
            }

            // Condition ternaire si accepte un texte vide et que le texte est vide ou null on retourne un string vide sinon on retourne le texte entré par l'utilisateur
            return allowEmpty && string.IsNullOrEmpty(parameter) ? string.Empty : parameter;
        }
    }
}