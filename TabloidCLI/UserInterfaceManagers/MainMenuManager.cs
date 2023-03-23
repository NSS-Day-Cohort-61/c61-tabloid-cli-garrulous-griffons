using System;

namespace TabloidCLI.UserInterfaceManagers
{
    public class MainMenuManager : IUserInterfaceManager
    {
        private const string CONNECTION_STRING = 
            @"Data Source=localhost\SQLEXPRESS;Database=TabloidCLI;Integrated Security=True";

        public IUserInterfaceManager Execute()
        {
            Console.WriteLine("The Garrulous Griffons Would Like to Welcome You to Our App!");


            Console.WriteLine("What Background Color Would you like?");
            Console.WriteLine(" 1) Blue");
            Console.WriteLine(" 2) Magenta");
            Console.WriteLine(" 3) Red");
            Console.WriteLine(" 4) Dark Green");
            Console.WriteLine(" 5) Dark Yellow");
            Console.WriteLine(" 6) Dark Cyan");
            Console.WriteLine(" 0) Exit");

            Console.Write("> ");
            string colorChoice = Console.ReadLine();
            switch (colorChoice)
            {

                case "1": Console.BackgroundColor = ConsoleColor.Blue;
                    break;
                case "2":  Console.BackgroundColor = ConsoleColor.Magenta;
                    break;
                case "3": Console.BackgroundColor = ConsoleColor.Red;
                    break;
                case "4": Console.BackgroundColor = ConsoleColor.DarkGreen;
                    break;
                case "5": Console.BackgroundColor = ConsoleColor.DarkYellow;
                    break;
                case "6": Console.BackgroundColor = ConsoleColor.DarkCyan;
                    break;
                case "0":
                    Console.WriteLine("Good bye");
                    return null;
                default:
                    Console.WriteLine("Invalid Selection");
                    return this;
            }

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
