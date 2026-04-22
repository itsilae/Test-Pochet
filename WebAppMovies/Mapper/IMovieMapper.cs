using WebAppMovies.DTOs;
using WebAppMovies.Models;

namespace WebAppMovies.Mapper
{
    public interface IMovieMapper
    {

        MovieDto ToDto(Movie movie);

        Movie ToEntity(MovieCreateDto dto, Guid directorId);
    }
}
