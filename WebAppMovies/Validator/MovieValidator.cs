using WebAppMovies.DTOs;
using WebAppMovies.MiddleWare;
using WebAppMovies.Repository;

namespace WebAppMovies.Validator
{
    public class MovieValidator : IMovieValidator
    {
        private const int MinYear = 1895;
        private readonly IMovieRepository _MovieRepository;

        public MovieValidator(IMovieRepository repo)
        {
            _MovieRepository = repo;
        }

        /// <summary>
        /// Check businness rules on movie data to create
        /// </summary>
        /// <param name="dto">Structure of data movie to check</param>
        public async Task ValidateAsync(MovieCreateDto dto)
        {
            if (dto == null)
                throw new AppException("Dto is required");

            ValidateBasic(dto);
            await ValidateDirectorAndExistence(dto);
            ValidateActors(dto);
        }

        /// <summary>
        /// Check basic rules on movie data to create
        /// </summary>
        /// <param name="dto">Structure of data movie to check</param>
        private void ValidateBasic(MovieCreateDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Title))
                throw new AppException("Title is required");

            if (dto.ReleaseYear < 1895)
                throw new AppException($"Release year must be >= {MinYear}");

            if (dto.ReleaseYear > DateTime.Now.Year)
                throw new AppException("Release year cannot be in the future");

            if (dto.Budget <= 0)
                throw new AppException("Budget must be > 0");
                      
        }

        /// <summary>
        /// Check Director and existence movie rules on movie data to create
        /// </summary>
        /// <param name="dto">Structure of data movie to check</param>
        private async Task ValidateDirectorAndExistence(MovieCreateDto dto)
        {
            Guid? directorId = null;

            if (dto.Director.Id.HasValue)
            {
                directorId = dto.Director.Id.Value;
            }
            else
            {
                if (dto.Director.NewDirector == null)
                    throw new AppException("Director is required");

                if (string.IsNullOrWhiteSpace(dto.Director.NewDirector.FirstName) ||
                    string.IsNullOrWhiteSpace(dto.Director.NewDirector.LastName))
                    throw new AppException("Director data invalid");

                directorId = await _MovieRepository.DirectorExistAsync(
                    dto.Director.NewDirector.LastName,
                    dto.Director.NewDirector.FirstName
                );
            }

            if (directorId.HasValue)
            {
                bool exists = await _MovieRepository.MovieExistAsync(
                    dto.Title,
                    dto.ReleaseYear,
                    directorId.Value
                );

                if (exists)
                    throw new AppException("Movie already exists");
            }
        }
        /// <summary>
        /// Check actors creation rules on movie data to create
        /// </summary>
        /// <param name="dto">Structure of data movie to check</param>
        private void ValidateActors(MovieCreateDto dto)
        {
            if (dto.Actors == null) return;

            foreach (var actor in dto.Actors)
            {
                if (actor.Id == null && actor.NewActor != null)
                {
                    if (string.IsNullOrWhiteSpace(actor.NewActor.FirstName) ||
                        string.IsNullOrWhiteSpace(actor.NewActor.LastName))
                    {
                        throw new AppException("Actor data invalid");
                    }
                }
            }
        }
    }
}
