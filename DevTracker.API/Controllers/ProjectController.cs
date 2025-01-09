using Microsoft.AspNetCore.Mvc;
using DevTracker.Application.Interfaces;
using DevTracker.Application.DTOs;
using DevTracker.Domain.Entities;
using Microsoft.AspNetCore.Authorization;

namespace DevTracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _service;
        private readonly IFeatureService _featureService;

        public ProjectController(IProjectService service, IFeatureService featureService)
        {
            _service = service;
            _featureService = featureService;
        }

        [Authorize]
        [HttpGet("basic-auth")]
        public IActionResult BasicAuth()
        {
            return Ok("You are authenticated.");
        }

        [HttpGet]
        public IActionResult GetAllProjects()
        {
            var projects = _service.GetAllProjects();
            return Ok(projects);
        }

        [HttpGet("{id}")]
        public IActionResult GetProjectById(int id)
        {
            var project = _service.GetProjectById(id);
            if (project == null) return NotFound();
            return Ok(project);
        }

        [HttpPost]
        public IActionResult CreateProject([FromBody] ProjectDTO projectDto)
        {
            try
            {
                var project = new Project
                {
                    Name = projectDto.Name,
                    Description = projectDto.Description
                };

                _service.CreateProject(project);
                return CreatedAtAction(nameof(GetProjectById), new { id = project.Id }, project);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProject(int id, [FromBody] ProjectDTO projectDto)
        {
            var project = _service.GetProjectById(id);
            if (project == null) return NotFound();

            project.Name = projectDto.Name;
            project.Description = projectDto.Description;
            project.UpdatedAt = DateTime.UtcNow;

            _service.UpdateProject(project);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProject(int id)
        {
            var project = _service.GetProjectById(id);
            if (project == null) return NotFound();

            _service.DeleteProject(id);
            return NoContent();
        }

        [HttpGet("{projectId}/features")]
        public async Task<IActionResult> GetFeaturesByProjectId(int projectId)
        {
            var features = await _featureService.GetFeaturesByProjectIdAsync(projectId);

            if (features == null || !features.Any())
            {
                return NotFound("No features found for this project.");
            }

            return Ok(features);
        }
    }
}