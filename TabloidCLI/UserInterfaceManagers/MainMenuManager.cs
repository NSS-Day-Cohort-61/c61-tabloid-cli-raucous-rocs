using System;

namespace TabloidCLI.UserInterfaceManagers
{
    public class MainMenuManager : IUserInterfaceManager
    {
        private const string CONNECTION_STRING = 
            @"Data Source=localhost\SQLEXPRESS;Database=TabloidCLI;Integrated Security=True";
        
        public IUserInterfaceManager Execute()
        {
            Console.WriteLine("Choose a text color:");
            Console.WriteLine("1. Black");
            Console.WriteLine("2. Red");
            Console.WriteLine("3. Green");
            Console.WriteLine("4. Yellow");
            Console.WriteLine("5. Blue");
            Console.WriteLine("6. Magenta");
            Console.WriteLine("7. Cyan");
            Console.WriteLine("8. White");

            Console.Write("Enter a number (1-8): ");
            int colorChoice = int.Parse(Console.ReadLine());

            switch (colorChoice)
            {
                case 1:
                    Console.ForegroundColor = ConsoleColor.Black;
                    break;
                case 2:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case 3:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case 4:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case 5:
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;
                case 6:
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    break;
                case 7:
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    break;
                case 8:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }

            Console.WriteLine("This is your new console color!");

            Console.WriteLine("Choose a console background color:");
            Console.WriteLine("1. Black");
            Console.WriteLine("2. Red");
            Console.WriteLine("3. Green");
            Console.WriteLine("4. Yellow");
            Console.WriteLine("5. Blue");
            Console.WriteLine("6. Magenta");
            Console.WriteLine("7. Cyan");
            Console.WriteLine("8. White");

            Console.Write("Enter a number (1-8): ");
            int backgroundColorChoice = int.Parse(Console.ReadLine());

            switch (backgroundColorChoice)
            {
                case 1:
                    Console.BackgroundColor = ConsoleColor.Black;
                    break;
                case 2:
                    Console.BackgroundColor = ConsoleColor.Red;
                    break;
                case 3:
                    Console.BackgroundColor = ConsoleColor.Green;
                    break;
                case 4:
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    break;
                case 5:
                    Console.BackgroundColor = ConsoleColor.Blue;
                    break;
                case 6:
                    Console.BackgroundColor = ConsoleColor.Magenta;
                    break;
                case 7:
                    Console.BackgroundColor = ConsoleColor.Cyan;
                    break;
                case 8:
                    Console.BackgroundColor = ConsoleColor.White;
                    break;
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }

            Console.WriteLine("This is your new console color!");
            Console.ReadLine();

            Console.WriteLine("Main Menu");

            Console.WriteLine(" 1) Journal Management");
            Console.WriteLine(" 2) Blog Management");
            Console.WriteLine(" 3) Author Management");
            Console.WriteLine(" 4) Post Management");
            Console.WriteLine(" 5) Tag Management");
            Console.WriteLine(" 6) Search by Tag");
            Console.WriteLine(" 0) Exit");

            Console.Write("> ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1": return new JournalManager(this, CONNECTION_STRING);
                case "2": return new BlogManager(this, CONNECTION_STRING);
                case "3": return new AuthorManager(this, CONNECTION_STRING);
                case "4": return new PostManager(this, CONNECTION_STRING);
                case "5": return new TagManager(this, CONNECTION_STRING);
                case "6": return new SearchManager(this, CONNECTION_STRING);
                case "0":
                    Console.WriteLine("Good bye");
                    return null;
                default:
                    Console.WriteLine("Invalid Selection");
                    return this;
            }
        }
    }
}
