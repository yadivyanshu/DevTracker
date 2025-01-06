using DevTracker.Application.DTOs;
using DevTracker.Domain.Enums;

namespace DevTracker.Application.Interfaces
{
    public interface ITagService
    {
        Task<List<TagDTO>> GetAllTagsAsync();
        Task<TagDTO> GetTagByIdAsync(int id);
        Task CreateTagAsync(CreateTagDTO createTagDTO);
        Task AssignTagToEntityAsync(int tagId, int entityId, EntityTypeEnum entityType);
    }
}