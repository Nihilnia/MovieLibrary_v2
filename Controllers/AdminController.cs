using Faint.Models;
using Faint.Models.Database;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Faint.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult AdminLoginControl(string userName, string Password)
        {
            

            var result = DatabaseProcesses.LoginControl(userName, Password);
            if (result.Role == "Admin")
            {
                HttpCookie cookie = new HttpCookie("Username", userName);
                Response.Cookies.Add(cookie);
                var model = DatabaseProcesses.GetUser(userName);

                //AdminDatabaseProcesses.AddRandomUsers(50);
                //AdminDatabaseProcesses.AddRandomMovies(220);

                return View("~/Views/Admin/Index.cshtml", model);
            }
            else
            {
                return View("FailedLogin");
            }
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(string userName, string passWord)
        {
            var cryptPassword = EncryptMD5.EnryptEm(passWord);

            DatabaseProcesses.Register(userName, cryptPassword, "Admin");
            return View("~/Views/Admin/Login.cshtml");
        }

        public ActionResult Index()
        {
            return View();
        }

        

        public ActionResult Dashboard(string userName)
        {
            var model = DatabaseProcesses.GetUser(userName);
            return View(model);
        }

        public ActionResult AddMovie()
        {
            var userName = Request.Cookies["Username"].Value;
            var model = DatabaseProcesses.GetUser(userName);
            return View(model);
        }

        [HttpPost]
        public ActionResult AddMovie(string userName, string movieName, decimal Price)
        {
            DatabaseProcesses.AddMovie(userName, movieName, Price);
            var model = DatabaseProcesses.GetUser(userName);
            return View("~/Views/Admin/Dashboard.cshtml", model);
        }


        [HttpGet]
        public ActionResult Profile()
        {
            var userName = Request.Cookies["Username"].Value;
            var model = DatabaseProcesses.GetUser(userName);
            return View(model);
        }

        [HttpPost]
        public ActionResult Profile(string Username, string firstName, string lastName, string EMail, string passWord, HttpPostedFileBase file)
        {
            var pFileName = "";

            if (file != null)
            {
                var extension = Path.GetExtension(file.FileName);

                if (extension == ".jpg" || extension == ".png")
                {

                    var fileName = Path.GetFileName(file.FileName);
                    pFileName = fileName;
                    var path = Path.Combine(Server.MapPath("~/Content/UserPictures"), fileName);

                    file.SaveAs(path);

                    DatabaseProcesses.UpdateUser(Username, passWord, firstName, lastName, EMail, pFileName);
                }
                else
                {
                    ViewData["Message"] = "Please select a picture file.";
                }


            }
            else
            {
                var currentSettings = DatabaseProcesses.GetUser(Username);
                DatabaseProcesses.UpdateUser(Username, passWord, firstName, lastName, EMail, currentSettings.ProfilePic);
            }

            var model = DatabaseProcesses.GetUser(Username);


            return View(model);

        }
    }
}