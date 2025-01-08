using DevTracker.Application.Interfaces;
using DevTracker.Domain.Enums;
using DevTracker.Common.DTOs;
using DevTracker.Infrastructure.Repositories.Interfaces;

namespace DevTracker.Application.Services
{
    public class TagSearchService : ITagSearchService
    {
        private readonly ITagSearchRepository _tagSearchRepository;

        public TagSearchService(ITagSearchRepository tagSearchRepository)
        {
            _tagSearchRepository = tagSearchRepository;
        }

        public async Task<List<EntitySearchResultDTO>> SearchEntitiesAsync(TagSearchDTO searchCriteria)
        {
            return await _tagSearchRepository.SearchEntitiesAsync(searchCriteria);
        }

        public async Task<List<TagUsageFrequencyDTO>> GetTagUsageFrequencyAsync()
        {
            return await _tagSearchRepository.GetTagUsageFrequencyAsync();
        }

        public async Task<List<TagSearchDTO>> GetTagsAssignedToEntityAsync(int entityId, EntityTypeEnum entityType)
        {
            return await _tagSearchRepository.GetTagsAssignedToEntityAsync(entityId, entityType);
        }

        public async Task<List<TagSearchDTO>> GetTagsByEntityTypeAsync(EntityTypeEnum entityType)
        {
            return await _tagSearchRepository.GetTagsByEntityTypeAsync(entityType);
        }
    }
}