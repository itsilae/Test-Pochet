using WebAppMovies.Models;

namespace WebAppMovies.Repository
{
    public interface IMovieRepository
    {
        #region MOVIES
        /// <summary> 
        /// Get All Movies 
        /// </summary>
        IQueryable<Movie> GetAllMovies();

        /// <summary>
        /// Get Movie by id
        /// </summary>
        /// <param name="movieId">Movie Identifier </param>
        Task<Movie> GetMovieAsync(Guid movieId);

        /// <summary>
        /// Add new Movie
        /// </summary>
        /// <param name="movie">movie to create </param> 
        Task<Guid> AddMovieAsync(Movie movie);

        /// <summary>
        /// Check Movie exist
        /// </summary>
        /// <param name="title">movie title </param>  
        /// <param name="releaseYear">movie release date </param>
        /// <param name="directorId">Director Identifier </param>
        Task<bool> MovieExistAsync(string title, int releaseYear, Guid directorId);
        #endregion

        #region DIRECTORS

        /// <summary>
        /// Add new Director
        /// </summary>
        /// <param name="lastName">Director lastname </param>  
        /// <param name="firstName">Director firstname </param>
        Task<Guid> AddDirectorAsync(string lastName, string firstName);

        /// <summary>
        /// Check Direcor exist
        /// </summary>
        /// <param name="lastName">Director lastname </param>  
        /// <param name="firstName">Director firstname </param>
        Task<Guid?> DirectorExistAsync(string lastName, string firstName);

        #endregion

        #region ACTORS
        /// <summary>
        /// Add new Actor
        /// </summary>
        /// <param name="lastName">Actor lastname </param>  
        /// <param name="firstName">Actor firstname </param>
        Task<Guid> AddActorAsync(string lastName, string firstName);

        /// <summary>
        /// Check Actor exist
        /// </summary>
        /// <param name="lastName">Actor lastname </param>  
        /// <param name="firstName">Actor firstname </param>
        Task<Guid?> ActorExistAsync(string lastName, string firstName);
        #endregion

        #region KINDS
        /// <summary>
        /// Add new Kind
        /// </summary>
        /// <param name="description">Kind description </param>  
        Task<Guid> AddKindAsync(string description);

        /// <summary>
        /// Check Kind exist
        /// </summary>
        /// <param name="description">Kind description </param>  
        Task<Guid?> KindExistAsync(string description);
        #endregion

        #region LINKS MOVIES 
        /// <summary>
        /// Add links Movie and Actor
        /// </summary>
        /// <param name="movieId">Movie Identifier </param>
        /// <param name="actorId">Actor Identifier </param>
        Task AddMovieActorAsync(Guid movieId, Guid actorId);

        /// <summary>
        /// Add links Movie and Kind
        /// </summary>
        /// <param name="movieId">Movie Identifier </param>
        /// <param name="kindId">Kind Identifier </param>
        Task AddMovieKindAsync(Guid movieId, Guid kindId);
        #endregion


    }
}
