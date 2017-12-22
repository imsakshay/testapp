using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using testApp.Models;

namespace testApp.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        public ActionResult AddOrEdit(int id = 0)
        {
            Login loginmodel = new Login();
            return View(loginmodel);
        }
        [HttpPost]
        public ActionResult AddorEdit(Login login)
        {

            using (LoginEntities loginmodel = new LoginEntities())
            {
                if (loginmodel.Logins.Any(x => x.UserName == login.UserName))
                {
                    ViewBag.Dublicate = "User name found ";
                    return View("AddorEdit", login);
                }

                loginmodel.Logins.Add(login);
                loginmodel.SaveChanges();
            }
            ModelState.Clear();
            ViewBag.SuccessMessage = "Registration succesfull";
            return View("AddorEdit", new Login());
        }


        public ActionResult login(Login Log)
        {
            using (LoginEntities loginmodel = new LoginEntities())
            {
                if (loginmodel.Logins.Any(x => x.UserName == Log.UserName) || loginmodel.Logins.Any(x => x.Password == Log.Password))
                {
                    
                    return RedirectToAction("Index", "Home");
                }
                else
                    return View("login");
            }
        }
    }
}