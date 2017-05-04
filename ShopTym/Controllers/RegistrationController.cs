using ShopTym.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopTym.Controllers
{
    public class RegistrationController : Controller
    {
        // GET: Registration
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Signup(User user) {
            HttpPostedFileBase file = Request.Files["Image"];
            user.Roles = "User";
            user.Dob = Convert.ToDateTime(Request.Form["Dob"]);
            string imageName = user.UserName + file.FileName;
            user.Image = imageName;
            file.SaveAs(Path.Combine(Server.MapPath("~/Image/"), imageName));
            ShopTymDBContext context = new ShopTymDBContext();
            context.Users.Add(user);
            context.SaveChanges();
            return RedirectToAction("Login", "Login");
        }
    }
}