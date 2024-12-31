using DevTracker.Application.DTOs; // Using DTOs for communication

namespace DevTracker.Application.Interfaces
{
    public interface IProjectService
    {
        IEnumerable<ProjectDTO> GetAllProjects();
        void CreateProject(ProjectDTO project);
    }
}