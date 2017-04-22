using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopTym.Models
{
    public class Purchase
    {
        [Key]
        public int PurcheseId { get; set; }
        public int ProductId { get; set; }
        public double ProductPrice { get; set; }
        public int ProductQuentity { get; set; }
        public DateTime ProductDate { get; set; }
    }
}