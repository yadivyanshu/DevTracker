using DevTracker.Application.DTOs;

namespace DevTracker.Application.Interfaces
{
    public interface IBugService
    {
        Task<BugDTO> GetByIdAsync(int id);
        Task<List<BugDTO>> GetByFeatureIdAsync(int featureId);
        Task<List<BugDTO>> GetAllAsync();
        Task CreateAsync(CreateBugDTO createBugDTO);
        Task UpdateAsync(int id, UpdateBugDTO updateBugDTO);
        Task DeleteAsync(int id);
    }
}