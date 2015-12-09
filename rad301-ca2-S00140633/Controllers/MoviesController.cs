using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using rad301_ca2_S00140633.Models;

namespace rad301_ca2_S00140633.Controllers
{
    public class MoviesController : Controller
    {
        private MovieDB db = new MovieDB();

        #region CRUD operations For Actors
        // GET: Movies
        public ActionResult Index(string movieGenre, string searchString, string sort)
        {

            if (sort == null) sort = "ascRating";
            ViewBag.ratingOrder = (sort == "ascRating") ? "descRating" : "ascRating";

            IQueryable<Movie> movies = db.Movies;
            switch (sort)
            {

                case "ascRating":
                    ViewBag.dateOrder = "descRating";
                    movies = movies.OrderByDescending(c => c.Rating).Include(c => c.Actors);
                    break;
                case "descRating":
                    ViewBag.numberOrder = "ascRating";
                    movies = movies.OrderBy(c => c.Rating).Include(c => c.Actors);
                    break;
                default:
                    ViewBag.numberOrder = "ascNumber";
                    movies = movies.OrderBy(c => c.Rating).Include(c => c.Actors);
                    break;
            }

            var GenreQry = (from m in db.Movies
                            orderby m.Genre
                            select m.Genre).Distinct();

            ViewBag.movieGenre = new SelectList(GenreQry);


            if (!String.IsNullOrEmpty(searchString))
            {
                movies = movies.Where(s => s.Title.Contains(searchString));
            }

            if (!string.IsNullOrEmpty(movieGenre))
            {
                movies = movies.Where(x => x.Genre.ToString() == movieGenre);
            }

            return View(movies.ToList());
        }
        //partial view to show number of Movies and actors
        public PartialViewResult HeaderDetails()
        {
            ViewBag.PageTitle = db.Movies.Count() + " Movies with " + db.Actors.Count() + " Actors";
            return PartialView("_NumberOfMovies");
        }


        
        //partial view list of movies
        public PartialViewResult ListOfMovies()
        {
            return PartialView("_AllMovies", db.Movies.ToList());
        }
        //GET: Movies/Details/5
        public PartialViewResult Details(int? id)
        {

            if (id == null)
            {
                return PartialView("_Error");
            }
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return PartialView("_Error");
            }

            return PartialView("_MovieDetails", movie);
        }

        // GET: Movies/Create
        public PartialViewResult CreateMovie()
        {
            return PartialView("_CreateMovie");
        }

        // POST: Movies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateMovie([Bind(Include = "MovieId,Title,Cert,Genre,Rating")] Movie movie)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Movies.Add(movie);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View("_Error");
            }
            catch
            {
                return View("_Error");
            }
        }

        // GET: Movies/Edit/5
        public PartialViewResult Edit(int? id)
        {
            if (id == null)
            {
                return PartialView("_Error");
            }
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return PartialView("_Error");
            }
            return PartialView("_EditMovie", movie);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MovieId,Title,Cert,Genre,Rating")] Movie movie)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(movie).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View("_Error");
            }
            catch
            {
                return View("_Error");
            }
        }

        // GET: Movies/Delete/5
        public PartialViewResult Delete(int? id)
        {
            if (id == null)
            {
                return PartialView("_Error");
            }
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return PartialView("_Error");
            }
            return PartialView(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost]
        public ActionResult DeleteMovieConfirmed(int id)
        {
            Movie movie = db.Movies.Find(id);
            db.Movies.Remove(movie);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        #endregion

        #region Actors Crud Operations
        public PartialViewResult ActorsbyId(int id)
        {
            var movie = db.Movies.Find(id);
            @ViewBag.MovieId = id;
            @ViewBag.MovieTitle = movie.Title;
            return PartialView("_ActorsInMovie", movie.Actors);
        }


        // GET: Movies/CreateActor
        public PartialViewResult CreateActor(int id)
        {
            Movie mov = db.Movies.Find(id);
            ViewBag.MovieTitle = mov.Title;

            Actor act = new Actor();
            ViewBag.MovieId = id;
            return PartialView("_AddActor");
        }

        // POST: Movies/CreateActor
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public PartialViewResult CreateActor([Bind(Include = "ActorId,FirstName,LastName,Gender,Character,MovieId")] Actor actor, int id)
        {
            try {
                var movie = db.Movies.Find(id);
                @ViewBag.MovieId = id;
                ViewBag.MovieTitle = movie.Title;

                if (ModelState.IsValid)
                {
                    db.Actors.Add(actor);
                    db.SaveChanges();
                    return PartialView("_ActorsInMovie", movie.Actors);
                }

                return PartialView("_Error");
            }
            catch
            {
                return PartialView("_Error");
            }
        }
        // GET: Movies/Edit/5
        public PartialViewResult EditActor(int? id)
        {

            if (id == null)
            {
                return PartialView("_Error");
            }
            Actor actor = db.Actors.Find(id);
            if (actor == null)
            {
                return PartialView("_Error");
            }
            return PartialView("_EditActor", actor);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public PartialViewResult EditActor([Bind(Include = "ActorId,FirstName,LastName,Gender,Character,MovieId")] Actor actor)
        {
            try
            {
                var movie = db.Movies.Find(actor.MovieId);
                @ViewBag.MovieId = movie.MovieId;
                ViewBag.MovieTitle = movie.Title;

                if (ModelState.IsValid)
                {
                    db.Entry(actor).State = EntityState.Modified;
                    db.SaveChanges();
                    return PartialView("_ActorsInMovie", movie.Actors);
                }
                return PartialView("_Error");
            }
            catch
            {
                return PartialView("_Error");
            }
        }
        public PartialViewResult DeleteActor(int? id)
        {
            if (id == null)
            {
                return PartialView("_Error");
            }
            Actor actor = db.Actors.Find(id);
            if (actor == null)
            {
                return PartialView("_Error");
            }
            return PartialView("_DeleteActor", actor);
        }

        // POST: Movies/DeleteActorConfirmed/5
        [HttpPost]
        public PartialViewResult DeleteActorConfirmed(int id)
        {
            Actor actor = db.Actors.Find(id);

            var movie = db.Movies.Find(actor.MovieId);
            @ViewBag.MovieId = movie.MovieId;
            ViewBag.MovieTitle = movie.Title;

            db.Actors.Remove(actor);
            db.SaveChanges();
            return PartialView("_ActorsInMovie", movie.Actors);
        }
        #endregion

        //partial view for pie chart
        public PartialViewResult showPieChart(int? id)
        {
            Movie movie = db.Movies.Find(id);

            ViewBag.GenderMale = (movie.Actors.Count(act => act.Gender == Genders.Male) == 0) ? 0 : movie.Actors.Count(act => act.Gender == Genders.Male);
            ViewBag.GenderFemale = (movie.Actors.Count(act => act.Gender == Genders.Female) == 0) ? 0 : movie.Actors.Count(act => act.Gender == Genders.Female);

            return PartialView("_PieChart", movie);
        }


        public PartialViewResult GetModalMovieDetails(int id)
        {
            Movie movie = db.Movies.Find(id);
            return PartialView("_ModalPreview", movie);
        }

        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
