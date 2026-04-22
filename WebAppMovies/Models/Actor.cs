namespace WebAppMovies.Models
{
    public class Actor
    {
        public Guid Id { get; set; }

        public string LastName { get; set; } = null!;

        public string FirstName { get; set; } = null!;

        public virtual ICollection<MovieActor> MovieActors { get; set; } = new List<MovieActor>();
    }
}
