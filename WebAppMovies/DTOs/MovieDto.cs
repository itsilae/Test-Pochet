namespace WebAppMovies.DTOs
{
    public class MovieDto
    {
        /// <summary>
        /// Movie Identifier 
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Movie title 
        /// </summary>
        public string Title { get; set; } = string.Empty;
        /// <summary>
        /// Movie release date 
        /// </summary>
        public int ReleaseYear { get; set; }
        /// <summary>
        /// Director of the movie 
        /// </summary>
        public string Director { get; set; } = string.Empty;
        /// <summary>
        /// Budget of the movie 
        /// </summary>
        public decimal Budget { get; set; }
        /// <summary>
        /// Actors of the movie 
        /// </summary>
        public List<string>? Actors { get; set; } = new();
        /// <summary>
        /// Kinds of the movie 
        /// </summary>
        public List<string>? Kinds { get; set; } = new();
    }
}
