using ShopTym.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopTym.Controllers
{
    public class ReportController : Controller
    {
        // GET: Report
        [Authorize]
        public ActionResult Index()
        {
            ShopTymDBContext context = new ShopTymDBContext();
            List<int> list = new List<int>();
            list = context.CustomerPurchases.GroupBy(p => p.ProductId).OrderByDescending(P => P.Count()).Select(p => p.Key).ToList();
            //return list[0].ToString();

            List<Product> tmp = new List<Product>();
            List<int> sumQ = new List<int>();
            for (int i = 0; i < list.Count; i++)
            {
                int id = Convert.ToInt32(list[i]);
                tmp.Add(context.Products.SingleOrDefault(p => p.ProductId == id));
                
            }
            
            ViewBag.MaxSell = tmp;
            ViewBag.maxSellQ = sumQ;
            ViewBag.LessQuantity = context.Products.Where(p => p.ProductQuentity < 4).ToList();

            return View();
        }
    }
}