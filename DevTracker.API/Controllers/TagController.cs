using DevTracker.Application.Interfaces;
using DevTracker.Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using DevTracker.Domain.Enums;
using Microsoft.AspNetCore.Authorization;

namespace DevTracker.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly ITagService _tagService;

        public TagController(ITagService tagService)
        {
            _tagService = tagService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTags()
        {
            var tags = await _tagService.GetAllTagsAsync();
            return Ok(tags);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTagById(int id)
        {
            var tag = await _tagService.GetTagByIdAsync(id);
            return Ok(tag);
        }

        [Authorize(Roles = "Admin, Manager")]
        [HttpPost]
        public async Task<IActionResult> CreateTag([FromBody] CreateTagDTO createTagDTO)
        {
            await _tagService.CreateTagAsync(createTagDTO);
            return CreatedAtAction(nameof(GetTagById), new { id = createTagDTO.Name }, createTagDTO);
        }

        [Authorize(Roles = "Admin, Manager")]
        [HttpPost("{tagId}/assign/{entityId}")]
        public async Task<IActionResult> AssignTagToEntity(int tagId, int entityId, [FromQuery] EntityTypeEnum entityType)
        {
            try
            {
                await _tagService.AssignTagToEntityAsync(tagId, entityId, entityType);
                return Ok(new { message = "Tag assigned successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}