using DevTracker.Domain.Enums;
using DevTracker.Domain.Entities;

namespace DevTracker.Infrastructure.Repositories.Interfaces
{
    public interface IDiscussionRepository
    {
        Task<Discussion> CreateAsync(Discussion discussion);
        Task<Discussion> GetByIdAsync(int id);
        Task<List<Discussion>> GetByEntityAsync(int entityId, EntityTypeEnum entityType);
        Task UpdateAsync(Discussion discussion);
        Task DeleteAsync(int id);
    }
}