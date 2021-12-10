using Faint.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Faint.Models
{
    //GET ALL MOVIES
    public static class AdminDatabaseProcesses
    {
        public static List<Movie> GetAllMovies()
        {
            var allMovies = new List<Movie>();

            using (var DB = new WitchContext())
            {
                allMovies = DB.Movies.ToList();
            }

            return allMovies;
        }


        //DELETE ALL USERS
        public static void DeleteAllUsers()
        {
            using (var DB = new WitchContext())
            {
                var allUsers = DB.Users.Where(f => f.Role == null).ToList();
                foreach (var user in allUsers)
                {
                    DB.Users.Remove(user);
                }
                DB.SaveChanges();
            }
        }

        //ADD Random Users
        public static void AddRandomUsers(int amount)
        {
            using (var DB = new WitchContext())
            {
                for (int f = 0; f < amount; f++)
                {
                    DB.Users.Add(new User()
                    {
                        UserName = "RandomUser_" + f,
                        Password = "123" + f,
                        Role = "User"
                    });
                }

                DB.SaveChanges();
            }
        }


        //ADD RANDOM MOVIES
        public static void AddRandomMovies(int amount)
        {
            using (var DB = new WitchContext())
            {
                for (int f = 0; f < amount; f++)
                {
                    Random rnd = new Random();
                    var firstUser = DB.Users.Where(x => x.Role == "User").First();
                    var lastUser = DB.Users.OrderByDescending(u => u.ID).FirstOrDefault();
                    int randomNumb = rnd.Next(firstUser.ID, lastUser.ID);

                    var theUser = DB.Users.Find(randomNumb);
                    DB.Movies.Add(new Movie() { UserID = theUser.ID, Name = "RandomMov_" + randomNumb + randomNumb });
                }

                DB.SaveChanges();
            }
        }
    }
    
}