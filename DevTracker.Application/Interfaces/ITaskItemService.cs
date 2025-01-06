using DevTracker.Domain.Entities;
using DevTracker.Application.DTOs;

namespace DevTracker.Application.Interfaces
{
    public interface ITaskItemService
    {
        Task<TaskItemDTO> GetTaskByIdAsync(int id);
        Task<List<TaskItemDTO>> GetByFeatureIdAsync(int featureId);
        Task<List<TaskItemDTO>> GetAllTasksAsync();
        Task<TaskItemDTO> CreateTaskAsync(CreateTaskItemDTO createTaskDTO);
        Task UpdateTaskAsync(int id, UpdateTaskItemDTO updateTaskDTO);
        Task DeleteTaskAsync(int id);
    }
}