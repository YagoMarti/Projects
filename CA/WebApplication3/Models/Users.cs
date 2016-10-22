using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebApplication3.Models
{
    public class Users
    {
        public int ID { set; get; }
        [Display(Name = "Username")]
        [Required, MaxLength(25), RegularExpression("^[a-zA-Z0-9]*$")]
        public string username { get; set; }
        [Display(Name = "Password")]
        [Required, MaxLength(25), RegularExpression("^[a-zA-Z0-9]*$")]
        public string password { get; set; }
        [Display(Name = "Email address")]
        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string email { get; set; }
    }

    public class UsersDBContext : ApplicationDbContext
    {
        public DbSet<Users> Users { get; set; }
    }
}