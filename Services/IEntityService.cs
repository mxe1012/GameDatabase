using GameDatabase.DTOs;

namespace GameDatabase.Services
{
    public interface IDeveloperService
    {
        Task<IEnumerable<DeveloperResponseDto>> GetDeveloperResponseDtosAsync();
        Task<DeveloperResponseDto> GetDeveloperByIdAsync(int id);
        Task<DeveloperResponseDto> AddDeveloperAsync(DeveloperRequestDto dto);
        Task UpdateDeveloperAsync(int id, DeveloperRequestDto dto);
        Task DeleteDeveloperAsync(int id, bool isHardDelete);
    }
    public interface IGenreService
    {
        Task<IEnumerable<GenreResponseDto>> GetGenreResponseDtosAsync();
        Task<GenreResponseDto> GetGenreByIdAsync(int id);
        Task<GenreResponseDto> AddGenreAsync(GenreRequestDto dto);
        Task UpdateGenreAsync(int id, GenreRequestDto dto);
        Task DeleteGenreAsync(int id, bool isHardDelete);
    }
    public interface IEngineService
    {
        Task<IEnumerable<EngineResponseDto>> GetEngineResponseDtosAsync();
        Task<EngineResponseDto> GetEngineByIdAsync(int id);
        Task<EngineResponseDto> AddEngineAsync(EngineRequestDto dto);
        Task UpdateEngineAsync(int id, EngineRequestDto dto);
        Task DeleteEngineAsync(int id);
    }
    public interface IGameService
    {
        Task<IEnumerable<GameResponseDto>> GetGameResponseDtosAsync();
        Task<GameResponseDto> GetGameByIdAsync(int id);
        Task<GameResponseDto> AddGameAsync(GameRequestDto dto);
        Task UpdateGameAsync(int id, GameRequestDto dto);
        Task DeleteGameAsync(int id);
    }

}