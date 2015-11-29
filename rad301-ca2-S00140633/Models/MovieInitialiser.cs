using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace rad301_ca2_S00140633.Models
{
    public class MovieInitialiser : DropCreateDatabaseAlways<MovieDB>
    {
        protected override void Seed(MovieDB context)
        {
            List<Movie> mov = new List<Movie>
            {
                new Movie{ Title="The GodFather", Cert = Certs._18, Genre = GenreType.Crime, Actors = new List<Actor>{
                    new Actor{FirstName = "Al", LastName= "Pacino", Gender=Genders.Male},
                    new Actor{FirstName = "Marlon", LastName="Brando", Gender = Genders.Male },
                    new Actor{FirstName = "Diane", LastName="Keaton", Gender = Genders.Female }}},

                new Movie{ Title="The Terminator", Cert = Certs._15A, Genre = GenreType.Action, Actors = new List<Actor>{
                    new Actor{FirstName = "Arnold", LastName= "Schwarzenegger", Gender=Genders.Male},
                    new Actor{FirstName = "Linda", LastName=" Hamilton", Gender = Genders.Female }}},


                new Movie{ Title="Toy Story", Cert = Certs._PG, Genre = GenreType.Animation, Actors = new List<Actor>{
                    new Actor{FirstName = "Tom", LastName= "Hanks", Gender=Genders.Male},
                    new Actor{FirstName = "Tim", LastName="Allen", Gender = Genders.Male },
                    new Actor{FirstName = "Don", LastName="Rickles", Gender = Genders.Male},
                    new Actor{FirstName = "John", LastName="Morris", Gender = Genders.Male }} },

                new Movie{ Title="Bridesmaids", Cert = Certs._15A, Genre = GenreType.Comedy, Actors = new List<Actor>{
                    new Actor{FirstName = "Kristen", LastName= "Wiig", Gender=Genders.Female},
                    new Actor{FirstName = "Mellisa", LastName="McCarthy", Gender = Genders.Female },
                    new Actor{FirstName = "Rose", LastName="Byrne", Gender = Genders.Female }}},

                new Movie{ Title="The Exorcist", Cert = Certs._18, Genre = GenreType.Crime, Actors = new List<Actor>{
                    new Actor{FirstName = "Linda", LastName= "Blair", Gender=Genders.Female},
                    new Actor{FirstName = "Ellen", LastName="Burstyn", Gender = Genders.Female },
                    new Actor{FirstName = "Jason", LastName="Miller", Gender = Genders.Male },
                    new Actor{FirstName = "Lee", LastName="Cobb", Gender = Genders.Male }
                } }

            };
            mov.ForEach(m => context.Movies.Add(m));
            context.SaveChanges();
        }
    }
}