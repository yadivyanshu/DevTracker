using DevTracker.Application.Interfaces;
using DevTracker.Domain.Entities;
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

        public async Task<IEnumerable<Project>> GetAllProjectsAsync()
        {
            return await _projectRepository.GetAllAsync();
        }

        public async Task AddProjectAsync(Project project)
        {
            await _projectRepository.AddAsync(project);
        }
    }
}