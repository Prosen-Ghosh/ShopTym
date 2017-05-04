using ShopTym.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ShopTym.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            ShopTymDBContext context = new ShopTymDBContext();
            string role = context.Users.Where(u => u.Email == User.Identity.Name).FirstOrDefault().Roles;
            if (role.ToUpper() == "ADMIN") return View(context.Products.ToList());
            else return RedirectToAction("Login", "Login");
        }
        // GET: Product
        [HttpGet]
        public ActionResult Create()
        {
            ShopTymDBContext context = new ShopTymDBContext();
            if (User.Identity.IsAuthenticated)
            {
                string role = context.Users.Where(u => u.Email == User.Identity.Name).FirstOrDefault().Roles;
                if (role.ToUpper() != "ADMIN") return RedirectToAction("Login", "Login");
            }
            ViewBag.Categories = context.Categories;
            return View();
        }
        // GET: Product
        [HttpPost, ActionName("Create")]
        public ActionResult _Create()
        {
            bool flag = false;

            if (Request["ProductName"] == String.Empty) {
                TempData["ProductErrorCssClass"] = "alert alert-danger";
                TempData["ProductError"] = "Product Name Can Not Be Empty.";
                flag = true;
            }
            else if (!System.Text.RegularExpressions.Regex.IsMatch(Request["ProductPrice"], @"\d"))
            {
                TempData["ProductErrorCssClass"] = "alert alert-danger";
                TempData["ProductError"] = "Latter Not Allowed.";
                flag = true;
            }
            else if (!System.Text.RegularExpressions.Regex.IsMatch(Request["ProductQuentity"], @"\d"))
            {
                TempData["ProductErrorCssClass"] = "alert alert-danger";
                TempData["ProductError"] = "Latter Not Allowed.";
                flag = true;
            }
            if (!flag)
            {
                HttpPostedFileBase file = Request.Files["ProductImage"];
                string path = Path.Combine(Server.MapPath("~/Image/"), file.FileName);

                var p = (Product)new Product();
                p.ProductName = Request["ProductName"];
                p.ProductPrice = Convert.ToDouble(Request["ProductPrice"]);
                p.ProductQuentity = Convert.ToInt32(Request["ProductQuentity"]);
                p.ProductImage = file.FileName;
                p.Description = Request["Description"];
                p.Features = Request["Features"];
                p.CategoryName = Request["CategoryName"];


                file.SaveAs(path);
                ShopTymDBContext context = new ShopTymDBContext();
                context.Products.Add(p);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            else {
                return RedirectToAction("Create");
            }
        }
        // GET: Product
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            ShopTymDBContext context = new ShopTymDBContext();
            if (User.Identity.IsAuthenticated)
            {
                string role = context.Users.Where(u => u.Email == User.Identity.Name).FirstOrDefault().Roles;
                if (role.ToUpper() != "ADMIN") return RedirectToAction("Login", "Login");
            }
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = context.Products.SingleOrDefault(p => p.ProductId == id);
            ViewBag.PImage = product.ProductImage;
            return View(product);
        }
        // GET: Product
        [HttpPost]
        public ActionResult Edit(Product product)
        {
            ShopTymDBContext context = new ShopTymDBContext();

            Product productToUpdate = context.Products.SingleOrDefault(p => p.ProductId == product.ProductId);

            HttpPostedFileBase file = Request.Files["ProductImage"];
            string path = Path.Combine(Server.MapPath("~/Image/"), file.FileName);

            productToUpdate.ProductName = product.ProductName;
            productToUpdate.ProductPrice = product.ProductPrice;
            productToUpdate.ProductQuentity = product.ProductQuentity;
            productToUpdate.ProductImage = file.FileName;
            productToUpdate.Description = product.Description;
            productToUpdate.Features = product.Features;
            productToUpdate.CategoryName = product.CategoryName;


            file.SaveAs(path);
            context.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: Product
        [HttpGet]
        public ActionResult Details(int id)
        {
            ShopTymDBContext context = new ShopTymDBContext();
            Product product = context.Products.SingleOrDefault(p => p.ProductId == id);
            return View(product);
        }
        // GET: Product
        [HttpGet]
        public ActionResult Delete(int id)
        {
            ShopTymDBContext context = new ShopTymDBContext();
            if (User.Identity.IsAuthenticated)
            {
                string role = context.Users.Where(u => u.Email == User.Identity.Name).FirstOrDefault().Roles;
                if (role.ToUpper() != "ADMIN") return RedirectToAction("Login", "Login");
            }
            Product product = context.Products.SingleOrDefault(p => p.ProductId == id);
            return View(product);
        }
        // GET: Product
        [HttpPost,ActionName("Delete")]
        public ActionResult Delete_Confirm(int id)
        {
            ShopTymDBContext context = new ShopTymDBContext();
            Product product = context.Products.SingleOrDefault(p => p.ProductId == id);
            context.Products.Remove(product);
            context.SaveChanges();
            return RedirectToAction("Index","Product");
        }
    }
}