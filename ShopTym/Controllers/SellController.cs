using ShopTym.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopTym.Controllers
{
    public class SellController : Controller
    {
        // GET: Sell
        [Authorize]
        public ActionResult Index()
        {
            ShopTymDBContext context = new ShopTymDBContext();
            if (User.Identity.IsAuthenticated)
            {
                string role = context.Users.Where(u => u.Email == User.Identity.Name).FirstOrDefault().Roles;
                if (role.ToUpper() == "ADMIN") return View(context.CustomerPurchases.ToList());
            }
            
            return RedirectToAction("Login", "Login");
        }
    }
}