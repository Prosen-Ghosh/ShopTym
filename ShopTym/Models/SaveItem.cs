using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopTym.Models
{
    public class SaveItem
    {
        [Key]
        public int SaveItemId {get; set; }
        public int CustomerId {get; set; }
        public int ProductId {get; set; }
        public int SaveQuentity { get; set; }
    }
}