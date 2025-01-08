using DevTracker.Common.DTOs;
using DevTracker.Domain.Enums;

namespace DevTracker.Application.Interfaces
{
    public interface ITagSearchService
    {
        Task<List<EntitySearchResultDTO>> SearchEntitiesAsync(TagSearchDTO searchCriteria);
        Task<List<TagUsageFrequencyDTO>> GetTagUsageFrequencyAsync();
        Task<List<TagSearchDTO>> GetTagsAssignedToEntityAsync(int entityId, EntityTypeEnum entityType);
    }
}