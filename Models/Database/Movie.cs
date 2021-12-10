using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Faint.Models.Database
{
    public class Movie
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        //public User User { get; set; }
        public int UserID { get; set; }
        public List<Category> Categories { get; set; }
    }
}