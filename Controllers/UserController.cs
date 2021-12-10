using Faint.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Faint.Controllers
{
    public class UserController : Controller
    {
        // GET: User
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
            return View("~/Views/User/Dashboard.cshtml", model);
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