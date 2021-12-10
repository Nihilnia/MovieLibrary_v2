using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Faint.Models.Database
{
    public class User
    {
        public User()
        {
            this.Movies = new List<Movie>();
        }

        public int ID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EMail { get; set; }
        public string ProfilePic { get; set; }
        public List<Movie> Movies { get; set; }
    }
}