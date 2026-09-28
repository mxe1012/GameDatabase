using Microsoft.EntityFrameworkCore;

using GameDatabase.Data;
using GameDatabase.Entites;
using GameDatabase.DTOs;

namespace GameDatabase.Repositories
{
    public class GameRepository: IGameRepository
    {
        private readonly GameDatabaseContext _context;

        public GameRepository(GameDatabaseContext context)
        {
            _context = context;
        }

        public async Task<(IEnumerable<Game> Games, int TotalCount)> GetGamesAsync(GameQueryParameters queryParameters, bool includeDeleted)
        {
            var query = _context.Games.AsQueryable();

            if(!includeDeleted)
            {
                query = query.Where(g => !g.IsDeleted);
            }

            if (!string.IsNullOrWhiteSpace(queryParameters.GameName))
            {
                query = query.Where(g => g.GameName.ToUpper() == queryParameters.GameName.ToUpper());
            }

            if (!string.IsNullOrWhiteSpace(queryParameters.GeneralSearch))
            {
                var pattern = $"%{queryParameters.GeneralSearch.Trim()}%";

                query = query.Where(g => EF.Functions.ILike(g.GameName, pattern));
            }

            if (queryParameters.RetailPrice.HasValue)
            {
                query = query.Where(g => g.RetailPrice == queryParameters.RetailPrice);
            }

            if (queryParameters.PriceGreaterThan.HasValue)
            {
                query = query.Where(g => g.RetailPrice <= queryParameters.PriceGreaterThan.Value);
            }

            if (queryParameters.PriceLessThan.HasValue)
            {
                query = query.Where(g => g.RetailPrice >= queryParameters.PriceLessThan.Value);
            }

            if (queryParameters.ReleaseDate.HasValue)
            {
                query = query.Where(g => g.ReleaseDate == queryParameters.ReleaseDate);
            }

            if (queryParameters.ReleasedOnOrBefore.HasValue)
            {
                query = query.Where(g => g.ReleaseDate <= queryParameters.ReleasedOnOrBefore);
            }

            if (queryParameters.ReleasedOnOrAfter.HasValue)
            {
                query = query.Where(g => g.ReleaseDate >= queryParameters.ReleasedOnOrAfter);
            }

            if (queryParameters.IsForSale.HasValue)
            {
                query = query.Where(g => g.IsForSale == queryParameters.IsForSale);
            }

            if (queryParameters.DeveloperId.HasValue)
            {
                query = query.Where(g => g.DeveloperId == queryParameters.DeveloperId);
            }

            if (queryParameters.GenreId.HasValue)
            {
                query = query.Where(g => g.GenreId == queryParameters.GenreId);
            }

            if (queryParameters.EngineId.HasValue)
            {
                query = query.Where(g => g.EngineId == queryParameters.EngineId);
            }

            var totalCount = await query.CountAsync();

            var games = await query
                .OrderBy(g => g.GameId)
                .Skip((queryParameters.PageNumber - 1) * queryParameters.PageSize)
                .Take(queryParameters.PageSize)
                .ToListAsync();

            return (games, totalCount);

        }

        public async Task<Game> GetByIdAsync(int id, bool isAdmin)
        {
            var game = await _context.Games.FindAsync(id);

            if(game == null)
            {
                return null;
            }
            if(!isAdmin && game.IsDeleted)
            {
                return null;
            }
            return game;
        }

        public async Task AddAsync(Game game)
        {
            await _context.Games.AddAsync(game);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Game game)
        {
            _context.Games.Update(game);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id, string? deletedBy, bool isHardDelete)
        {
            var game = await _context.Games.FindAsync(id);

            if(game != null)
            {
                if(isHardDelete)
                {
                    _context.Games.Remove(game);
                }
                else
                {
                    game.IsDeleted = true;
                    game.DeletedBy = deletedBy;
                    game.DeletedAt = DateTime.UtcNow;
                }
                await _context.SaveChangesAsync();
            }
        }
    }
}
