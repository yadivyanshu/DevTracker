using DevTracker.Common.DTOs;
using DevTracker.Domain.Enums;

namespace DevTracker.Application.Interfaces
{
    public interface ITagSearchService
    {
        Task<List<TagUsageFrequencyDTO>> GetTagUsageFrequencyAsync();
        Task<List<TagSearchDTO>> GetTagsAssignedToEntityAsync(int entityId, EntityTypeEnum entityType);
        Task<List<TagSearchDTO>> GetTagsByEntityTypeAsync(EntityTypeEnum entityType);
        Task<List<TagSearchDTO>> SearchEntitiesByTagNameAsync(string tagName);
        Task<List<TagSearchDTO>> SearchEntitiesByMultipleTagsAsync(List<string> tagNames);
    }
}