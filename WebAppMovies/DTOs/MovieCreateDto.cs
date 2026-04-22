using WebAppMovies.Models;

namespace WebAppMovies.DTOs
{
    public class MovieCreateDto
    {
        /// <summary>
        /// Movie title
        /// </summary>
        public string Title { get; set; } = string.Empty;
        /// <summary>
        /// Movie Release year
        /// </summary>
        public int ReleaseYear { get; set; }
        /// <summary>
        /// Movie budget
        /// </summary>
        public decimal Budget { get; set; }
        /// <summary>
        /// Director associate to movie
        /// </summary>
        public DirectorDto Director { get; set; } = new();
        /// <summary>
        /// Actors to link with movie
        /// </summary>
        public List<ActorDto>? Actors { get; set; } = new();
        /// <summary>
        /// Kinds to link with movie
        /// </summary>
        public List<KindDto>? Kinds { get; set; } = new();

    }
}
