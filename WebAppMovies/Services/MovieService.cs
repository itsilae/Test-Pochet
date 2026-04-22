using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using WebAppMovies.DTOs;
using WebAppMovies.Mapper;
using WebAppMovies.Models;
using WebAppMovies.Repository;
using WebAppMovies.Validator;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Collections.Specialized.BitVector32;

namespace WebAppMovies.Services
{
    public class MovieService : IMovieService
    {

        private readonly IMovieRepository _MovieRepository;
        private readonly IMovieValidator _MovieValidator;
        private readonly IMovieMapper _MovieMapper;

        public MovieService(IMovieRepository repository, IMovieValidator validator, IMovieMapper mapper)
        {
            _MovieRepository = repository;
            _MovieValidator = validator;
            _MovieMapper = mapper;
        }


        #region MOVIES

        /// <summary>
        /// Get All Movies with filter and sort options
        /// </summary>
        /// <param name="directorId">Filter on Director Identifier</param>
        /// <param name="sortBy">Sort by: title, year, budget or none</param>
        /// <param name="sortAsc">Sort asc if true</param>
        public async Task<List<MovieDto>> GetAllMoviesAsync(Guid? directorId, string? sortBy, bool sortAsc)
        {
            // Get All Movies 
            IQueryable<Movie> queryMovies = _MovieRepository.GetAllMovies();

            //Filter on Director
            if (directorId.HasValue)
                queryMovies = queryMovies.Where(f => f.DirectorId == directorId);

            // Sort Data
            if (!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy.ToLower())
                {
                    case "titre":
                        queryMovies = sortAsc? queryMovies.OrderBy(f => f.Title) : queryMovies.OrderByDescending(f => f.Title);
                        break;
                    case "annee":
                        queryMovies = sortAsc ? queryMovies.OrderBy(f => f.ReleaseYear) : queryMovies.OrderByDescending(f => f.ReleaseYear);
                        break;
                    case "budget":
                        queryMovies = sortAsc ? queryMovies.OrderBy(f => f.Budget) : queryMovies.OrderByDescending(f => f.Budget);
                        break;
                    default:
                        break;
                }
            }

            //Execute SQL request (EF)
            var movies =  queryMovies.ToList();

            //Return Movies format in MovieDto
            return movies.Select(_MovieMapper.ToDto).ToList();

        }

        /// <summary>
        /// Add new Movie
        /// </summary>
        /// <param name="dto">Structure of data movie to create</param>
        public async Task<MovieDto> CreateMovieAsync(MovieCreateDto dto)
        {
            //Validate Data
            await _MovieValidator.ValidateAsync(dto);

            //Detect if Director creation is needed
            Guid directorId = await GetOrCreateDirector(dto.Director);

            //Create new Movie
            var newMovie = new Movie
            {
                Id = Guid.NewGuid(),
                Title = dto.Title,
                ReleaseYear = dto.ReleaseYear,
                Budget = dto.Budget,
                DirectorId = directorId,
            };
            var createdMovieId = await _MovieRepository.AddMovieAsync(newMovie);
                       
          
            //Creation Link Movie Actor is needed
            await AddMovieActors(dto.Actors, createdMovieId);

            //Check Creation Link Movie Kind is needed
            await AddMovieKinds(dto.Kinds, createdMovieId);

            Movie returnMovie = await _MovieRepository.GetMovieAsync(createdMovieId);

            return _MovieMapper.ToDto(returnMovie);
        }

        #endregion


        #region HELPERS
        /// <summary>
        /// Format name or firstName with first capital letter
        /// </summary>
        /// <param name="name">string to format</param>
        public static string FormatName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return name;

            return string.Join("-",
                name
                    .Split('-', StringSplitOptions.RemoveEmptyEntries)
                    .Select(part =>
                        char.ToUpper(part[0]) + part.Substring(1).ToLower()
                    )
            );
        }

        #endregion

        #region PRIVATE
        private async Task<Guid> GetOrCreateDirector(DirectorDto dto)
        {
            if (dto.Id.HasValue)
                return dto.Id.Value;

            return await _MovieRepository.AddDirectorAsync(
                FormatName(dto.NewDirector.LastName),
                FormatName(dto.NewDirector.FirstName));
        }

        private async Task AddMovieActors(List<ActorDto>? actors, Guid movieId)
        {
            if (actors == null || !actors.Any()) return;

            foreach (var a in actors)
            {
                Console.WriteLine($"ID: {a.Id}, FirstName: {a.NewActor?.FirstName}");

                Guid actorId;
                if (a.Id != null)
                    actorId = a.Id.Value;
                else
                    actorId = await _MovieRepository.AddActorAsync( FormatName(a.NewActor.LastName),FormatName(a.NewActor.FirstName));

                             

                await _MovieRepository.AddMovieActorAsync(movieId, actorId);
            }
        }

        private async Task AddMovieKinds(List<KindDto>? kinds, Guid movieId)
        {
            if (kinds == null || !kinds.Any()) return;

            foreach (var k in kinds)
            {
                var kindId = k.Id ??
                    await _MovieRepository.AddKindAsync(FormatName(k.NewKind));

                await _MovieRepository.AddMovieKindAsync(movieId, kindId);
            }
        }

        #endregion
    }
}
