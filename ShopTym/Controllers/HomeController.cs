using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopTym.Models;

namespace ShopTym.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        //[Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            ShopTymDBContext context = new ShopTymDBContext();
            if (User.Identity.IsAuthenticated)
            {
                string role = context.Users.Where(u => u.Email == User.Identity.Name).FirstOrDefault().Roles;

                if (role.ToUpper() == "ADMIN") return RedirectToAction("Index", "Categorie");
                else if (role.ToUpper() == "USER") return RedirectToAction("Index", "User");
                //return RedirectToAction("Login", "Login");
            }

            ViewBag.Products = context.Products.ToList();
            return View();
        }
    }
}