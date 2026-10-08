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
                query = query.Where(d => d.DeveloperName.ToUpper() == queryParameters.DeveloperName.ToUpper());
            }

            if (!string.IsNullOrWhiteSpace(queryParameters.GeneralSearch))
            {
                var pattern = $"%{queryParameters.GeneralSearch.Trim()}%";

                query = query.Where(d => 
                EF.Functions.ILike(d.DeveloperName, pattern) || 
                EF.Functions.ILike(d.City, pattern));
            }

            if (!string.IsNullOrWhiteSpace(queryParameters.City))
            {
                query = query.Where(d => d.City.ToUpper() == queryParameters.City.ToUpper());
            }

            if (!string.IsNullOrWhiteSpace(queryParameters.State))
            {
                query = query.Where(d => d.State.ToUpper() == queryParameters.State.ToUpper());
            }

            if (!string.IsNullOrWhiteSpace(queryParameters.CountryCode))
            {
                query = query.Where(d => d.CountryCode.ToUpper() == queryParameters.CountryCode.ToUpper());
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
            await using var transaction = await _context.Database.BeginTransactionAsync();

            var developer = await _context.Developers.FindAsync(id);

            if (developer != null)
            {
                try
                {
                    if(isHardDelete){
                        _context.Developers.Remove(developer);
                    }
                    else
                    {
                        developer.IsDeleted = true;
                        developer.DeletedBy = deletedBy;
                        developer.DeletedAt = DateTime.UtcNow; 

                        var games = await _context.Games.Where(g => g.DeveloperId == id && !g.IsDeleted).ToListAsync();

                        foreach (var game in games)
                        {
                            game.IsDeleted = true;
                            game.DeletedBy = deletedBy;
                            game.DeletedAt = DateTime.Now;
                        }


                    }
                    await _context.SaveChangesAsync();

                    await transaction.CommitAsync();
                }
                catch
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }
    }
}