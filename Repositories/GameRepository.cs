using Microsoft.EntityFrameworkCore;

using GameDatabase.Data;
using GameDatabase.Entites;

namespace GameDatabase.Repositories
{
    public class GameRepository: IGameRepository
    {
        private readonly GameDatabaseContext _context;

        public GameRepository(GameDatabaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Game>> GetGamesAsync()
        {
            return await _context.Games.Where(g => g.IsDeleted).OrderBy(g => g.GameId).ToArrayAsync();
        }

        public async Task<Game> GetByIdAsync(int id)
        {
            return await _context.Games.FindAsync(id);
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

        public async Task DeleteAsync(int id, string? deletedBy)
        {
            var game = await _context.Games.FindAsync(id);

            if(game != null)
            {
                game.IsDeleted = true;
                game.DeletedBy = deletedBy;
                game.DeletedAt = DateTime.UtcNow;

                await _context.SaveChangesAsync();
            }
        }
        // public async Task DeleteAsync(int id)
        // {
        //     var game = await _context.Games.FindAsync(id);

        //     if(game != null)
        //     {
        //         _context.Games.Remove(game);

        //         await _context.SaveChangesAsync();
        //     }
        // }

    }
}