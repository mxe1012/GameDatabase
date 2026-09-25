using GameDatabase.Entites;
using GameDatabase.DTOs;

namespace GameDatabase.Repositories
{
    public interface IDeveloperRepository
    {
        Task<IEnumerable<Developer>> GetDevelopersAsync(bool includeDeleted);
        Task <Developer> GetByIdAsync(int id, bool isAdmin);
        Task AddAsync(Developer developer);
        Task UpdateAsync(Developer developer);
        Task DeleteAsync(int id, string? deletedBy, bool isHardDelete);
    }

    public interface IGenreRepository
    {
        Task<(IEnumerable<Genre> Genres, int TotalCount)> GetGenresAsync(GenreQueryParameters queryParams, bool includeDeleted);
        Task <Genre> GetByIdAsync(int id, bool isAdmin);
        Task AddAsync(Genre genre);
        Task UpdateAsync(Genre genre);
        Task DeleteAsync(int id, string? deletedBy, bool isHardDelete);
    }

    public interface IEngineRepository
    {
        Task<(IEnumerable<Engine> Engines, int TotalCount)> GetEnginesAsync(EngineQueryParameters queryParams, bool includeDeleted);
        Task <Engine> GetByIdAsync(int id, bool isAdmin);
        Task AddAsync(Engine engine);
        Task UpdateAsync(Engine engine);
        Task DeleteAsync(int id, string? deletedBy, bool isHardDelete);
    }

    public interface IGameRepository
    {
        Task<IEnumerable<Game>> GetGamesAsync(bool includeDeleted);
        Task <Game> GetByIdAsync(int id, bool isAdmin);
        Task AddAsync(Game game);
        Task UpdateAsync(Game game);
        Task DeleteAsync(int id, string? deletedBy, bool isHardDelete);
    }
}