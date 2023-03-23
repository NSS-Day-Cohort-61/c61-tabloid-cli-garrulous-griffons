using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabloidCLI.Models;
using TabloidCLI.Repositories;

namespace TabloidCLI.UserInterfaceManagers
{
    internal class PostManager : IUserInterfaceManager
    {
        private readonly IUserInterfaceManager _parentUI;
        private PostRepository _postRepository;
        private AuthorRepository _authorRepository;
        private BlogRepository _blogRepository;
        private string _connectionString;
        public int _authorId;
        public int _blogId;

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
            Console.WriteLine(" 2) Add Post");
            Console.WriteLine(" 3) Edit Post");
            Console.WriteLine(" 4) Remove Post");
            Console.WriteLine(" 5) Note Management");
            Console.WriteLine(" 0) Return to Main Menu");

            Console.Write("> ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    Post chosenPost = List();
                    if (chosenPost == null)
                    {
                        return this;
                    }
                    else
                    {
                        return new PostDetailManager(this, _connectionString, chosenPost.Id);
                    }
                case "2":
                    Add();
                    return this;
                case "3":
                    Edit();
                    return this;
                case "4":
                    Remove();
                    return this;
                case "5":
                    //remove();
                    return this;
                case "0":
                    return _parentUI;
                default:
                    Console.WriteLine("invalid selection");
                    return this;
            }
        }

        private Post List()
        {

            List<Post> postsForDetails = _postRepository.GetAll();
            foreach (Post p in postsForDetails)
            {
                Console.WriteLine($" {p.Id}) {p.Title} : {p.Url}");
            }
            Console.WriteLine("");
            Console.Write("Enter the Id # of the post you would like to see ");
            int id = int.Parse(Console.ReadLine());
            Post post = _postRepository.Get(id);
            return post;
        }

        private void Add()
        {
            Console.WriteLine("New Post");
            Post post = new Post();

            Console.Write("Title: ");
            post.Title = Console.ReadLine();
           
            Console.Write("URL: ");
            post.Url = Console.ReadLine();

            Console.Write("Publish Date: ");
            DateTime userdate = DateTime.Parse(Console.ReadLine());
            post.PublishDateTime = userdate;

            List<Author> authors = _authorRepository.GetAll();
            foreach (Author author in authors)
            {
                Console.WriteLine($"{author.Id}: {author.FirstName} {author.LastName}");
            }
            Console.Write("Select an author by Id #: ");
            post.Author = authors[int.Parse(Console.ReadLine()) - 1];

            List<Blog> blogs = _blogRepository.GetAll();
            foreach (Blog blog in blogs)
            {
                Console.WriteLine($"{blog.Id}: {blog.Title}");
            }
            Console.Write("Select a blog by Id #: ");
            post.Blog = blogs[int.Parse(Console.ReadLine()) - 1];


            _postRepository.Insert(post);

            Console.WriteLine($"{post.Title} has been added.");
            Console.Write("Press any key to continue");
            Console.ReadKey();
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
            catch (Exception ex)
            {
                Console.WriteLine("Invalid Selection");
                return null;
            }
        }

        private void Edit()
        {
            Post postToEdit = Choose("Choose which post you would like to edit?");
            if (postToEdit == null)
            {
                return;
            }

            Console.WriteLine();
            Console.Write("New Title (blank to leave unchanged): ");
            string title = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(title))
            {
                postToEdit.Title = title;
            }
            Console.Write("Url (blank to leave unchanged): ");
            string url = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(url))
            {
                postToEdit.Url = url;
            }

            Console.Write("PublishDateTime (blank to leave unchanged): ");
            DateTime datetime = DateTime.Parse(Console.ReadLine());
                postToEdit.PublishDateTime = datetime;

            List<Author> authors = _authorRepository.GetAll();
            foreach (Author author in authors)
            {
                Console.WriteLine($"{author.Id}: {author.FirstName} {author.LastName}");
            }
            Console.Write("Update the author by Id # (blank to leave unchanged): ");
            postToEdit.Author = authors[int.Parse(Console.ReadLine()) - 1];

            List<Blog> blogs = _blogRepository.GetAll();
            foreach (Blog blog in blogs)
            {
                Console.WriteLine($"{blog.Id}: {blog.Title}");
            }
            Console.Write("Update the blog by Id #: ");
            postToEdit.Blog = blogs[int.Parse(Console.ReadLine()) - 1];

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
