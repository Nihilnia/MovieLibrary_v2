using Faint.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Faint.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Login()
        {
            //DatabaseProcesses.AddRandomUsers(50);
            //DatabaseProcesses.AddRandomMovies(200);
            return View();
        }

        [HttpPost]
        public ActionResult LoginControl(string userName, string passWord)
        {
            var result = DatabaseProcesses.LoginControl(userName, passWord);
            if (result.Role == "User")
            {
                var cryptUser = EncryptMD5.EnryptEm(userName);
                HttpCookie cookie = new HttpCookie("Username", userName);
                Response.Cookies.Add(cookie);
                var model = DatabaseProcesses.GetUser(userName);
                return View("~/Views/User/Index.cshtml", model);
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
        public ActionResult Register(string userName, string Password)
        {
            var cryptPassword = EncryptMD5.EnryptEm(Password);

            DatabaseProcesses.Register(userName, cryptPassword, "User");
            return RedirectToAction("Login", "Home");
        }


        
    }
}