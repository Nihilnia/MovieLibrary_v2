using Faint.Models.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Faint.Models
{
    public static class DatabaseProcesses
    {
        //REGISTER
        public static void Register(string daUserName, string daPassWord, string daRole)
        {
            using (var DB = new WitchContext())
            {
                var newUser = new User() { UserName = daUserName, Password = daPassWord, Role = daRole };
                DB.Users.Add(newUser);
                DB.SaveChanges();
            }
        }


        //LOGIN CONTROL
        public static User LoginControl(string daUserName, string daPassWord)
        {
            var result = new User();

            using (var DB = new WitchContext())
            {
                var findUser = DB.Users.Where(f => f.UserName == daUserName).FirstOrDefault();

                if (findUser != null)
                {
                    var cryptPass = EncryptMD5.EnryptEm(daPassWord);

                    if (findUser.Password == cryptPass)
                    {
                        result = findUser;
                    }
                }
            }

            return result;
        }



        //GETTING USER INFOS
        public static User GetUser(string daUserName)
        {
            var theUser = new User();

            using (var DB = new WitchContext())
            {
                theUser = DB.Users.Where(f => f.UserName == daUserName).FirstOrDefault();
            }

            return theUser;
        }


        //UPDATING USER INFOS
        public static void UpdateUser(string daUserName, string daPassWord, string daFirstName, string daLastName, string daEMail, string daProfilePic)
        {
            using (var DB = new WitchContext())
            {
                var theUser = DB.Users.Where(f => f.UserName == daUserName).FirstOrDefault();
                theUser.FirstName = daFirstName;
                theUser.LastName = daLastName;
                theUser.Password = daPassWord;
                theUser.EMail = daEMail;
                theUser.ProfilePic = daProfilePic;

                DB.SaveChanges();
            }
        }


        //GETTING USER MOVIES
        public static List<Movie> GetUserMovies(string daUserName)
        {
            var userMovies = new List<Movie>();

            using (var DB = new WitchContext())
            {
                var theUser = DB.Users.Where(f => f.UserName == daUserName).Include(m => m.Movies).FirstOrDefault();
                userMovies = theUser.Movies.ToList();
            }

            return userMovies;
        }




        //ADD NEW MOVIE
        public static void AddMovie(string daUserName, string daMovieName, decimal daPrice)
        {
            using (var DB = new WitchContext())
            {
                var theUser = DB.Users.Where(f => f.UserName == daUserName).FirstOrDefault();
                theUser.Movies.Add(new Movie() { Name = daMovieName, Price = daPrice });
                DB.SaveChanges();
            }
        }


        //ADD CATEGORY
        public static void AddCategory(Movie daMovie)
        {
            using (var DB = new WitchContext())
            {
                DB.Categories.Add(new Category() { MovieID = daMovie.ID, Name = daMovie.Categories.ToString()});
            }
        }

        //DELETE MOVIE
        public static void DeleteMovie(Movie daMovie)
        {
            using (var DB = new WitchContext())
            {
                DB.Movies.Remove(daMovie);
                DB.SaveChanges();
            }
        }

        //EDIT MOVIE
        public static void EditMovie(Movie daMovie, decimal daPrice)
        {
            using (var DB = new WitchContext())
            {
                var theMovie = DB.Movies.Find(daMovie.ID);
                theMovie.Name = daMovie.Name;
                theMovie.Price = daPrice;
                DB.SaveChanges();
            }
        }


        
    }
}