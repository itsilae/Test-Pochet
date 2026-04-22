namespace WebAppMovies.DTOs
{
    public class KindDto
    {
        /// <summary>
        /// Kind Identifier (existing in DB) to link to movie
        /// </summary>
        public Guid? Id { get; set; }
        /// <summary>
        ///  New Kind to create and Link to Movie
        /// </summary>
        public string NewKind { get; set; } = string.Empty ;

    }
}
