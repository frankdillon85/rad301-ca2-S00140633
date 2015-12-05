using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;


namespace rad301_ca2_S00140633.Models
{
    public enum Certs
    {
        [Display(Name = "G")]
        _G,
        [Display(Name = "PG")]
        _PG,
        [Display(Name = "12A")]
        _12A,
        [Display(Name = "15A")]
        _15A,
        [Display(Name = "16")]
        _16,
        [Display(Name = "18")]
        _18
    }
    public enum GenreType
    {
        Action, Adventure, Animation, Comedy, Crime, Documentary, Drama, Horror, Romance, Thriller, War, Western, Unknown
    };
    public class Movie
    {
        public int MovieId { get; set; }

        [Required]
        public string Title { get; set; }

        public Certs Cert { get; set; }

        public float Rating { get; set; }

        public string FilmCert { get { return Cert.ToString().TrimStart(new char[] { '_' }); }  }
        public GenreType Genre { get; set; }
        public virtual List<Actor> Actors { get; set; }
    }

    public class MovieDB : DbContext
    {
        public MovieDB():base("MovieDB")
        {

        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Actor> Actors { get; set; }
    }

}
