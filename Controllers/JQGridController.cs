using Faint.Models;
using Faint.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Faint.Controllers
{
    public class JQGridController : Controller
    {
        // GET: JQGrid
        public JsonResult GetDashboard()
        {
            var userName = Request.Cookies["Username"].Value;
            var model = DatabaseProcesses.GetUserMovies(userName);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public string AddMovie(Movie Movie, decimal Price)
        {
            var userName = Request.Cookies["Username"].Value;
            DatabaseProcesses.AddMovie(userName, Movie.Name, Price);
            return $"Added {Movie.Name} to your movie/s.";
        }

        public string DeleteMovie(Movie Movie)
        {
            var userName = Request.Cookies["Username"].Value;
            DatabaseProcesses.DeleteMovie(Movie);
            return $"Deleted from your movie/s.";
        }

        public string EditMovie(Movie Movie, decimal Price)
        {
            var userName = Request.Cookies["Username"].Value;
            DatabaseProcesses.EditMovie(Movie, Price); //LMAO
            return $"edited.";
        }


        //ADMIN SIDE

        public JsonResult AdminGetDashboard()
        {
            var userName = Request.Cookies["Username"].Value;
            var model = AdminDatabaseProcesses.GetAllMovies();
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public string AdminAddMovie(Movie Movie, decimal Price)
        {
            var userName = Request.Cookies["Username"].Value;
            DatabaseProcesses.AddMovie(userName, Movie.Name, Price);
            return $"Added {Movie.Name} to your movie/s.";
        }

        public string AdminDeleteMovie(Movie Movie)
        {
            var userName = Request.Cookies["Username"].Value;
            DatabaseProcesses.DeleteMovie(Movie);
            return $"Deleted '{Movie.Name}' from your movie/s.";
        }

        public string AdminEditMovie(Movie Movie, decimal Price)
        {
            var userName = Request.Cookies["Username"].Value;
            DatabaseProcesses.EditMovie(Movie, Price); //LMAO
            return $"'{Movie.Name}' edited.";
        }
    }
}
