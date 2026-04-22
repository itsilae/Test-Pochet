namespace WebAppMovies.Models
{
    public class MovieKind
    {
        public Guid MovieId { get; set; }
        public Movie? Movie { get; set; }

        public Guid KindId { get; set; }
        public Kind? Kind { get; set; }
    }
}
