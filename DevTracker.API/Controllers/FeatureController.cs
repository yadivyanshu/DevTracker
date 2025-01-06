using DevTracker.API.DTOs;
using DevTracker.Application.Interfaces;
using DevTracker.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DevTracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeatureController : ControllerBase
    {
        private readonly IFeatureService _featureService;

        public FeatureController(IFeatureService featureService)
        {
            _featureService = featureService;
        }

        [HttpGet]
        public async Task<ActionResult<List<FeatureDTO>>> GetAllFeatures()
        {
            var features = await _featureService.GetAllFeatures();
            return Ok(features);  // Convert to DTO if needed
        }

        // [HttpGet("{id}")]
        // public async Task<ActionResult<FeatureDTO>> GetFeatureById(int id)
        // {
        //     var feature = await _featureService.GetFeatureById(id);
        //     if (feature == null)
        //     {
        //         return NotFound();
        //     }
        //     return Ok(feature);  // Convert to DTO if needed
        // }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFeatureById(int id)
        {
            var feature = await _featureService.GetFeatureByIdAsync(id);

            if (feature == null)
                return NotFound();

            return Ok(feature);
        }

        [HttpPost]
        public async Task<IActionResult> CreateFeature([FromBody] FeatureDTO featureDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var feature = new Feature
            {
                ProjectId = featureDTO.ProjectId,
                Title = featureDTO.Title,
                Description = featureDTO.Description,
                Status = featureDTO.Status,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            var result = await _featureService.AddFeatureAsync(feature);
            
            if (result != null)
                return CreatedAtAction(nameof(GetFeatureById), new { id = feature.Id }, feature);

            return BadRequest("Failed to create the feature.");
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<FeatureDTO>> UpdateFeature(int id, [FromBody] FeatureDTO featureDTO)
        {
            var feature = await _featureService.GetFeatureByIdAsync(id);
            if (feature == null)
            {
                return NotFound();
            }

            feature.Title = featureDTO.Title;
            feature.Description = featureDTO.Description;
            feature.Status = featureDTO.Status;
            feature.UpdatedAt = DateTime.UtcNow;

            var result = await _featureService.UpdateFeature(id, feature);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest("Failed to update the feature.");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteFeature(int id)
        {
            var result = await _featureService.DeleteFeature(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}