
using System;

namespace TabloidCLI.UserInterfaceManagers
{
    internal class ColorManager : IUserInterfaceManager
    {

        private readonly IUserInterfaceManager _parentUI;

        public ColorManager(IUserInterfaceManager parentUI)
        {
            _parentUI = parentUI;
        }

        public IUserInterfaceManager Execute()
        {

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

                case "1":
                    Console.BackgroundColor = ConsoleColor.Blue;
                    return this;
                case "2":
                    Console.BackgroundColor = ConsoleColor.Magenta;
                    return this;
                case "3":
                    Console.BackgroundColor = ConsoleColor.Red;
                    return this;
                case "4":
                    Console.BackgroundColor = ConsoleColor.DarkGreen;
                    return this;
                case "5":
                    Console.BackgroundColor = ConsoleColor.DarkYellow;
                    return this;
                case "6":
                    Console.BackgroundColor = ConsoleColor.DarkCyan;
                    return this;
                case "0":
                    Console.WriteLine("Good bye");
                    return _parentUI;
                default:
                    Console.WriteLine("Invalid Selection");
                    return this;
            }
        }
    }
 }
