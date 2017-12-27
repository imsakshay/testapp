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

        [HttpGet]
        public ActionResult login()
        {
            Login Log = new Login();
            return View(Log);
        }

        [HttpPost]
        public ActionResult login(Login Log)
        {
            using (LoginEntities loginmodel = new LoginEntities())
            {
                if (loginmodel.Logins.Any(x => x.UserName == Log.UserName) && loginmodel.Logins.Any(x => x.Password == Log.Password))
                {
                    
                    return RedirectToAction("Index", "Home");
                }

                var d = loginmodel.Logins.Where(x => x.UserName == Log.UserName);//  .Any(x => x.UserName != Log.UserName);

                
                if (d.Count()==0)
                {
                    ViewBag.Dublicate = "Wrong Username";
                    return View("login", Log);
                }
                //else if (loginmodel.Logins.Any(x => x.UserName != Log.UserName))
                //{
                //    ViewBag.Dublicate = "Wrong Username";
                //    return View("login", Log);
                //}
                if (loginmodel.Logins.Any(x => x.Password != Log.Password))
                {
                    ViewBag.SuccessMessage = "Wrong Password";
                    return View("login", Log);
                }
                else
                    return View("login",Log);
            }
        }
    }
}