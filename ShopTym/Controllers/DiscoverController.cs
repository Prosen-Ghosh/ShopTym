using ShopTym.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopTym.Controllers
{
    public class DiscoverController : Controller
    {
        // GET: Discover
        public ActionResult Index()
        {
            ShopTymDBContext _context = new ShopTymDBContext();
            ViewBag.Products = _context.Products.ToList();
            return View();
        }

        [HttpPost]
        public ActionResult Search() {
            ShopTymDBContext _context = new ShopTymDBContext();
            string key = Request.Form["search"].ToLower();
            //ViewBag.Products = _context.Products.Where(p => p.CategoryName.ToLower().Contains( Request.Form["search"].ToLower())).ToList();
            ViewBag.Products = (from p in _context.Products where p.CategoryName.ToLower().Contains(key) || p.ProductName.ToLower().Contains(key) select p).ToList();
            return View();
        }
        public ActionResult Item(int id)
        {
            ShopTymDBContext _context = new ShopTymDBContext();
            ViewBag.Products = _context.Products.SingleOrDefault(p => p.ProductId == id);
            return View();
        }
        [HttpPost]
        [Authorize]
        public ActionResult BuyOrSave() {
            ShopTymDBContext context = new ShopTymDBContext();
            if (User.Identity.IsAuthenticated)
            {
                string role = context.Users.Where(u => u.Email == User.Identity.Name).FirstOrDefault().Roles;
                if (role.ToUpper() != "USER") return RedirectToAction("Login", "Login");
            }
            if (Request.Form["SaveItem"] != null){

                var item = (SaveItem)new SaveItem();
                item.ProductId = Convert.ToInt32(Request.Form["productId"]);
                item.SaveQuentity = Convert.ToInt32(Request.Form["Quentity"]);
                item.CustomerId = Convert.ToInt32(Session["UserId"]);
                context.SaveItems.Add(item);
                context.SaveChanges();
            }
            else if(Request.Form["BuyItem"] != null) {
                
                int productId = Convert.ToInt32(Request.Form["productId"]);

                Product product = context.Products.SingleOrDefault(p => p.ProductId == productId);
                product.ProductQuentity -= Convert.ToInt32(Request.Form["Quentity"]);

                context.Products.Attach(product);
                context.Entry(product).Property(x => x.ProductQuentity).IsModified = true;
                context.SaveChanges();

                var customerPurchase = (CustomerPurchase)new CustomerPurchase();
                customerPurchase.ProductId = productId;
                customerPurchase.ProductPrice = product.ProductPrice;
                customerPurchase.ProductQuentity = Convert.ToInt32(Request.Form["Quentity"]);
                customerPurchase.PurchaseDate = DateTime.Now;
                customerPurchase.UserId = Convert.ToInt32(Session["UserId"]);

                context.CustomerPurchases.Add(customerPurchase);
                context.SaveChanges();
                return RedirectToAction("Index", "User");
            }
            return RedirectToAction("Index");
        }
    }
}