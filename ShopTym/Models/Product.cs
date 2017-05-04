using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopTym.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public double ProductPrice { get; set; }
        public int ProductQuentity { get; set; }
        public string ProductImage { get; set; }
        public string Description { get; set; }
        public string Features { get; set; }
        public string CategoryName { get; set; }
    }
}