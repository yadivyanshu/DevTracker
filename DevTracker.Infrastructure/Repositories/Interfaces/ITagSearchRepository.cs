using DevTracker.Domain.Enums;
using DevTracker.Common.DTOs;
using DevTracker.Infrastructure.DataContext;
using Microsoft.EntityFrameworkCore;

namespace DevTracker.Infrastructure.Repositories.Interfaces{
    public interface ITagSearchRepository
    {
        Task<List<TagUsageFrequencyDTO>> GetTagUsageFrequencyAsync();
        Task<List<TagSearchDTO>> GetTagsAssignedToEntityAsync(int entityId, EntityTypeEnum entityType);
        Task<List<TagSearchDTO>> GetTagsByEntityTypeAsync(EntityTypeEnum entityType);
        Task<List<TagSearchDTO>> SearchEntitiesByTagNameAsync(string tagName);
        Task<List<TagSearchDTO>> SearchEntitiesByMultipleTagsAsync(List<string> tagNames);
    }
}