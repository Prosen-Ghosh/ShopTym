using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopTym.Models
{
    public class CustomerPurchase
    {
        [Key]
        public int PurchaseId { get; set; }
        public int ProductId { get; set; }
        public double ProductPrice { get; set; }
        public int ProductQuentity { get; set; }
        public DateTime PurchaseDate { get; set; }
        public int UserId { get; set; }
    }
}