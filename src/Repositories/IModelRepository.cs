using GameDatabase.Entites;

namespace GameDatabase.Repositories{
    public interface IDeveloperRepository
    {
        Task<IEnumerable<Developer>> GetDevelopersAsync();
        Task <Developer> GetByIdAsync(int id);
        Task AddAsync(Developer developer);
        Task UpdateAsync(Developer developer);
        Task DeleteAsync(int id);
    }
}