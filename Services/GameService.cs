using GameDatabase.DTOs;
using GameDatabase.Entites;
using GameDatabase.Repositories;

namespace GameDatabase.Services
{
    public class GameService: IGameService
    {
        private readonly IGameRepository _gameRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GameService(IGameRepository gameRepository, IHttpContextAccessor httpContextAccessor)
        {
            _gameRepository = gameRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IEnumerable<GameResponseDto>> GetGameResponseDtosAsync(bool includeDeleted)
        {
            var games = await _gameRepository.GetGamesAsync(includeDeleted);

            return games.Select(p => new GameResponseDto
                {
                    GameId = p.GameId,
                    GameName = p.GameName,
                    RetailPrice = p.RetailPrice,
                    ReleaseDate = p.ReleaseDate,
                    IsForSale = p.IsForSale,
                    DeveloperId = p.DeveloperId,
                    GenreId = p.GenreId,
                    EngineId = p.EngineId,
                    CreatedBy = p.CreatedBy
                }
            );
        }

        public async Task<GameResponseDto> GetGameByIdAsync(int id, bool isAdmin)
        {
            var game = await _gameRepository.GetByIdAsync(id, isAdmin);

            if(game == null)
            {
                throw new KeyNotFoundException("Game not found");
            }

            return new GameResponseDto
            {
                GameId = game.GameId,
                GameName = game.GameName,
                RetailPrice = game.RetailPrice,
                ReleaseDate = game.ReleaseDate,
                IsForSale = game.IsForSale,
                DeveloperId = game.DeveloperId,
                GenreId = game.GenreId,
                EngineId = game.EngineId,
                CreatedBy = game.CreatedBy,
            };
        }

        public async Task<GameResponseDto> AddGameAsync(GameRequestDto dto)
        {
            var game = new Game
            {
                GameName = dto.GameName,
                RetailPrice = dto.RetailPrice,
                ReleaseDate = dto.ReleaseDate,
                IsForSale = dto.IsForSale,
                DeveloperId = dto.DeveloperId,
                GenreId = dto.GenreId,
                EngineId = dto.EngineId,
                CreatedBy = _httpContextAccessor.HttpContext?.User?.Identity?.Name 
            };

            await _gameRepository.AddAsync(game);

            return new GameResponseDto
            {
                GameId = game.GameId,
                GameName = game.GameName,
                RetailPrice = game.RetailPrice,
                ReleaseDate = game.ReleaseDate,
                IsForSale = game.IsForSale,
                DeveloperId = game.DeveloperId,
                GenreId = game.GenreId,
                EngineId = game.EngineId,
                CreatedBy = game.CreatedBy
            };
        }

        public async Task UpdateGameAsync(int id, GameRequestDto dto)
        {
            var game = await _gameRepository.GetByIdAsync(id, false);

            if(game == null)
            {
                throw new KeyNotFoundException("Game not found");
            }
            game.GameName = dto.GameName;
            game.RetailPrice = dto.RetailPrice;
            game.ReleaseDate = dto.ReleaseDate;
            game.IsForSale = dto.IsForSale;
            game.DeveloperId = dto.DeveloperId;
            game.GenreId = dto.GenreId;
            game.EngineId = dto.EngineId;
            await _gameRepository.UpdateAsync(game);
        }

        public async Task DeleteGameAsync(int id, bool isHardDelete)
        {
            var game = await _gameRepository.GetByIdAsync(id, false);

            if(game == null)
            {
                throw new KeyNotFoundException("Game not found");
            }

            var deletedBy = _httpContextAccessor.HttpContext?.User?.Identity?.Name;

            await _gameRepository.DeleteAsync(id, deletedBy, isHardDelete);
        }
    }
}