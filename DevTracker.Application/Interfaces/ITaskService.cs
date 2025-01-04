using DevTracker.Domain.Entities;
using DevTracker.Application.DTOs;

namespace DevTracker.Application.Interfaces
{
    public interface ITaskService
    {
        Task<TaskDTO> GetTaskByIdAsync(int id);
        Task<List<TaskDTO>> GetAllTasksAsync();
        Task<TaskDTO> CreateTaskAsync(CreateTaskDTO createTaskDTO);
        Task UpdateTaskAsync(int id, UpdateTaskDTO updateTaskDTO);
        Task DeleteTaskAsync(int id);
    }
}