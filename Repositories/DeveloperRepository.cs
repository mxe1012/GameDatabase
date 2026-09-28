using Microsoft.EntityFrameworkCore;

using GameDatabase.Data;
using GameDatabase.Entites;
using GameDatabase.DTOs;

namespace GameDatabase.Repositories
{
    public class DeveloperRepository : IDeveloperRepository
    {
        private readonly GameDatabaseContext _context;

        public DeveloperRepository(GameDatabaseContext context)
        {
            _context = context;
        }

        public async Task<(IEnumerable<Developer> Developers, int TotalCount)> GetDevelopersAsync(DeveloperQueryParameters queryParameters, bool includeDeleted)
        {
            var query = _context.Developers.AsQueryable();

            if (!includeDeleted)
            {
                query = query.Where(d => !d.IsDeleted);
            }

            if (!string.IsNullOrWhiteSpace(queryParameters.DeveloperName))
            {
                query = query.Where(d => d.DeveloperName == queryParameters.DeveloperName);
            }

            if (!string.IsNullOrWhiteSpace(queryParameters.GeneralSearch))
            {
                query = query.Where(d => d.DeveloperName.Contains(queryParameters.GeneralSearch));
            }

            if (!string.IsNullOrWhiteSpace(queryParameters.City))
            {
                query = query.Where(d => d.City == queryParameters.City);
            }

            if (!string.IsNullOrWhiteSpace(queryParameters.State))
            {
                query = query.Where(d => d.State == queryParameters.State);
            }

            if (!string.IsNullOrWhiteSpace(queryParameters.CountryCode))
            {
                query = query.Where(d => d.CountryCode == queryParameters.CountryCode);
            }

            if (queryParameters.YearFounded.HasValue)
            {
                query = query.Where(d => d.YearFounded == queryParameters.YearFounded.Value);
            }

            if (queryParameters.IsActive.HasValue)
            {
                query = query.Where(d => d.IsActive == queryParameters.IsActive.Value);
            }

            var totalCount = await query.CountAsync();

            var developers = await query
                .OrderBy(d => d.DeveloperId)
                .Skip((queryParameters.PageNumber - 1) * queryParameters.PageSize)
                .Take(queryParameters.PageSize)
                .ToListAsync();

            return (developers, totalCount);

        }

        public async Task<Developer> GetByIdAsync(int id, bool isAdmin)
        {
            var developer = await _context.Developers.FindAsync(id);

            if (developer == null)
            {
                return null;
            }

            if (!isAdmin && developer.IsDeleted)
            {
                return null;
            }

            return developer;
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

        public async Task DeleteAsync(int id, string? deletedBy, bool isHardDelete)
        {
            var developer = await _context.Developers.FindAsync(id);

            if (developer != null)
            {
                if(isHardDelete){
                    _context.Developers.Remove(developer);
                }
                else
                {
                    developer.IsDeleted = true;
                    developer.DeletedBy = deletedBy;
                    developer.DeletedAt = DateTime.UtcNow; 
                }
                await _context.SaveChangesAsync();
            }
        }
    }
}