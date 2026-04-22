namespace WebAppMovies.Models
{
    public class Kind
    {
        public Guid Id { get; set; }

        public string Description { get; set; } = null!;

        public virtual ICollection<MovieKind> MovieKinds { get; set; } = new List<MovieKind>();
    }
}
