using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ShopTym.Models
{
    public class ShopTymDBContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<CustomerPurchase> CustomerPurchases { get; set; }
        public DbSet<SaveItem> SaveItems { get; set; }
        public DbSet<Categorie> Categories { get; set; }
        public DbSet<CustomerReview> CustomerReviews { get; set; }
        public DbSet<Sell> Sells { get; set; }
    }
}