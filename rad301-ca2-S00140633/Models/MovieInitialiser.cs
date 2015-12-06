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
                new Movie{ Title="The GodFather", Cert = Certs._18, Genre = GenreType.Crime,Rating= 5.0f, Actors = new List<Actor>{
                    new Actor{FirstName = "Al", LastName= "Pacino",Character = "Michael Corleone" ,Gender=Genders.Male},
                    new Actor{FirstName = "Marlon", LastName="Brando",Character = "Vito Corleone", Gender = Genders.Male },
                    new Actor{FirstName = "Diane", LastName="Keaton",Character = "Kay Adams-Corleone", Gender = Genders.Female }}},

                new Movie{ Title="The Terminator", Cert = Certs._15A, Genre = GenreType.Action,Rating= 4.0f, Actors = new List<Actor>{
                    new Actor{FirstName = "Arnold", LastName= "Schwarzenegger",Character = "Terminator", Gender=Genders.Male},
                    new Actor{FirstName = "Linda", LastName=" Hamilton",Character = "Sarah Connor" ,Gender = Genders.Female }}},


                new Movie{ Title="Toy Story", Cert = Certs._PG, Genre = GenreType.Animation,Rating= 4.5f, Actors = new List<Actor>{
                    new Actor{FirstName = "Tom", LastName= "Hanks",Character = "Sheriff Woody" , Gender=Genders.Male},
                    new Actor{FirstName = "Tim", LastName="Allen",Character = "Buzz Lightyear" , Gender = Genders.Male },
                    new Actor{FirstName = "Don", LastName="Rickles",Character = "Mr Potato Head" , Gender = Genders.Male},
                    new Actor{FirstName = "John", LastName="Morris",Character = "Andy" , Gender = Genders.Male }} },

                new Movie{ Title="Bridesmaids", Cert = Certs._15A, Genre = GenreType.Comedy,Rating= 3.5f, Actors = new List<Actor>{
                    new Actor{FirstName = "Kristen", LastName= "Wiig",Character = "Annie Walker" , Gender=Genders.Female},
                    new Actor{FirstName = "Mellisa", LastName="McCarthy",Character = "Megan" , Gender = Genders.Female },
                    new Actor{FirstName = "Rose", LastName="Byrne",Character = "Helen" , Gender = Genders.Female }}},

                new Movie{ Title="The Exorcist", Cert = Certs._18, Genre = GenreType.Horror,Rating= 4.5f, Actors = new List<Actor>{
                    new Actor{FirstName = "Linda", LastName= "Blair",Character = "Regan McNeil" , Gender=Genders.Female},
                    new Actor{FirstName = "Ellen", LastName="Burstyn",Character = "Chris McNeil" , Gender = Genders.Female },
                    new Actor{FirstName = "Jason", LastName="Miller",Character = "Damian Karras" , Gender = Genders.Male },
                    new Actor{FirstName = "Lee", LastName="Cobb",Character = "Lt William Kinderman" , Gender = Genders.Male }
                } }

            };
            mov.ForEach(m => context.Movies.Add(m));
            context.SaveChanges();
        }
    }
}