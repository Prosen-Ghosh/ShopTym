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
    public class CategorieController : Controller
    {
        // GET: Categorie
        public ActionResult Index()
        {
             
            ShopTymDBContext context = new ShopTymDBContext();
            if (User.Identity.IsAuthenticated)
            {
                string role = context.Users.Where(u => u.Email == User.Identity.Name).FirstOrDefault().Roles;
                if (role.ToUpper() == "ADMIN") return View(context.Categories.ToList());
            }
            return RedirectToAction("Login","Login");
        }

        // GET: Categorie
        [HttpGet]
        public ActionResult Create()
        {
            ShopTymDBContext context = new ShopTymDBContext();
            if (User.Identity.IsAuthenticated)
            {
                string role = context.Users.Where(u => u.Email == User.Identity.Name).FirstOrDefault().Roles;
                if (role.ToUpper() != "ADMIN") return RedirectToAction("Login", "Login");
            }
            return View();
        }

        // POST: Categorie
        [HttpPost]
        public ActionResult Create(Categorie c)
        {
            bool flag = false;

            if (c.CategoryName == String.Empty || c.CategoryName == null )
            {
                TempData["CategoryErrorCssClass"] = "alert alert-danger";
                TempData["CategoryError"] = "Category Name Can not empty.";
                flag = true;
            }
            else if(c.CategoryName != null && System.Text.RegularExpressions.Regex.IsMatch(c.CategoryName,@"\d")) {
                TempData["CategoryErrorCssClass"] = "alert alert-danger";
                TempData["CategoryError"] = "Category Name Can not Contain Numbers.";
                flag = true;
            }
            if (!flag) {
                ShopTymDBContext context = new ShopTymDBContext();
                context.Categories.Add(c);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            else return RedirectToAction("Create");
        }
        // GET: Categorie
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
            Categorie categorie =  context.Categories.SingleOrDefault(c => c.CategoryId == id);
            return View(categorie);
        }
        // POST: Categorie
        [HttpPost]
        public ActionResult Edit(Categorie categorie)
        {
            ShopTymDBContext context = new ShopTymDBContext();
            //Categorie categorieToUpdate = context.Categories.SingleOrDefault(c => c.CategoryId == categorie.CategoryId);
            if (ModelState.IsValid) {
                
                //categorieToUpdate.CategoryName = categorie.CategoryName;
                context.Entry(categorie).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(categorie);
        }
        // GET: Categorie
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            ShopTymDBContext context = new ShopTymDBContext();
            if (User.Identity.IsAuthenticated)
            {
                string role = context.Users.Where(u => u.Email == User.Identity.Name).FirstOrDefault().Roles;
                if (role.ToUpper() != "ADMIN") return RedirectToAction("Login", "Login");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categorie categorie = context.Categories.SingleOrDefault(c => c.CategoryId == id);
            return View(categorie);
        }
        // POST: Categorie
        [HttpPost,ActionName("Delete")]
        public ActionResult Delete_Confirm(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShopTymDBContext context = new ShopTymDBContext();
            Categorie categorie = context.Categories.SingleOrDefault(c => c.CategoryId == id);
            context.Categories.Remove(categorie);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}