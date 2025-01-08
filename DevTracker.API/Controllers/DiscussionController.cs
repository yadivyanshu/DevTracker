using DevTracker.Domain.Enums;
using DevTracker.API.DTOs;
using DevTracker.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DevTracker.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DiscussionController : ControllerBase
    {
        private readonly IDiscussionService _service;

        public DiscussionController(IDiscussionService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateDiscussionDTO dto)
        {
            var result = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            return result != null ? Ok(result) : NotFound();
        }

        [HttpGet("entity/{entityId}/{entityType}")]
        public async Task<IActionResult> GetByEntity(int entityId, EntityTypeEnum entityType)
        {
            var result = await _service.GetByEntityAsync(entityId, entityType);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateDiscussionDTO dto)
        {
            await _service.UpdateAsync(id, dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}