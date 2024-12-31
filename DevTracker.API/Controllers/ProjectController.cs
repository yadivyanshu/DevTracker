using Microsoft.AspNetCore.Mvc;
using DevTracker.Application.Interfaces;
using DevTracker.Application.DTOs;
using DevTracker.Domain.Entities;

namespace DevTracker.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProjects()
        {
            var projects = await _projectService.GetAllProjectsAsync();
            return Ok(projects);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProject([FromBody] ProjectDTO projectDTO)
        {
            var project = new Project
            {
                Name = projectDTO.Name,
                Description = projectDTO.Description
            };

            await _projectService.AddProjectAsync(project);
            return Ok(project);
        }
    }
}