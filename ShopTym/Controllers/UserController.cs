using ShopTym.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ShopTym.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            ShopTymDBContext context = new ShopTymDBContext();
            if (User.Identity.IsAuthenticated)
            {
                string role = context.Users.Where(u => u.Email == User.Identity.Name).FirstOrDefault().Roles;
                if (role.ToUpper() != "USER") return RedirectToAction("Login", "Login");
            }
            ViewBag.Products = context.Products.ToList();
            return View();
        }

        public ActionResult SaveItems()
        {
            ShopTymDBContext context = new ShopTymDBContext();
            if (User.Identity.IsAuthenticated)
            {
                string role = context.Users.Where(u => u.Email == User.Identity.Name).FirstOrDefault().Roles;
                if (role.ToUpper() != "USER") return RedirectToAction("Login", "Login");
            }
            return View(context.SaveItems.ToList());
        }
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            ShopTymDBContext context = new ShopTymDBContext();
            if (User.Identity.IsAuthenticated)
            {
                string role = context.Users.Where(u => u.Email == User.Identity.Name).FirstOrDefault().Roles;
                if (role.ToUpper() != "USER") return RedirectToAction("Login", "Login");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            SaveItem items = context.SaveItems.SingleOrDefault(s => s.SaveItemId == id);
            return View(items);
        }
        [HttpPost,ActionName("Delete")]
        public ActionResult Delete_C(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShopTymDBContext context = new ShopTymDBContext();
            SaveItem item = context.SaveItems.SingleOrDefault(s => s.SaveItemId == id);
            context.SaveItems.Remove(item);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete_P(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShopTymDBContext context = new ShopTymDBContext();
            CustomerPurchase items = context.CustomerPurchases.SingleOrDefault(s => s.PurchaseId == id);
            return View(items);
        }

        [HttpPost, ActionName("Delete_P")]
        public ActionResult Delete_PP(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShopTymDBContext context = new ShopTymDBContext();
            CustomerPurchase item = context.CustomerPurchases.SingleOrDefault(s => s.PurchaseId == id);
            context.CustomerPurchases.Remove(item);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Purchase()
        {
            ShopTymDBContext context = new ShopTymDBContext();
            if (User.Identity.IsAuthenticated)
            {
                string role = context.Users.Where(u => u.Email == User.Identity.Name).FirstOrDefault().Roles;
                if (role.ToUpper() != "USER") return RedirectToAction("Login", "Login");
            }

            return View(context.CustomerPurchases.ToList());
        }

        [HttpGet]
        public ActionResult Me()
        {
            ShopTymDBContext context = new ShopTymDBContext();
            if (User.Identity.IsAuthenticated)
            {
                string role = context.Users.Where(u => u.Email == User.Identity.Name).FirstOrDefault().Roles;
                if (role.ToUpper() != "USER") return RedirectToAction("Login", "Login");
            }
            string uname = Session["UserName"].ToString();
            User user = context.Users.SingleOrDefault(u => u.Email == uname);
            ViewBag.Name = user.UserName;
            ViewBag.Email = user.Email;
            ViewBag.Address = user.Address;
            ViewBag.Phone = user.Phone;
            ViewBag.Image = user.Image;
            return View();
        }
    }
}