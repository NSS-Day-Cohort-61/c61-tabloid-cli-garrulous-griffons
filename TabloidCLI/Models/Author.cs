﻿using System.Collections.Generic;
using System.Linq;

namespace TabloidCLI.Models
{
    public class Author : ISearch
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Bio { get; set; }
        public List<Tag> Tags { get; set; } = new List<Tag>();

        //testing

        public string FullName
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }
    
        public override string ToString()
        {
            return FullName;
        }
    }
}