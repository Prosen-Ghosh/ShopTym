using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopTym.Models
{
    public class CustomerReview
    {
        [Key]
        public int ReviewId { get; set; }
        public string ReviewMessage { get; set; }
        public double Stars { get; set; }
        public int ProductId { get; set; }
        public int CustomerId { get; set; }
    }
}