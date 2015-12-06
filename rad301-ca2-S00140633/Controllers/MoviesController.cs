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

        // GET: Movies
        public ActionResult Index(string movieGenre, string searchString, string sort)
        {
            ViewBag.PageTitle = db.Movies.Count() +  " Movies with " + db.Actors.Count() + " Actors";
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

        // GET: Movies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
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
            if (ModelState.IsValid)
            {
                db.Movies.Add(movie);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(movie);
        }

        // GET: Movies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MovieId,Title,Cert,Genre")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                db.Entry(movie).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(movie);
        }

        // GET: Movies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Movie movie = db.Movies.Find(id);
            db.Movies.Remove(movie);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public PartialViewResult ActorsbyId(int id)
        {
            var movie = db.Movies.Find(id);
            @ViewBag.MovieId = id;
            @ViewBag.campName = movie.Title;
            return PartialView("_ActorsInMovie", movie.Actors);
        }


        // GET: Movies/Create
        public PartialViewResult CreateActor(int id)
        {
            Actor act = new Actor();
            ViewBag.MovieId = id;
            return PartialView("_AddActor");
        }

        // POST: Movies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public PartialViewResult CreateActor([Bind(Include = "ActorId,FirstName,LastName,Gender,Character,MovieId")] Actor actor, int id)
        {
            var movie = db.Movies.Find(id);
            @ViewBag.MovieId = id;
            
            if (ModelState.IsValid)
            {
                db.Actors.Add(actor);
                db.SaveChanges();
                return PartialView("_ActorsInMovie", movie.Actors);
            }

            return PartialView("_ActorsInMovie");
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
