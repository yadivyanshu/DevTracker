using DevTracker.Application.DTOs; // Using DTOs for communication
using DevTracker.Application.Interfaces;
using DevTracker.Infrastructure.Repositories;

namespace DevTracker.Application.Services
{
    public class ProjectService : IProjectService
    {
        private readonly ProjectRepository _projectRepository;

        public ProjectService(ProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public IEnumerable<ProjectDTO> GetAllProjects()
        {
            var projects = _projectRepository.GetAll();
            return projects.Select(p => new ProjectDTO
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description
            });
        }

        public void CreateProject(ProjectDTO project)
        {
            var newProject = new DevTracker.Domain.Entities.Project
            {
                Name = project.Name,
                Description = project.Description
            };
            
            _projectRepository.Add(newProject);
        }
    }
}