using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace MvcMovie.Models
{
    public class Movie
    {
        public int ID { set; get; }
        public string Title { set; get; }

        [Display(Name = "Release Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ReleaseDate { set; get; }
        public string Genre { set; get; }
        public decimal Price { set; get; }
    }

    public class MovieDBContext : DbContext 
    {
        public DbSet<Movie> Movies { set; get; }
    }
}
