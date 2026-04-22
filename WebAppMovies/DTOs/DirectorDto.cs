namespace WebAppMovies.DTOs
{
    public class DirectorDto
    {
        /// <summary>
        /// Director Identifier (existing in DB) to associate to movie
        /// </summary>
        public Guid? Id { get; set; }
        /// <summary>
        ///  New Director to create and associate to Movie
        /// </summary>
        public PersonDto? NewDirector { get; set; } = new();
    }
}
