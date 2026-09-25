using Microsoft.EntityFrameworkCore;

using GameDatabase.Data;
using GameDatabase.Entites;
using GameDatabase.DTOs;

namespace GameDatabase.Repositories
{
    public class EngineRepository : IEngineRepository
    {
        private readonly GameDatabaseContext _context;

        public EngineRepository(GameDatabaseContext context)
        {
            _context = context;
        }

        public async Task<(IEnumerable<Engine> Engines, int TotalCount)> GetEnginesAsync(EngineQueryParameters queryParameters, bool includeDeleted)
        {

            var query = _context.Engines.AsQueryable();

            if (!includeDeleted)
            {
                query = query.Where(e => !e.IsDeleted);
            }

            if (!string.IsNullOrWhiteSpace(queryParameters.EngineName))
            {
                query = query.Where(e => e.EngineName == queryParameters.EngineName);
            }

            if (!string.IsNullOrWhiteSpace(queryParameters.Search))
            {
                query = query.Where(e => e.EngineName.Contains(queryParameters.Search));
            }

            if (queryParameters.IsOpenSource.HasValue)
            {
                query = query.Where(e => e.IsOpenSource == queryParameters.IsOpenSource.Value);
            }

            var totalCount = await query.CountAsync();

            var engines = await query
                .OrderBy(e => e.EngineId)
                .Skip((queryParameters.PageNumber - 1) * queryParameters.PageSize)
                .Take(queryParameters.PageSize)
                .ToListAsync();

            return (engines, totalCount);
        }

        public async Task<Engine> GetByIdAsync(int id, bool isAdmin)
        {
            var engine = await _context.Engines.FindAsync(id);

            if(engine == null)
            {
                return null;
            }
            if(!isAdmin && engine.IsDeleted)
            {
                return null;
            }
            return engine;
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