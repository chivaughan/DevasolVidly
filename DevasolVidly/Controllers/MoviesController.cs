using DevasolVidly.Models;
using DevasolVidly.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace DevasolVidly.Controllers
{
    [Authorize(Roles = RoleName.CanManageMovies)]
    public class MoviesController : Controller
    {
        private ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Movie movie)
        {
            if (movie.Id == 0)
            {
                _context.Movies.Add(movie);
            }
            else
            {
                var movieInDb = _context.Movies.Single(m => m.Id == movie.Id);
                movieInDb.Name = movie.Name;
                movieInDb.GenreId = movie.GenreId;
                movieInDb.ReleaseDate = movie.ReleaseDate;
                movieInDb.DateAdded = movie.DateAdded;
                movieInDb.StockQuantity = movie.StockQuantity;
            }
            _context.SaveChanges();
            return RedirectToAction("Index", "Movies");
        }
        public ActionResult Edit(int id)
        {
            var movie = _context.Movies.SingleOrDefault(m => m.Id == id);
            if (movie == null)
                return HttpNotFound();
            else
            {
                var viewModel = new MovieFormViewModel()
                {
                    Movie = movie,
                    Genres = _context.Genres
                };
                return View("MovieForm",viewModel);
            }
        }

        
        public ActionResult MovieForm()
        {
            var viewModel = new MovieFormViewModel()
            {
                Genres = _context.Genres.ToList(),
                Movie = new Movie() 
                { 
                    DateAdded = DateTime.Now,
                    ReleaseDate = DateTime.Now,
                },
            };
            return View(viewModel);
        }

        [Route("movies/details/{id}")]
        public ActionResult Details(int id)
        {
            try
            {
                var movies = _context.Movies.Include(c => c.Genre).SingleOrDefault(c => c.Id == id);
                return View(movies);
            }
            catch(Exception ex)
            {
                return HttpNotFound();
            }
            
        }
        

        //public ActionResult Index(int? pageIndex, string sortBy)
        //{
        //    // If no page index has been specified, page index should be set to 1
        //    if (!pageIndex.HasValue)
        //    {
        //        pageIndex = 1;
        //    }
        //    if (string.IsNullOrEmpty(sortBy))
        //    {
        //        sortBy = "Name";
        //    }
        //    return Content(string.Format("pageIndex={0}&sortBy={1}",pageIndex,sortBy));
        //}

        public ActionResult Index()
        {
            if (User.IsInRole(RoleName.CanManageMovies))
            {
                var movies = _context.Movies.Include(c => c.Genre).ToList();

                var moviesList = new MovieListViewModel { Movies = movies };
                return View("List", moviesList);
            }
            else
            {
                var movies = _context.Movies.Include(c => c.Genre).ToList();

                var moviesList = new MovieListViewModel { Movies = movies };
                return View("ReadOnlyList", moviesList);
            }
            
        }

        [Route("movies/released/{year:regex(\\d{4}):range(2010,2022)}/{month:regex(\\d{2}):range(1,12)}")]
        public ActionResult ByReleaseDate(int year, int month)
        {
            return Content(year + "/" + month);
        }
    }
}