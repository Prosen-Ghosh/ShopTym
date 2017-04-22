using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopTym.Models
{
    public class Sell
    {
        [Key]
        public int SellId { get; set; }
        public int ProductId { get; set; }
        public int ProductQuentity { get; set; }
        public double ProductPrice { get; set; }
        public DateTime SellDate { get; set; }
    }
}