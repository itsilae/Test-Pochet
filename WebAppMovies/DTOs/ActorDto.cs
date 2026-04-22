namespace WebAppMovies.DTOs
{
    public class ActorDto
    {
        /// <summary>
        /// Actor Identifier (existing in DB)  to  Link to Movie
        /// </summary>
        public Guid? Id { get; set; }

        /// <summary>
        ///  New actor to create and Link to Movie
        /// </summary>
        public PersonDto? NewActor { get; set; } = new();

    }
}
