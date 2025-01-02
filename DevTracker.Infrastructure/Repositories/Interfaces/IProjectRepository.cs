using DevTracker.Domain.Entities;

namespace DevTracker.Infrastructure.Repositories.Interfaces
{
    public interface IProjectRepository
    {
        IEnumerable<Project> GetAll();
        Project GetById(int id);
        void Add(Project project);
        void Update(Project project);
        void Delete(int id);
        bool IsNameTaken(string name, int? excludeId = null);
    }
}