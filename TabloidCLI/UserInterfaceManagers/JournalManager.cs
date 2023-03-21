using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine("Journal Menu");
            Console.WriteLine(" 1) List Journal Entries");
            Console.WriteLine(" 2) Journal Details");
            Console.WriteLine(" 3) Add Journal");
            Console.WriteLine(" 4) Edit Journal");
            Console.WriteLine(" 5) Remove Journal");
            Console.WriteLine(" 6) Note Management");
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
                Console.WriteLine(journal.Title);
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
                Console.WriteLine($" {i + 1}) {journal.Title}");
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
            Console.WriteLine("New Journal Entry");
            Journal journal = new Journal();

            Console.Write("Title: ");
            journal.Title = Console.ReadLine();

            Console.Write("Content: ");
            journal.Content = Console.ReadLine();

            Console.Write("Date Created (YYYY-MM-DD HH:MM:SS): ");
            string createDateTimeStr = Console.ReadLine();
            DateTime createDateTime;
            if (DateTime.TryParse(createDateTimeStr, out createDateTime))
            {
                journal.CreateDateTime = createDateTime;
            }
            else
            {
                Console.WriteLine("Invalid date format!");
                return;
            }


            _journalRepository.Insert(journal);
            Console.WriteLine("Succesfully added");
        }

        private void Edit()
        {
            Journal journalToEdit = Choose("Which Journal Entry would you like to edit?");
            if (journalToEdit == null)
            {
                return;
            }

            Console.WriteLine();
            Console.Write("New title (blank to leave unchanged: ");
            string title = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(title))
            {
                journalToEdit.Title = title;
            }
            Console.Write("New content (blank to leave unchanged: ");
            string content = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(content))
            {
                journalToEdit.Content= content;
            }

            Console.Write("New date time (blank to leave unchanged: ");
            string createDateTimeStr = Console.ReadLine();
            DateTime createDateTime;
            if (DateTime.TryParse(createDateTimeStr, out createDateTime))
            {
                journalToEdit.CreateDateTime = createDateTime;
            }
            else
            {
                Console.WriteLine("Invalid date format!");
                return;
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
