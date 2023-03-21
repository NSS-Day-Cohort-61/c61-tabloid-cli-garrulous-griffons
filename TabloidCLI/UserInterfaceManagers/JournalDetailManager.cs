using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabloidCLI.Models;
using TabloidCLI.Repositories;

namespace TabloidCLI.UserInterfaceManagers
{
    internal class JournalDetailManager : IUserInterfaceManager
    {
        private IUserInterfaceManager _parentUI;
        private JournalRepository _journalRepository;
        private int _journalId;

        public JournalDetailManager(IUserInterfaceManager parentUI, string connectionString, int authorId)
        {
            _parentUI = parentUI;
            _journalRepository = new JournalRepository(connectionString);
            _journalId = authorId;
        }

        public IUserInterfaceManager Execute()
        {
            Journal journal = _journalRepository.Get(_journalId);
            Console.WriteLine($"{journal.Title} Details");
            Console.WriteLine(" 1) View");
            Console.WriteLine(" 0) Go Back");
            //Console.WriteLine(" 2) View Blog Posts");


            Console.Write("> ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    View();
                    return this;
                case "0":
                    return _parentUI;
                default:
                    Console.WriteLine("Invalid Selection");
                    return this;
            }


        }

        private void View()
        {
            Journal journal = _journalRepository.Get(_journalId);
            Console.WriteLine($"Title {journal.Title}");
            Console.WriteLine($"Content: {journal.Content}");
            Console.WriteLine($"DateTime: {journal.CreateDateTime}");
      
            Console.WriteLine();
        }



    }
}
