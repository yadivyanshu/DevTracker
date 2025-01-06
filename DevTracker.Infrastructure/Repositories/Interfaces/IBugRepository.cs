using DevTracker.Domain.Entities;

namespace DevTracker.Infrastructure.Repositories.Interfaces
{
    public interface IBugRepository
    {
        Task<Bug> GetByIdAsync(int id);
        Task<List<Bug>> GetByFeatureIdAsync(int featureId);
        Task<List<Bug>> GetAllAsync();
        Task AddAsync(Bug bug);
        Task UpdateAsync(Bug bug);
        Task DeleteAsync(int id);
        Task<bool> EntityExistsAsync(int bugId);
    }
}