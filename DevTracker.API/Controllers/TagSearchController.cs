using DevTracker.Common.DTOs;
using DevTracker.Application.Interfaces;
using DevTracker.Domain.Enums;
using Microsoft.AspNetCore.Mvc;


[ApiController]
[Route("api/[controller]")]
public class TagSearchController : ControllerBase
{
    private readonly ITagSearchService _tagSearchService;

    public TagSearchController(ITagSearchService tagSearchService)
    {
        _tagSearchService = tagSearchService;
    }

    [HttpPost("SearchEntities")]
    public async Task<IActionResult> SearchEntities([FromBody] TagSearchDTO searchCriteria)
    {
        var result = await _tagSearchService.SearchEntitiesAsync(searchCriteria);
        return Ok(result);
    }

    [HttpGet("TagUsageFrequency")]
    public async Task<IActionResult> GetTagUsageFrequency()
    {
        var result = await _tagSearchService.GetTagUsageFrequencyAsync();
        return Ok(result);
    }

    [HttpGet("TagsAssignedToEntity/{entityType}/{entityId?}")]
    public async Task<IActionResult> GetTagsAssignedToEntity(EntityTypeEnum entityType, int? entityId = null)
    {
        List<TagSearchDTO> result;

        if (entityId.HasValue)
        {
            // Fetch tags for a specific entity
            result = await _tagSearchService.GetTagsAssignedToEntityAsync(entityId.Value, entityType);
        }
        else
        {
            // Fetch all tags used in the specified entity type
            result = await _tagSearchService.GetTagsByEntityTypeAsync(entityType);
        }

        return Ok(result);
    }

}
