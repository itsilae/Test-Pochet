using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAppMovies.DTOs;
using WebAppMovies.Services;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebAppMovies.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : Controller
    {
        private readonly IMovieService _MovieService;

        public MoviesController(IMovieService movieService)
        {
            _MovieService = movieService;
        }


        /// <summary>
        /// Get All Movies with filter and sort options
        /// </summary>
        /// <param name="directorId">Filter on Director Identifier</param>
        /// <param name="sortBy">Sort by: title, year, budget or none</param>
        /// <param name="sortAsc">Sort asc if true</param>
        // GET: MoviesController
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll([FromQuery] Guid? directorId, [FromQuery] string? sortBy, [FromQuery] bool sortAsc)
        {
            List<MovieDto> result = await _MovieService.GetAllMoviesAsync(directorId, sortBy, sortAsc);

            return Ok(result);
        }

        /// <summary>
        /// Add new Movie
        /// </summary>
        /// <param name="dto">Structure of data movie to create</param>
        // POST: MoviesController
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody]  MovieCreateDto dto)
        {
            
            var result = await _MovieService.CreateMovieAsync(dto);

            return Ok(result);
        }
    }
}
