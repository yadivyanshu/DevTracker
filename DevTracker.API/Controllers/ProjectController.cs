using DevTracker.Application.DTOs; // DTOs are in API
using DevTracker.Application.Interfaces; // Interface is in Application
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult GetProjects()
        {
            var projects = _projectService.GetAllProjects(); // Fetch data from service
            return Ok(projects);
        }

        [HttpPost]
        public IActionResult CreateProject([FromBody] ProjectDTO project)
        {
            _projectService.CreateProject(project);
            return Ok("Project created successfully.");
        }
    }
}