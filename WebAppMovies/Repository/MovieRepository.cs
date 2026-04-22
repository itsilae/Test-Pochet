using Microsoft.EntityFrameworkCore;
using System.IO;
using WebAppMovies.Data;
using WebAppMovies.Models;

namespace WebAppMovies.Repository
{

    public class MovieRepository: IMovieRepository
    {
        private readonly MoviesDbContext _Context;

        /// <summary>
        /// Initializes a new instance of MovieRepository
        /// </summary>
        /// <param name="context">DB context.</param>
        public MovieRepository(MoviesDbContext context)
        {
            _Context = context;
        }

        #region MOVIES
        /// <summary> 
        /// Get All Movies 
        /// </summary>
        public IQueryable<Movie> GetAllMovies()
        {
            return _Context.Movies
            .Include(f => f.Director)
            .Include(f => f.MovieActors).ThenInclude(ma => ma.Actor)
            .Include(f => f.MovieKinds).ThenInclude(mk => mk.Kind);
        }
        /// <summary>
        /// Get Movie by id
        /// </summary>
        /// <param name="movieId">Movie Identifier </param>
        public async Task<Movie> GetMovieAsync(Guid movieId)
        {
            return await _Context.Movies
                    .Where(m => m.Id == movieId)
                    .Include(m => m.Director)
                    .Include(m => m.MovieActors).ThenInclude(ma => ma.Actor)
                    .Include(m => m.MovieKinds).ThenInclude(mk => mk.Kind)
                    .FirstAsync();
               ;
        }

        /// <summary>
        /// Add new Movie
        /// </summary>
        /// <param name="movie">movie to create </param> 
        public async Task<Guid> AddMovieAsync(Movie movie)
        {
            _Context.Movies.Add(movie);
            await _Context.SaveChangesAsync();
            return movie.Id;
        }

        /// <summary>
        /// Check Movie exist
        /// </summary>
        /// <param name="title">movie title </param>  
        /// <param name="releaseYear">movie release date </param>
        /// <param name="directorId">Director Identifier </param>
        public async Task<bool> MovieExistAsync(string title, int releaseYear, Guid directorId)
        {
            return await _Context.Movies.AnyAsync(m =>
            m.Title.ToLower() == title.ToLower() &&
            m.ReleaseYear == releaseYear &&
            m.DirectorId == directorId);
        }
        #endregion

        #region DIRECTORS

        /// <summary>
        /// Add new Director
        /// </summary>
        /// <param name="lastName">Director lastname </param>  
        /// <param name="firstName">Director firstname </param>
        public async Task<Guid> AddDirectorAsync(string lastName, string firstName)
        {
            //Check if Director already exist
            Guid? foundDirectorId =  await DirectorExistAsync(lastName, firstName);  

            if (foundDirectorId == null)
            {
                //Create a new Director
                Guid newGuid = Guid.NewGuid();
                Director newDirector = new Director { Id = newGuid, LastName = lastName, FirstName = firstName };

                _Context.Directors.Add(newDirector);
                await _Context.SaveChangesAsync();

                return newDirector.Id;
            }
            else
                return foundDirectorId.Value;
        }

        /// <summary>
        /// Check Direcor exist
        /// </summary>
        /// <param name="lastName">Director lastname </param>  
        /// <param name="firstName">Director firstname </param>
        public async Task<Guid?> DirectorExistAsync(string lastName, string firstName)
        {
            Director? director = await _Context.Directors.FirstOrDefaultAsync(d =>
            d.LastName.ToLower() == lastName.ToLower() &&
            d.FirstName.ToLower() == firstName.ToLower());

            return director == null ? null : director.Id;
        }
        #endregion

        #region ACTORS
        /// <summary>
        /// Add new Actor
        /// </summary>
        /// <param name="lastName">Actor lastname </param>  
        /// <param name="firstName">Actor firstname </param>
        public async Task<Guid> AddActorAsync(string lastName, string firstName)
        {
            //Check if Actor already exist
            Guid? foundActorId = await ActorExistAsync(lastName, firstName);
         
            if (foundActorId == null)
            {
                //Create a new Actor
                Guid newGuid = Guid.NewGuid();
                Actor newActor = new Actor { Id = newGuid, LastName = lastName, FirstName = firstName };

                _Context.Actors.Add(newActor);
                await _Context.SaveChangesAsync();

                return newActor.Id;
            }
            else
                return foundActorId.Value;
        }

        /// <summary>
        /// Check Actor exist
        /// </summary>
        /// <param name="lastName">Actor lastname </param>  
        /// <param name="firstName">Actor firstname </param>
        public async Task<Guid?> ActorExistAsync(string lastName, string firstName)
        {
            Actor? actor = await _Context.Actors.FirstOrDefaultAsync(d =>
            d.LastName.ToLower() == lastName.ToLower() &&
            d.FirstName.ToLower() == firstName.ToLower());

            return actor == null ? null : actor.Id;
        }
        #endregion

        #region KINDS
        /// <summary>
        /// Add new Kind
        /// </summary>
        /// <param name="description">Kind description </param>  
        public async Task<Guid> AddKindAsync(string description)
        {
            //Check if Director already exist
            Guid? foundKindId = await KindExistAsync(description);

            if (foundKindId == null)
            {
                //Create a new Kind
                Guid newGuid = Guid.NewGuid();
                Kind newKind = new Kind { Id = newGuid, Description = description };

                _Context.Kinds.Add(newKind);
                await _Context.SaveChangesAsync();

                return newKind.Id;
            }
            else
                return foundKindId.Value;
        }

        /// <summary>
        /// Check Kind exist
        /// </summary>
        /// <param name="description">Kind description </param>  
        public async Task<Guid?> KindExistAsync(string description)
        {
            Kind? kind = await _Context.Kinds.FirstOrDefaultAsync(d =>
            d.Description.ToLower() == description.ToLower());

            return kind == null ? null : kind.Id;
        }
        #endregion

        #region LINKS MOVIES 
        /// <summary>
        /// Add links Movie and Actor
        /// </summary>
        /// <param name="movieId">Movie Identifier </param>
        /// <param name="actorId">Actor Identifier </param>
        public async Task AddMovieActorAsync(Guid movieId, Guid actorId)
        {
            //Check if Link Movie Actor already exist
            MovieActor? movieActor = await _Context.MovieActors.FirstOrDefaultAsync(ma => ma.MovieId == movieId && ma.ActorId == actorId);

            if (movieActor == null)
            {
                //Create a new Link Movie Actor
                MovieActor newMovieActor = new MovieActor { ActorId = actorId, MovieId = movieId };

                _Context.MovieActors.Add(newMovieActor);
                await _Context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Add links Movie and Kind
        /// </summary>
        /// <param name="movieId">Movie Identifier </param>
        /// <param name="kindId">Kind Identifier </param>
        public async Task AddMovieKindAsync(Guid movieId, Guid kindId)
        {
            //Check if Link Movie Kind already exist
            MovieKind? movieKind = await _Context.MovieKinds.FirstOrDefaultAsync(mv => mv.MovieId == movieId && mv.KindId == kindId);

            if (movieKind == null)
            {
                //Create a new Link Movie Kind
                MovieKind newMovieKind = new MovieKind { KindId = kindId, MovieId = movieId };

                _Context.MovieKinds.Add(newMovieKind);
                await _Context.SaveChangesAsync();
            }
        }
        #endregion

    }
}
