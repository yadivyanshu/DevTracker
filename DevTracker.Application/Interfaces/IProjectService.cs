using DevTracker.Domain.Entities;

namespace DevTracker.Application.Interfaces
{
    public interface IProjectService
    {
        Task<IEnumerable<Project>> GetAllProjectsAsync();
        Task AddProjectAsync(Project project);
    }
}