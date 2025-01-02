using DevTracker.Application.Interfaces;
using DevTracker.Domain.Entities;
using DevTracker.Infrastructure.Repositories.Interfaces;

namespace DevTracker.Application.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _repository;

        public ProjectService(IProjectRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Project> GetAllProjects()
        {
            return _repository.GetAll();
        }

        public Project GetProjectById(int id)
        {
            return _repository.GetById(id);
        }

        public void CreateProject(Project project)
        {
            if (_repository.IsNameTaken(project.Name))
            {
                throw new ArgumentException("A project with the same name already exists.");
            }
            _repository.Add(project);
        }

        public void UpdateProject(Project project)
        {
            if (_repository.IsNameTaken(project.Name))
            {
                throw new ArgumentException("A project with the same name already exists.");
            }
            _repository.Update(project);
        }

        public void DeleteProject(int id)
        {
            _repository.Delete(id);
        }
    }
}