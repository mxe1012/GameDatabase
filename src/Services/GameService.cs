using GameDatabase.DTOs;
using GameDatabase.Entites;
using GameDatabase.Repositories;

namespace GameDatabase.Services
{
    public class GameService: IGameService
    {
        private readonly IGameRepository _gameRepository;

        public GameService(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public async Task<IEnumerable<GameResponseDto>> GetGameResponseDtosAsync()
        {
            var games = await _gameRepository.GetGamesAsync();

            return games.Select(p => new GameResponseDto
                {
                    GameId = p.GameId,
                    GameName = p.GameName,
                    Price = p.Price,
                    ReleaseDate = p.ReleaseDate,
                    DeveloperId = p.DeveloperId,
                    GenreId = p.GenreId,
                    EngineId = p.EngineId,
                }
            );
        }

        public async Task<GameResponseDto> GetGameByIdAsync(int id)
        {
            var game = await _gameRepository.GetByIdAsync(id);

            if(game == null)
            {
                throw new KeyNotFoundException("Game not found");
            }

            return new GameResponseDto
            {
                GameId = game.GameId,
                GameName = game.GameName,
                Price = game.Price,
                ReleaseDate = game.ReleaseDate,
                DeveloperId = game.DeveloperId,
                GenreId = game.GenreId,
                EngineId = game.EngineId,
            };
        }

        public async Task AddGameAsync(GameRequestDto dto)
        {
            var game = new Game
            {
                GameName = dto.GameName,
                Price = dto.Price,
                ReleaseDate = dto.ReleaseDate,
                DeveloperId = dto.DeveloperId,
                GenreId = dto.GenreId,
                EngineId = dto.EngineId, 
            };

            await _gameRepository.AddAsync(game);
        }

        public async Task UpdateGameAsync(int id, GameRequestDto dto)
        {
            var game = await _gameRepository.GetByIdAsync(id);

            if(game == null)
            {
                throw new KeyNotFoundException("Game not found");
            }
            game.GameName = dto.GameName;
            game.Price = dto.Price;
            game.ReleaseDate = dto.ReleaseDate;
            game.DeveloperId = dto.DeveloperId;
            game.GenreId = dto.GenreId;
            game.EngineId = dto.EngineId;
            await _gameRepository.UpdateAsync(game);
        }

        public async Task DeleteGameAsync(int id)
        {
            var game = await _gameRepository.GetByIdAsync(id);

            if(game == null)
            {
                throw new KeyNotFoundException("Game not found");
            }
            await _gameRepository.DeleteAsync(id);
        }
    }
}