using DevTracker.Domain.Entities;

namespace DevTracker.Infrastructure.Repositories.Interfaces
{
    public interface IFeatureRepository
    {
        Task<List<Feature>> GetAllAsync();
        Task<Feature> GetByIdAsync(int id);
        Task<Feature> AddAsync(Feature feature);
        Task<Feature> UpdateAsync(Feature feature);
        Task<bool> DeleteAsync(Feature feature);
        Task<int> SaveChangesAsync();
    }
}