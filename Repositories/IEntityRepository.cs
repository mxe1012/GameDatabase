using GameDatabase.Entites;

namespace GameDatabase.Repositories
{
    public interface IDeveloperRepository
    {
        Task<IEnumerable<Developer>> GetDevelopersAsync();
        Task <Developer> GetByIdAsync(int id);
        Task AddAsync(Developer developer);
        Task UpdateAsync(Developer developer);
        Task DeleteAsync(int id, string? deletedBy, bool isHardDelete);
    }

    public interface IGenreRepository
    {
        Task<IEnumerable<Genre>> GetGenresAsync();
        Task <Genre> GetByIdAsync(int id);
        Task AddAsync(Genre genre);
        Task UpdateAsync(Genre genre);
        Task DeleteAsync(int id, string? deletedBy, bool isHardDelete);
    }

    public interface IEngineRepository
    {
        Task<IEnumerable<Engine>> GetEnginesAsync();
        Task <Engine> GetByIdAsync(int id);
        Task AddAsync(Engine engine);
        Task UpdateAsync(Engine engine);
        Task DeleteAsync(int id, string? deletedBy, bool isHardDelete);
    }

    public interface IGameRepository
    {
        Task<IEnumerable<Game>> GetGamesAsync();
        Task <Game> GetByIdAsync(int id);
        Task AddAsync(Game game);
        Task UpdateAsync(Game game);
        Task DeleteAsync(int id, string? deletedBy, bool isHardDelete);
    }
}