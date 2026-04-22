using WebAppMovies.DTOs;

namespace WebAppMovies.Services
{
    public interface IMovieService
    {


        /// <summary>
        /// Get All Movies with filter and sort options
        /// </summary>
        /// <param name="directorId">Filter on Director Identifier</param>
        /// <param name="sortBy">Sort by: title, year, budget or none</param>
        /// <param name="sortAsc">Sort asc if true</param>
        Task<List<MovieDto>> GetAllMoviesAsync(Guid? directorId, string? sortBy, bool sortAsc);

        /// <summary>
        /// Add new Movie
        /// </summary>
        /// <param name="dto">Structure of data movie to create</param>
        Task<MovieDto> CreateMovieAsync(MovieCreateDto dto);

    }
}
