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
            ShopTymDBContext _context = new ShopTymDBContext();
            //var a = _context.Products.ToList();

            return View();
        }
    }
}