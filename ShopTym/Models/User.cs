using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopTym.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        //[Required(ErrorMessage = "User Name Required.")]
        public string UserName { get; set; }
        public string Password { get; set; }
        //[DataType("")]
        public DateTime Dob { get; set; }
        //[EmailAddress]
        public string Email { get; set; }
        //[Phone]
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Image { get; set; }
        public string Roles { get; set; }
    }
}