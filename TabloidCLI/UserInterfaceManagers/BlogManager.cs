using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabloidCLI.Models;

namespace TabloidCLI.UserInterfaceManagers
{
    public class BlogManager : IUserInterfaceManager
    {
        private readonly IUserInterfaceManager _parentUI;
        private BlogRepository _blogRepository;
        private string _connectionString;

        public BlogManager(IUserInterfaceManager parentUI, string connectionString)
        {
            _parentUI = parentUI;
            _blogRepository = new BlogRepository(connectionString);
            _connectionString = connectionString;
        }

        public IUserInterfaceManager Execute()
        {
            Console.WriteLine("Blog Menu");
            Console.WriteLine(" 1) List Blogs");
            Console.WriteLine(" 2) Blog Details");
            Console.WriteLine(" 3) Add Blog");
            Console.WriteLine(" 4) Edit Blog");
            Console.WriteLine(" 5) Remove Blog");
            Console.WriteLine(" 0) Go Back");



            Console.Write("> ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                //All blogs
                case "1":
                    List<Blog> blogs = _blogRepository.GetAll();
                    foreach (Blog b in blogs)
                    {
                        Console.WriteLine($" {b.Id}) {b.Title} : {b.Url}");
                    }
                    Console.Write("Press any key to continue");
                    Console.ReadKey();
                    return this;
                //Blog details
                case "2":
                    List<Blog> blogsForDetails = _blogRepository.GetAll();
                    foreach (Blog b in blogsForDetails)
                    {
                        Console.WriteLine($" {b.Id}) {b.Title} : {b.Url}");
                    }
                    Console.WriteLine("");
                    Console.Write("Enter the number of the blog you would like to see. ");
                    int id = int.Parse(Console.ReadLine());

                    Blog blog = _blogRepository.Get(id);
                    if (blog == null)
                    {
                        return this;
                    }
                    else
                    {
                        return new BlogDetailManager(this, _connectionString, blog.Id);
                    }
                //Add blog
                case "3":
                    Console.Write("Blog title: ");
                    string blogTitle = Console.ReadLine();
                    Console.Write("Blog URL: ");
                    string blogUrl = Console.ReadLine();

                    Blog blogToAdd = new Blog()
                    {
                        Title = blogTitle,
                        Url = blogUrl
                    };

                    _blogRepository.Insert(blogToAdd);
                    Console.WriteLine($"{blogToAdd.Title} has been added and assigned an Id of {blogToAdd.Id}.");
                    Console.Write("Press any key to continue");
                    Console.ReadKey();
                    return this;
               //Update blog details
                case "4":
                    Console.WriteLine("Which blog would you like to update?");
                    List<Blog> blogUpdateList = _blogRepository.GetAll();
                    foreach (Blog b in blogUpdateList)
                    {
                        Console.WriteLine($" {b.Id}) {b.Title} : {b.Url}");
                    }
                    
                    int blogId = int.Parse(Console.ReadLine());
                    Blog selectedBlog = blogUpdateList.FirstOrDefault(b => b.Id == blogId);

                    Console.Write(" New Title: ");
                    selectedBlog.Title = Console.ReadLine();

                    Console.Write(" New Url: ");
                    selectedBlog.Url = Console.ReadLine();

                    _blogRepository.Update(selectedBlog);

                    Console.WriteLine("Blog has been successfully updated");
                    Console.Write("Press any key to continue");
                    Console.ReadKey();
                    return this;

                //Delete blog
                case "5":
                    Console.WriteLine("Which blog would you like to delete?");
                    List<Blog> blogDeleteList = _blogRepository.GetAll();
                    foreach (Blog b in blogDeleteList)
                    {
                        Console.WriteLine($" {b.Id}) {b.Title} : {b.Url}");
                    }
                    
                    int blogIdDelete = int.Parse(Console.ReadLine());

                    _blogRepository.Delete(blogIdDelete);
                    Console.WriteLine("Blog has been successfully deleted");

                    Console.Write("Press any key to continue");
                    Console.ReadKey();
                    return this;
                case "0":
                    return _parentUI;
                default:
                    Console.WriteLine("Invalid Selection");
                    return this;
            }
        }
    }
}
