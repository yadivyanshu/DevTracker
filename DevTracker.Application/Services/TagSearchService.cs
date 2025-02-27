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

        public async Task<List<TagSearchDTO>> SearchEntitiesByTagNameAsync(string tagName)
        {
            return await _tagSearchRepository.SearchEntitiesByTagNameAsync(tagName);
        }

        public async Task<List<TagSearchDTO>> SearchEntitiesByMultipleTagsAsync(List<string> tagNames)
        {
            return await _tagSearchRepository.SearchEntitiesByMultipleTagsAsync(tagNames);
        }
    }
}