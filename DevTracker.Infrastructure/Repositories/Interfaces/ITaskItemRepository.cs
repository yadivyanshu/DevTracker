using DevTracker.Domain.Entities;

namespace DevTracker.Infrastructure.Repositories.Interfaces{
    public interface ITaskItemRepository
    {
        Task<TaskItem> GetTaskByIdAsync(int id);
        Task<List<TaskItem>> GetByFeatureIdAsync(int featureId);
        Task<List<TaskItem>> GetAllTasksAsync();
        Task AddTaskAsync(TaskItem taskItem);
        Task UpdateTaskAsync(TaskItem taskItem);
        Task DeleteTaskAsync(int id);
        Task<bool> EntityExistsAsync(int taskItemId);
    }
}