using GameDatabase.DTOs;

namespace GameDatabase.Services
{
    public interface IDeveloperService
    {
        Task<IEnumerable<DeveloperResponseDto>> GetDeveloperResponseDtosAsync(bool includeDeleted);
        Task<DeveloperResponseDto> GetDeveloperByIdAsync(int id, bool isAdmin);
        Task<DeveloperResponseDto> AddDeveloperAsync(DeveloperRequestDto dto);
        Task UpdateDeveloperAsync(int id, DeveloperRequestDto dto);
        Task DeleteDeveloperAsync(int id, bool isHardDelete);
    }
    public interface IGenreService
    {
        Task<PagedResult<GenreResponseDto>> GetGenreResponseDtosAsync(GenreQueryParameters queryParams, bool includeDeleted);
        Task<GenreResponseDto> GetGenreByIdAsync(int id, bool isAdmin);
        Task<GenreResponseDto> AddGenreAsync(GenreRequestDto dto);
        Task UpdateGenreAsync(int id, GenreRequestDto dto);
        Task DeleteGenreAsync(int id, bool isHardDelete);
    }
    public interface IEngineService
    {
        Task<PagedResult<EngineResponseDto>> GetEngineResponseDtosAsync(EngineQueryParameters queryParams, bool includeDeleted);
        Task<EngineResponseDto> GetEngineByIdAsync(int id, bool isAdmin);
        Task<EngineResponseDto> AddEngineAsync(EngineRequestDto dto);
        Task UpdateEngineAsync(int id, EngineRequestDto dto);
        Task DeleteEngineAsync(int id, bool isHardDelete);
    }
    public interface IGameService
    {
        Task<IEnumerable<GameResponseDto>> GetGameResponseDtosAsync(bool includeDeleted);
        Task<GameResponseDto> GetGameByIdAsync(int id, bool isAdmin);
        Task<GameResponseDto> AddGameAsync(GameRequestDto dto);
        Task UpdateGameAsync(int id, GameRequestDto dto);
        Task DeleteGameAsync(int id, bool isHardDelete);
    }

}