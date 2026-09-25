using Microsoft.EntityFrameworkCore;

using GameDatabase.DTOs;
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

        public async Task<(IEnumerable<Genre> Genres, int TotalCount)> GetGenresAsync(GenreQueryParameters queryParams, bool includeDeleted)
        {
            var query = _context.Genres.AsQueryable();

            if (!includeDeleted)
            {
                query = query.Where(g => !g.IsDeleted);
            }

            if (!string.IsNullOrWhiteSpace(queryParams.GenreName))
            {
                query = query.Where(g => g.GenreName == queryParams.GenreName);
            }

            if (!string.IsNullOrWhiteSpace(queryParams.Search))
            {
                query = query.Where(g => g.GenreName.Contains(queryParams.Search));
            }

            var totalCount = await query.CountAsync();

            var genres = await query
                .OrderBy(g => g.GenreId)
                .Skip((queryParams.PageNumber - 1) * queryParams.PageSize)
                .Take(queryParams.PageSize)
                .ToListAsync();

            return (genres, totalCount);
        }

        public async Task<Genre> GetByIdAsync(int id, bool isAdmin)
        {
            var genre = await _context.Genres.FindAsync(id);

            if (genre == null)
            {
                return null;
            }

            if (!isAdmin && genre.IsDeleted)
            {
                return null;
            }
            
            return genre;
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