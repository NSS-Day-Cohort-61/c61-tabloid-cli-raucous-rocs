using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabloidCLI.Models;

namespace TabloidCLI.UserInterfaceManagers
{
    internal class NoteManager : IUserInterfaceManager
    {
        private readonly IUserInterfaceManager _parentUI;
        private NoteRepository _noteRepository;
        private string _connectionString;

        public NoteManager(IUserInterfaceManager parentUI, string connectionString)
        {
            _parentUI = parentUI;
            _noteRepository = new NoteRepository(connectionString);
            _connectionString = connectionString;
        }

        public IUserInterfaceManager Execute()
        {
            Console.WriteLine("Journal Menu");
            Console.WriteLine(" 1) List Notes for this post");
            Console.WriteLine(" 2) Add Note for this post");
            Console.WriteLine(" 3) Remove Note for this post");
            Console.WriteLine(" 0) Go Back");

            Console.Write("> ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    List();
                    return this;
                case "2":
                    Add();
                    return this;
                case "3":
                    Edit();
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
            Console.WriteLine("All Notes for this post:");
            List<Note> notes = _noteRepository.GetByPost();
            foreach (Journal journal in journals)
            {
                Console.WriteLine(journal);
            }
        }
    }
}
