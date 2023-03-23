using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using TabloidCLI.Models;
using TabloidCLI.Repositories;

namespace TabloidCLI.UserInterfaceManagers
{
    internal class NoteManager : IUserInterfaceManager
    {
        private readonly IUserInterfaceManager _parentUI;
        private NoteRepository _noteRepository;
        private string _connectionString;
        private int _postId;
        private PostRepository _postRepository;

        public NoteManager(IUserInterfaceManager parentUI, string connectionString, int postId)
        {
            _parentUI = parentUI;
            _noteRepository = new NoteRepository(connectionString);
            _connectionString = connectionString;
            _postId = postId;
            _postRepository = new PostRepository(connectionString);
        }

        public IUserInterfaceManager Execute()
        {
            Post post = _postRepository.Get(_postId);

            Console.WriteLine("Note Menu");
            Console.WriteLine(" 1) List Notes for this post");
            Console.WriteLine(" 2) Add Note for this post");
            Console.WriteLine(" 3) Remove Note for this post");
            Console.WriteLine(" 0) Go Back");

            Console.Write("> ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    ListPerPost();
                    return this;
                case "2":
                    Add();
                    return this;
                case "3":
                    Remove();
                    return this;
                case "0":
                    return _parentUI;
                default:
                    Console.WriteLine("Invalid Selection");
                    return this;
            }
        }

        private Note Choose(string prompt = null)
        {
            if (prompt == null)
            {
                prompt = "Please choose an Note:";
            }

            Console.WriteLine(prompt);

            List<Note> notes = _noteRepository.GetAll();

            for (int i = 0; i < notes.Count; i++)
            {
                Note note = notes[i];
                Console.WriteLine($" {i + 1}) {note.Title}");
            }
            Console.Write("> ");

            string input = Console.ReadLine();
            try
            {
                int choice = int.Parse(input);
                return notes[choice - 1];
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid Selection");
                return null;
            }
        }
        private void ListPerPost()
        {
            
            Console.WriteLine("All Notes for this post:");
            Post post = _postRepository.Get(_postId);
            int postId = post.Id;
            List<Note> notes = _noteRepository.GetAllPerPost(postId);
            foreach (Note note in notes)
            {
                Console.WriteLine(note);
            }
        }

        private void Add()
        {
            Console.WriteLine("New Note");
            Note note = new Note();

            Console.Write("Title: ");
            note.Title = Console.ReadLine();

            Console.Write("Content: ");
            note.Content = Console.ReadLine();
            Post post = _postRepository.Get(_postId);
            note.Post = post;

            _noteRepository.Insert(note);
        }

        private void Remove()
        {
            Note noteToDelete = Choose("Which Note would you like to remove?");
            if (noteToDelete != null)
            {
                _noteRepository.Delete(noteToDelete.Id);
                Console.WriteLine("Congrats! You deleted something.");
            }
        }
    }
}
