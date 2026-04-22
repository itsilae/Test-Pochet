using System.IO;

namespace WebAppMovies.Models
{
    public class Movie
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = null!;

        public int ReleaseYear { get; set; }

        public decimal Budget { get; set; }

        public Guid DirectorId { get; set; }

        public virtual Director Director { get; set; } = null!;

        public virtual ICollection<MovieActor> MovieActors { get; set; } = new List<MovieActor>();

        public virtual ICollection<MovieKind> MovieKinds { get; set; } = new List<MovieKind>();
    }
}
