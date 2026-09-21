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
    }
}