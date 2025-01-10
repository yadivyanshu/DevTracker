using DevTracker.Common.DTOs;
using DevTracker.Application.Interfaces;
using DevTracker.Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class TagSearchController : ControllerBase
{
    private readonly ITagSearchService _tagSearchService;

    public TagSearchController(ITagSearchService tagSearchService)
    {
        _tagSearchService = tagSearchService;
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
            result = await _tagSearchService.GetTagsAssignedToEntityAsync(entityId.Value, entityType);
        }
        else
        {
            result = await _tagSearchService.GetTagsByEntityTypeAsync(entityType);
        }

        return Ok(result);
    }

    [HttpGet("SearchByTag/{tagName}")]
    public async Task<IActionResult> SearchByTag(string tagName)
    {
        if (string.IsNullOrWhiteSpace(tagName))
            return BadRequest("Tag name cannot be empty.");

        var result = await _tagSearchService.SearchEntitiesByTagNameAsync(tagName);
        return Ok(result);
    }

    [HttpPost("SearchByMultipleTags")]
    public async Task<IActionResult> SearchByMultipleTags([FromBody] List<string> tagNames)
    {
        if (tagNames == null || !tagNames.Any())
            return BadRequest("Tag names cannot be null or empty.");

        var result = await _tagSearchService.SearchEntitiesByMultipleTagsAsync(tagNames);
        return Ok(result);
    }

}
