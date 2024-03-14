using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vidly7.Data;
using Vidly7.Models;
using Vidly7.ViewModels;

namespace Vidly7.Controllers
{
    public class MoviesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MoviesController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Details(int id) 
        {
            var movie = _context.Movies.
                Include(m => m.Genre)
                .SingleOrDefault(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }
            return
                View(movie);
        }

        [Route("/movies/new")]
        public IActionResult New() 
        {
            var genreName = _context.Genre.ToList();
            var viewModel = new MovieFormViewModel
            {
                Genres = genreName,
            };
            return View("MovieForm", viewModel);
        }

        public IActionResult Edit(int id)
        {
            var movie = _context.Movies.SingleOrDefault(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }
            var viewModel = new MovieFormViewModel(movie)
            {
                Genres = _context.Genre.ToList(),
            };
            return View("MovieForm", viewModel);
        }

        [HttpPost]
        [Route("movies/save")]
        public IActionResult Save(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new MovieFormViewModel(movie)
                {
                    Genres = _context.Genre.ToList()
                };
                return View("MovieForm", viewModel);
            }

            if (movie.Id == 0)
            {
                _context.Movies.Add(movie);
            }
            else 
            {
                var movieFromDb = _context.Movies.Single(m => m.Id == movie.Id);
                   
            }
            _context.SaveChanges();

            return RedirectToAction("Index", "Movies");

        }

        //movies/random
        public IActionResult Random()
        {
            var movie = new Movie()
            {
                Name = "Shrek!"
            };

            var customers = new List<Customer>
            {
               new Customer { Name = "Customer 1"},
               new Customer { Name = "Customer 2"}
            };

            var viewModel = new RandomMovieViewModel
            {
                Movie = movie,
                Customers = customers
            };

            return View(viewModel);
        }

        [Route("movies/released/{year}/{month:regex(\\d{{2}}: range(1,12))}")]
        public IActionResult ByReleaseDate(int year, int month)
        {
            return Content(year + "/" + month);
        }

        public ActionResult NotRandom()
        {
            var movie = new Movie()
            {
                Name = "Not Shrek!"
            };
            return View(movie);
        }
    }
}
