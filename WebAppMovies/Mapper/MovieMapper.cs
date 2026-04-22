using WebAppMovies.DTOs;
using WebAppMovies.Models;

namespace WebAppMovies.Mapper
{
    public class MovieMapper : IMovieMapper
    {
        public MovieDto ToDto(Movie movie)
        {
            return new MovieDto
            {
                Id = movie.Id,
                Title = movie.Title,
                ReleaseYear = movie.ReleaseYear,
                Budget = movie.Budget,

                Director = movie.Director != null
                    ? $"{movie.Director.FirstName} {movie.Director.LastName}"
                    : "",

                Actors = movie.MovieActors?
                    .Select(a => $"{a.Actor.FirstName} {a.Actor.LastName}")
                    .ToList() ?? new List<string>(),

                Kinds = movie.MovieKinds?
                    .Select(k => k.Kind.Description)
                    .ToList() ?? new List<string>()
            };
        }

        public Movie ToEntity(MovieCreateDto dto, Guid directorId)
        {
            return new Movie
            {
                Id = Guid.NewGuid(),
                Title = dto.Title,
                ReleaseYear = dto.ReleaseYear,
                Budget = dto.Budget,
                DirectorId = directorId
            };
        }
    }
}
