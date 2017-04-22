using ShopTym.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ShopTym.Controllers
{
    public class CategorieController : Controller
    {
        // GET: Categorie
        public ActionResult Index()
        {
            ShopTymDBContext context = new ShopTymDBContext();

            return View(context.Categories.ToList());
        }

        // GET: Categorie
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Categorie
        [HttpPost]
        public ActionResult Create(Categorie c)
        {
            ShopTymDBContext context = new ShopTymDBContext();
            context.Categories.Add(c);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
        // GET: Categorie
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShopTymDBContext context = new ShopTymDBContext();
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
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShopTymDBContext context = new ShopTymDBContext();
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