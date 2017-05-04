using ShopTym.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ShopTym.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Auth()
        {
            string userName = Request.Form["userName"];
            string password = Request.Form["password"];

            Response.Write(userName);
            if (userName == "")
            {
                TempData["LoginErrorCssClass"] = "alert alert-info";
                TempData["LoginError"] = "Useer Name Field Can't Be Empty.";
                return RedirectToAction("Login", ViewBag);
            }
            else if (password == "")
            {
                TempData["LoginErrorCssClass"] = "alert alert-info";
                TempData["LoginError"] = "Password Field Can't Be Empty.";
                return RedirectToAction("Login", ViewBag);
            }
            else {
                TempData["LoginErrorCssClass"] = "";
                TempData["LoginError"] = "";
            }
            ShopTymDBContext context = new ShopTymDBContext();
            User user = (User)context.Users.SingleOrDefault(c => c.Email == userName && c.Password == password);
            if (user == null)
            {
                TempData["LoginErrorCssClass"] = "alert alert-danger";
                TempData["LoginError"] = "Useer Name Or Password Is Incorrect.";
                return RedirectToAction("Login", ViewBag);
            }
            else {
                FormsAuthentication.SetAuthCookie(user.Email, false);
                if (user.Roles.ToUpper() == "ADMIN")
                {
                    return RedirectToAction("Index", "Categorie");
                }
                else if (user.Roles.ToUpper() == "USER") {
                    
                    Session["UserName"] = user.Email;
                    Session["UserId"] = user.UserId;
                    return RedirectToAction("Index", "User");
                }
            }
            return RedirectToAction("Login");
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}