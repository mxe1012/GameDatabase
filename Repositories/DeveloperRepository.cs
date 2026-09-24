using Microsoft.EntityFrameworkCore;

using GameDatabase.Data;
using GameDatabase.Entites;

namespace GameDatabase.Repositories
{
    public class DeveloperRepository : IDeveloperRepository
    {
        private readonly GameDatabaseContext _context;

        public DeveloperRepository(GameDatabaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Developer>> GetDevelopersAsync()
        {
            return await _context.Developers.Where(d => !d.IsDeleted).OrderBy(d => d.DeveloperId).ToListAsync();
        }

        public async Task<Developer> GetByIdAsync(int id)
        {
            return await _context.Developers.FindAsync(id);
        }

        public async Task AddAsync(Developer developer)
        {
            await _context.Developers.AddAsync(developer);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Developer developer)
        {
            _context.Developers.Update(developer);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id, string? deletedBy)
        {
            var developer = await _context.Developers.FindAsync(id);

            if (developer != null)
            {
                developer.IsDeleted = true;
                developer.DeletedBy = deletedBy;
                developer.DeletedAt = DateTime.UtcNow;

                await _context.SaveChangesAsync();
            }
        }
        
        // public async Task DeleteAsync(int id)
        // {
        //     var developer = await _context.Developers.FindAsync(id);

        //     if (developer != null)
        //     {
        //         _context.Developers.Remove(developer);

        //         await _context.SaveChangesAsync();
        //     }
        // }

    }
}