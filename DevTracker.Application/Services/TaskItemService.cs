using DevTracker.Domain.Entities;
using DevTracker.Infrastructure.Repositories.Interfaces;
using DevTracker.Application.DTOs;
using DevTracker.Application.Interfaces;
using AutoMapper;

namespace DevTracker.Application.Services
{
    public class TaskItemService : ITaskItemService
    {
        private readonly ITaskItemRepository _taskItemRepository;
        private readonly IMapper _mapper;

        public TaskItemService(ITaskItemRepository taskItemRepository, IMapper mapper)
        {
            _taskItemRepository = taskItemRepository;
            _mapper = mapper;
        }

        public async Task<TaskItemDTO> GetTaskByIdAsync(int id)
        {
            var task = await _taskItemRepository.GetTaskByIdAsync(id);
            if (task == null) throw new Exception("Task not found");
            return _mapper.Map<TaskItemDTO>(task);
        }

        public async Task<List<TaskItemDTO>> GetAllTasksAsync()
        {
            var tasks = await _taskItemRepository.GetAllTasksAsync();
            return tasks.Select(t => _mapper.Map<TaskItemDTO>(t)).ToList();
        }

        public async Task<TaskItemDTO> CreateTaskAsync(CreateTaskItemDTO createTaskItemDTO)
        {
            var task = _mapper.Map<TaskItem>(createTaskItemDTO);
            task.CreatedAt = DateTime.UtcNow;
            task.UpdatedAt = DateTime.UtcNow;

            await _taskItemRepository.AddTaskAsync(task);
            return _mapper.Map<TaskItemDTO>(task);
        }

        public async Task UpdateTaskAsync(int id, UpdateTaskItemDTO updateTaskItemDTO)
        {
            var task = await _taskItemRepository.GetTaskByIdAsync(id);
            if (task == null) throw new Exception("Task not found");

            _mapper.Map(updateTaskItemDTO, task);
            task.UpdatedAt = DateTime.UtcNow;

            await _taskItemRepository.UpdateTaskAsync(task);
        }

        public async Task DeleteTaskAsync(int id)
        {
            await _taskItemRepository.DeleteTaskAsync(id);
        }
    }
}