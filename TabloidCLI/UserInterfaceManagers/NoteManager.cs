using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TabloidCLI.Repositories;
using TabloidCLI.UserInterfaceManagers;

namespace TabloidCLI
{
    public class NoteManager : IUserInterfaceManager
    {
        private IUserInterfaceManager _parentUI;
        private NoteRepository _noteRepository;
        private PostRepository _postRepository;
        private string _connectionString;
        private int _postId;
        
        public NoteManager(IUserInterfaceManager parentUI, string connectionString, int postId)
        {
            _parentUI = parentUI;
            _noteRepository = new NoteRepository(connectionString);
            _postRepository = new PostRepository(connectionString);
            _connectionString = connectionString;
            _postId = postId;
        }

        public IUserInterfaceManager Execute()
        {
            Console.WriteLine("Note Menu");
            Console.WriteLine("1) List Note");
            Console.WriteLine("2) Add Note");
            Console.WriteLine("3) Remove Note");
            Console.WriteLine("4) Return");

            Console.Write("> ");
            string choice = Console.ReadLine();

            switch(choice)
            {
                case "1":
                    List();
                    return this;

                case "2":
                    Add();
                    return this;
                case "3":
                    Remove();
                    return this;
                case "4":
                    return _parentUI;
                default:
                    Console.WriteLine("Invalid Selection");
                    return this;
            }

        }

        private void List()
        {
            List<Note> notes = _noteRepository.GetAll();
            foreach (Note note in notes)
            {
                Console.WriteLine($"{note.Title}: {note.Content} ({note.CreateDateTime})");
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

            note.CreateDateTime = DateTime.Now;

          
            note.PostId = _postId;

            _noteRepository.Insert(note);
        }

        private void Remove()
        {
            Console.WriteLine("Which note would you like to delete?");
            Console.WriteLine("0) Return");
            List<Note> notes = _noteRepository.GetAll();
            foreach (Note note in notes)
            {
                Console.WriteLine($"{note.Id}) {note.Title}: {note.Content} ({note.CreateDateTime})");
            }

            int noteToDelete = int.Parse(Console.ReadLine());

            if (noteToDelete > 0 && noteToDelete < notes.Count)
            {
             _noteRepository.Delete(noteToDelete);
            }
            else
            {
                Console.WriteLine();

            }
           
            
        }



    }
}
