using System;
using System.Collections.Generic;
using TabloidCLI.Models;
using TabloidCLI.Repositories;

namespace TabloidCLI.UserInterfaceManagers
{
    public class PostManager : IUserInterfaceManager
    {
        private readonly IUserInterfaceManager _parentUI;
        private PostRepository _postRepository;
        private AuthorRepository _authorRepository;
        private BlogRepository _blogRepository;
        private string _connectionString;

        public PostManager(IUserInterfaceManager parentUI, string connectionString)
        {
            _parentUI = parentUI;
            _postRepository = new PostRepository(connectionString);
            _authorRepository = new AuthorRepository(connectionString);
            _blogRepository = new BlogRepository(connectionString);
            _connectionString = connectionString;
        }

        public IUserInterfaceManager Execute()
        {
            Console.WriteLine("Post Menu");
            Console.WriteLine(" 1) List Posts");
            Console.WriteLine(" 2) Post Details");
            Console.WriteLine(" 3) Add Post");
            Console.WriteLine(" 4) Edit Post");
            Console.WriteLine(" 5) Remove Post");
            Console.WriteLine(" 0) Go Back");

            Console.Write("> ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    List();
                    return this;
                case "2":
                    Post post = Choose();
                    if (post == null)
                    {
                        return this;
                    }
                    else
                    {
                        return new PostDetailManager(this, _connectionString, post.Id);
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
            List<Post> posts= _postRepository.GetAll();
            foreach (Post post in posts)
            {
                Console.WriteLine(post);
            }
        }

        private Post Choose(string prompt = null)
        {
            if (prompt == null)
            {
                prompt = "Please choose a Post:";
            }

            Console.WriteLine(prompt);

            List<Post> posts = _postRepository.GetAll();

            for (int i = 0; i < posts.Count; i++)
            {
                Post post = posts[i];
                Console.WriteLine($" {i + 1}) {post.Title}");
            }
            Console.Write("> ");

            string input = Console.ReadLine();
            try
            {
                int choice = int.Parse(input);
                return posts[choice - 1];
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid Selection");
                return null;
            }
        }

        private void Add()
        {
            Console.WriteLine("New Post");
            Post post = new Post();

            Console.Write("Title: ");
            post.Title = Console.ReadLine();

            Console.Write("Url: ");
            post.Url = Console.ReadLine();


            bool enterCorrectDate = false;
            do
            {
                Console.Write("Publish Date (yyyy-MM-dd HH:mm:ss): ");
                string publishDateInput = Console.ReadLine();

                DateTime publishDate;

                if (DateTime.TryParse(publishDateInput, out publishDate))
                {
                    post.PublishDateTime = publishDate;
                    enterCorrectDate = true;
                }
                else
                {
                    Console.WriteLine("Invalid date format. Please try again.");
                }
            }
            while (enterCorrectDate == false);

            Console.WriteLine("Choose an author");
            List<Author> allAuthors = _authorRepository.GetAll();
            foreach (Author thisAuthor in allAuthors)
            {
                Console.WriteLine($"{thisAuthor.Id} - {thisAuthor.FirstName} {thisAuthor.LastName}");
            }
            Author author = new Author()
            {
                Id = int.Parse(Console.ReadLine())
            };
            post.Author = author;

            Console.WriteLine("Choose a blog");
            List<Blog> allBlogs = _blogRepository.GetAll();
            foreach (Blog thisBlog in allBlogs)
            {
                Console.WriteLine($"{thisBlog.Id} - {thisBlog.Title} {thisBlog.Url}");
            }
            Blog Blog = new Blog()
            {
                Id = int.Parse(Console.ReadLine())
            };
            post.Blog = Blog;

            _postRepository.Insert(post);
        }


        private void Edit()
        {
            Post postToEdit = Choose("Which post would you like to edit?");
            if (postToEdit == null)
            {
                return;
            }

            Console.WriteLine();
            Console.Write("New title (blank to leave unchanged): ");
            string title = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(title))
            {
                postToEdit.Title= title;
            }
            Console.Write("New Url (blank to leave unchanged): ");
            string url = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(url))
            {
                postToEdit.Url = url;
            }

            Console.WriteLine("New author (blank to leave unchanged): ");
            List<Author> authors = _authorRepository.GetAll();
            foreach (Author thisAuthor in authors)
            {
                Console.WriteLine($"{thisAuthor.Id} - {thisAuthor.FirstName} {thisAuthor.LastName}");
            }
            string authorId = (Console.ReadLine());
            if (!string.IsNullOrWhiteSpace(authorId))
            {
                {
                    Author author = new Author
                    {
                        Id = int.Parse(authorId),
                    };
                    postToEdit.Author = author;
                }
            }

            Console.WriteLine("New blog (blank to leave unchanged): ");
            List<Blog> blogs = _blogRepository.GetAll();
            foreach (Blog b in blogs)
            {
                Console.WriteLine($"{b.Id} - {b.Title}");
            }

            string blogId = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(blogId))
            {
                {
                    Blog blog = new Blog
                    {
                        Id = int.Parse(blogId),
                    };
                    postToEdit.Blog = blog;
                }
            }

            _postRepository.Update(postToEdit);
        }

        private void Remove()
        {
            Post postToDelete = Choose("Which post would you like to remove?");
            if (postToDelete != null)
            {
                _postRepository.Delete(postToDelete.Id);
            }
        }
    }
}
