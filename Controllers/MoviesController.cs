using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vidly7.Data;
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

    }
}
