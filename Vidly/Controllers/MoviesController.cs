using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;
using System.Data.Entity;
namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Movies
        public ActionResult Index()
        {
            var movies = db.Movies.Include(m => m.Genre).ToList();  

            
            return View(movies);
        }

        public ActionResult Details(int id)
        {
            var movie = db.Movies.Include(m => m.Genre).Where( m =>m.Id == id).SingleOrDefault();

            if(movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        public ActionResult Create()
        {

            ViewBag.GenerId = new SelectList(db.Genres.ToList(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Movie movie)
        {
            if (ModelState.IsValid)
            {
                movie.DateAdded = DateTime.Now;

                db.Movies.Add(movie);
                db.SaveChanges();
                return RedirectToAction("Index", "Movies");
            }
            ViewBag.GenerId = new SelectList(db.Genres.ToList(), "Id", "Name");
            return View(movie);
        }

        public ActionResult Edit(int id)
        {
            
            var movie = db.Movies.Include(m => m.Genre).Where(m => m.Id == id).SingleOrDefault();
            if(movie == null)
            {
                return HttpNotFound();
            }
            ViewBag.GenreId = new SelectList(db.Genres.ToList(), "Id", "Name", movie.GenreId);

            return View(movie);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Movie movie)
        {
            if (ModelState.IsValid)
            {
                db.Entry(movie).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", new { id = movie.Id });
            }
          
            ViewBag.GenreId = new SelectList(db.Genres.ToList(), "Id", "Name", movie.GenreId);
            return View(movie);
        }


    }
}