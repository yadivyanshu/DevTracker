using DevTracker.Application.DTOs;
using DevTracker.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DevTracker.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BugController : ControllerBase
    {
        private readonly IBugService _bugService;

        public BugController(IBugService bugService)
        {
            _bugService = bugService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var bug = await _bugService.GetByIdAsync(id);
            return Ok(bug);
        }

        [HttpGet("feature/{featureId}")]
        public async Task<IActionResult> GetByFeatureId(int featureId)
        {
            var bugs = await _bugService.GetByFeatureIdAsync(featureId);
            return Ok(bugs);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBugs()
        {
            var bugs = await _bugService.GetAllAsync();
            return Ok(bugs);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBugDTO createBugDTO)
        {
            await _bugService.CreateAsync(createBugDTO);
            return CreatedAtAction(nameof(GetById), new { id = createBugDTO.FeatureId }, createBugDTO);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateBugDTO updateBugDTO)
        {
            await _bugService.UpdateAsync(id, updateBugDTO);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _bugService.DeleteAsync(id);
            return NoContent();
        }
    }
}