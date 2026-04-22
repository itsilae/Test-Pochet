using WebAppMovies.DTOs;

namespace WebAppMovies.Validator
{
    public interface IMovieValidator
    {
        /// <summary>
        /// Check businness rules on movie data to create
        /// </summary>
        /// <param name="dto">Structure of data movie to check</param>
        Task ValidateAsync(MovieCreateDto dto);
    }
}
