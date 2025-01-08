using DevTracker.Domain.Enums;
using DevTracker.API.DTOs;

namespace DevTracker.Application.Interfaces
{
    public interface IDiscussionService
    {
        Task<DiscussionDTO> CreateAsync(CreateDiscussionDTO dto);
        Task<DiscussionDTO> GetByIdAsync(int id);
        Task<List<DiscussionDTO>> GetByEntityAsync(int entityId, EntityTypeEnum entityType);
        Task UpdateAsync(int id, UpdateDiscussionDTO dto);
        Task DeleteAsync(int id);
    }
}