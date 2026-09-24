using Microsoft.EntityFrameworkCore;

using GameDatabase.Data;
using GameDatabase.Entites;

namespace GameDatabase.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        private readonly GameDatabaseContext _context;

        public GenreRepository(GameDatabaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Genre>> GetGenresAsync(bool includeDeleted)
        {
            if (includeDeleted)
            {
                return await _context.Genres.OrderBy(g => g.GenreId).ToListAsync();
            }
            return await _context.Genres.Where(g => !g.IsDeleted).OrderBy(g => g.GenreId).ToListAsync();
        }

        public async Task<Genre> GetByIdAsync(int id)
        {
            return await _context.Genres.FindAsync(id);
        }

        public async Task AddAsync(Genre genre)
        {
            await _context.Genres.AddAsync(genre);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Genre genre)
        {
            _context.Genres.Update(genre);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id, string? deletedBy, bool isHardDelete)
        {
            var genre = await _context.Genres.FindAsync(id);

            if(genre != null)
            {
                if (isHardDelete)
                {
                    _context.Genres.Remove(genre);
                }
                else
                {
                    genre.IsDeleted = true;
                    genre.DeletedBy = deletedBy;
                    genre.DeletedAt = DateTime.UtcNow; 
                }
                await _context.SaveChangesAsync();
            }
        }
    }
}