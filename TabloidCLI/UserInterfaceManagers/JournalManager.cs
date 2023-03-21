using System;
using System.Collections.Generic;
using TabloidCLI.Models;

namespace TabloidCLI.UserInterfaceManagers
{
    public class JournalManager : IUserInterfaceManager
    {
        private readonly IUserInterfaceManager _parentUI;
        private JournalRepository _journalRepository;
        private string _connectionString;

        public JournalManager(IUserInterfaceManager parentUI, string connectionString)
        {
            _parentUI = parentUI;
            _journalRepository = new JournalRepository(connectionString);
            _connectionString = connectionString;
        }

        public IUserInterfaceManager Execute()
        {
            Console.WriteLine("Journal Menu");
            Console.WriteLine(" 1) List Journals");
            Console.WriteLine(" 2) Journal Details");
            Console.WriteLine(" 3) Add Journal");
            Console.WriteLine(" 4) Edit Journal");
            Console.WriteLine(" 5) Remove Journal");
            Console.WriteLine(" 0) Go Back");

            Console.Write("> ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    List();
                    return this;
                case "2":
                    Journal journal = Choose();
                    if (journal == null)
                    {
                        return this;
                    }
                    else
                    {
                        return new JournalDetailManager(this, _connectionString, journal.Id);
                    }
                case "3":
                    Add();
                    return this;
                case "4":
                    Edit();
                    return this;
                case "5":
                    Remove();
                    return this;
                case "0":
                    return _parentUI;
                default:
                    Console.WriteLine("Invalid Selection");
                    return this;
            }
        }

        private void List()
        {
            List<Journal> journals = _journalRepository.GetAll();
            foreach (Journal journal in journals)
            {
                Console.WriteLine(journal);
            }
        }

        private Journal Choose(string prompt = null)
        {
            if (prompt == null)
            {
                prompt = "Please choose an Journal:";
            }

            Console.WriteLine(prompt);

            List<Journal> journals = _journalRepository.GetAll();

            for (int i = 0; i < journals.Count; i++)
            {
                Journal journal = journals[i];
                Console.WriteLine($" {i + 1}) {journal.FullName}");
            }
            Console.Write("> ");

            string input = Console.ReadLine();
            try
            {
                int choice = int.Parse(input);
                return journals[choice - 1];
            }
            catch (Exception ex)
            {
                Console.WriteLine("Invalid Selection");
                return null;
            }
        }

        private void Add()
        {
            Console.WriteLine("New Journal");
            Journal journal = new Journal();

            Console.Write("First Name: ");
            journal.FirstName = Console.ReadLine();

            Console.Write("Last Name: ");
            journal.LastName = Console.ReadLine();

            Console.Write("Bio: ");
            journal.Bio = Console.ReadLine();

            _journalRepository.Insert(journal);
        }

        private void Edit()
        {
            Journal journalToEdit = Choose("Which journal would you like to edit?");
            if (journalToEdit == null)
            {
                return;
            }

            Console.WriteLine();
            Console.Write("New first name (blank to leave unchanged: ");
            string firstName = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(firstName))
            {
                journalToEdit.FirstName = firstName;
            }
            Console.Write("New last name (blank to leave unchanged: ");
            string lastName = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(lastName))
            {
                journalToEdit.LastName = lastName;
            }
            Console.Write("New bio (blank to leave unchanged: ");
            string bio = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(bio))
            {
                journalToEdit.Bio = bio;
            }

            _journalRepository.Update(journalToEdit);
        }

        private void Remove()
        {
            Journal journalToDelete = Choose("Which journal would you like to remove?");
            if (journalToDelete != null)
            {
                _journalRepository.Delete(journalToDelete.Id);
            }
        }
    }
}

