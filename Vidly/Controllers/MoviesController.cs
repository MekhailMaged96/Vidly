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
    

    }
}