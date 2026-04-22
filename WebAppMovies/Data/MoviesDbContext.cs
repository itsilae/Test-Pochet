


using Microsoft.EntityFrameworkCore;
using WebAppMovies.Models;

namespace WebAppMovies.Data
{
    public partial class MoviesDbContext : DbContext
    {

        public MoviesDbContext(DbContextOptions<MoviesDbContext> options)
       : base(options)
        {
        }
              
        public DbSet<Actor> Actors { get; set; } = null!;

        public DbSet<Director> Directors { get; set; } = null!;

        public DbSet<Kind> Kinds { get; set; } = null!;

        public DbSet<Movie> Movies { get; set; } = null!;

        public DbSet<MovieActor> MovieActors { get; set; } = null!;

        public DbSet<MovieKind> MovieKinds { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Actor>(entity =>
            {
                entity.HasKey(a => a.Id);
                entity.Property(a => a.FirstName)
                    .IsRequired()
                    .HasMaxLength(100);
                entity.Property(a => a.LastName)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Director>(entity =>
            {
                entity.HasKey(d => d.Id);
                entity.Property(d => d.FirstName)
                    .IsRequired()
                    .HasMaxLength(100);
                entity.Property(d => d.LastName)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Kind>(entity =>
            {
                entity.HasKey(k => k.Id);
                entity.Property(k => k.Description)
                    .IsRequired()
                    .HasMaxLength(100);

            });

            modelBuilder.Entity<Movie>(entity =>
            {


                entity.HasKey(m => m.Id);
                entity.Property(m => m.Budget)
                     .HasPrecision(18, 2);
                entity.Property(m => m.Title)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.HasOne(d => d.Director)
                    .WithMany(p => p.Movies)
                    .HasForeignKey(d => d.DirectorId)
                    .OnDelete(DeleteBehavior.Restrict);


                entity.HasMany(m => m.MovieActors)
                    .WithOne(ma => ma.Movie)
                    .HasForeignKey(ma => ma.MovieId);

                entity.HasMany(m => m.MovieKinds)
                    .WithOne(mk => mk.Movie)
                    .HasForeignKey(mk => mk.MovieId);
            });


            modelBuilder.Entity<MovieActor>(entity =>
            {
                entity.HasKey(e => new { e.MovieId, e.ActorId });

                entity.HasOne(e => e.Movie)
                    .WithMany(m => m.MovieActors)
                    .HasForeignKey(e => e.MovieId);

                entity.HasOne(e => e.Actor)
                    .WithMany(a => a.MovieActors)
                    .HasForeignKey(e => e.ActorId);
            });

            modelBuilder.Entity<MovieKind>(entity =>
            {

                entity.HasKey(e => new { e.MovieId, e.KindId });

                entity.HasOne(e => e.Movie)
                    .WithMany(m => m.MovieKinds)
                    .HasForeignKey(e => e.MovieId);

                entity.HasOne(e => e.Kind)
                    .WithMany(k => k.MovieKinds)
                    .HasForeignKey(e => e.KindId);
            });


            // Données - Réalisateurs
            var dirId1 = Guid.Parse("3f1c9b2a-7c4e-4d1a-9a2b-1c2d3e4f5a61");
            var dirId2 = Guid.Parse("7a8d2c3b-1e4f-4a9b-8c2d-5e6f7a8b9c10");
            var dirId3 = Guid.Parse("9c2e1a7b-3d4f-4b8a-9e1c-2d3f4a5b6c72");

            modelBuilder.Entity<Director>().HasData(
                new Director { Id = dirId1, FirstName = "Steven", LastName = "Spielberg" },
                new Director { Id = dirId2, FirstName = "Kathryn", LastName = "Bigelow" },
                new Director { Id = dirId3, FirstName = "Quentin", LastName = "Tarantino" }
            );

            // Données - Acteurs
            var actId1 = Guid.Parse("1a2b3c4d-5e6f-47a8-9b0c-1d2e3f4a5b6c");
            var actId2 = Guid.Parse("2b3c4d5e-6f7a-48b9-0c1d-2e3f4a5b6c7d");
            var actId3 = Guid.Parse("3c4d5e6f-7a8b-49c0-1d2e-3f4a5b6c7d8e");

            modelBuilder.Entity<Actor>().HasData(
                new Actor { Id = actId1, FirstName = "Michelle", LastName = "Yeoh" },
                new Actor { Id = actId2, FirstName = "Brad", LastName = "Pitt" },
                new Actor { Id = actId3, FirstName = "Jenna", LastName = "Ortega" }
            );

            var kinId1 = Guid.Parse("1a2b3c4d-5e6f-47a8-9b0c-111111111111");
            var kinId2 = Guid.Parse("2b3c4d5e-6f7a-48b9-0c1d-222222222222");
            var kinId3 = Guid.Parse("3c4d5e6f-7a8b-49c0-1d2e-333333333333");
            var kinId4 = Guid.Parse("7a8d2c3b-1e4f-4a9b-8c2d-444444444444");
            var kinId5 = Guid.Parse("9c2e1a7b-3d4f-4b8a-9e1c-555555555555");


            // Données - Kind
            modelBuilder.Entity<Kind>().HasData(
                new Kind { Id = kinId1, Description  = "Action" },
                new Kind { Id = kinId2, Description = "Comédie" },
                new Kind { Id = kinId3, Description = "Drame" },
                new Kind { Id = kinId4, Description = "Horreur" },
                new Kind { Id = kinId5, Description = "Science-fiction" }
            );

        }
    }
}
