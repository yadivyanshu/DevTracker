using DevTracker.Domain.Entities;

namespace DevTracker.Infrastructure.Repositories.Interfaces{
    public interface ITaskRepository
    {
        Task<FeatureTask> GetTaskByIdAsync(int id);
        Task<List<FeatureTask>> GetAllTasksAsync();
        Task AddTaskAsync(FeatureTask task);
        Task UpdateTaskAsync(FeatureTask task);
        Task DeleteTaskAsync(int id);
    }
}