using Microsoft.EntityFrameworkCore;

using GameDatabase.Entites;

namespace GameDatabase.Data
{
    public class GameDatabaseContext(DbContextOptions<GameDatabaseContext> options): DbContext(options)
    {
        public DbSet<Developer> Developers {get; set;}
        public DbSet<Genre> Genres {get; set;}
        public DbSet<Engine> Engines {get; set;}
        public DbSet<Game> Games {get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Game>()
            .HasOne(d => d.Developer)
            .WithMany()
            .HasForeignKey(d => d.DeveloperId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Game>()
            .HasOne(d => d.Genre)
            .WithMany()
            .HasForeignKey(d => d.GenreId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Game>()
            .HasOne(d => d.Engine)
            .WithMany()
            .HasForeignKey(d => d.EngineId)
            .OnDelete(DeleteBehavior.Restrict);

        }

    }
}