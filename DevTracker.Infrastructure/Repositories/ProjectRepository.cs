using DevTracker.Domain.Entities;

namespace DevTracker.Infrastructure.Repositories
{
    public class ProjectRepository
    {
        private readonly List<Project> _projects = new();

        public IEnumerable<Project> GetAll()
        {
            return _projects;
        }

        public void Add(Project project)
        {
            project.Id = _projects.Count + 1;
            _projects.Add(project);
        }
    }
}