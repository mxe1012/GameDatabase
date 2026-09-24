using Microsoft.EntityFrameworkCore;

using GameDatabase.Data;
using GameDatabase.Entites;

namespace GameDatabase.Repositories
{
    public class EngineRepository : IEngineRepository
    {
        private readonly GameDatabaseContext _context;

        public EngineRepository(GameDatabaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Engine>> GetEnginesAsync(bool includeDeleted)
        {
            if (includeDeleted)
            {
                return await _context.Engines.OrderBy(e => e.EngineId).ToListAsync();
            }
            return await _context.Engines.Where(e => !e.IsDeleted).OrderBy(e => e.EngineId).ToListAsync();
        }

        public async Task<Engine> GetByIdAsync(int id)
        {
            return await _context.Engines.FindAsync(id);
        }

        public async Task AddAsync(Engine engine)
        {
            await _context.Engines.AddAsync(engine);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Engine engine)
        {
            _context.Engines.Update(engine);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id, string? deletedBy, bool isHardDelete)
        {
            var engine = await _context.Engines.FindAsync(id);

            if(engine != null)
            {
                if (isHardDelete)
                {
                    _context.Engines.Remove(engine);
                }
                else
                {
                    engine.IsDeleted = true;
                    engine.DeletedBy = deletedBy;
                    engine.DeletedAt = DateTime.UtcNow;
                }
                await _context.SaveChangesAsync();
            }
        }
    }
}