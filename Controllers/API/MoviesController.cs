using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vidly7.Data;

namespace Vidly7.Controllers.API
{
	[Route("api/[controller]")]
	[ApiController]
	public class MoviesController : ControllerBase
	{
		private readonly ApplicationDbContext _context;

		public MoviesController(ApplicationDbContext context)
		{
			_context = context;
		}

		//GET /api/movies
		[HttpGet]
		[Route("/api/movies")]
		public IActionResult GetMovies()
		{
			var movies = _context.Movies.
				 Include(m => m.Genre)
				 .ToList();
			return Ok(movies);
		}



	}
}
