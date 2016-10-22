using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace MvcHeribert.Models
{
    public class User
    {
        public int userID { set; get; }

        [Required]
        [StringLength(16, ErrorMessage = "The username must be 6 to 16 characters long.", MinimumLength = 6)]
        public string Username { set; get; }

        [Required]
        [StringLength(16, ErrorMessage = "The password must be 6 to 16 characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [StringLength(45, ErrorMessage = "The email must be 6 to 45 characters long.", MinimumLength = 6)]
        [DataType(DataType.EmailAddress)]
        public string Email { set; get; }


    }
    public class UserDBContext : DbContext
    {
        public DbSet<User> Users { get; set; }
    }
}