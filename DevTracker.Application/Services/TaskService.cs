using DevTracker.Domain.Entities;
using DevTracker.Infrastructure.Repositories.Interfaces;
using DevTracker.Application.DTOs;
using DevTracker.Application.Interfaces;
using DevTracker.Domain.Enums;
using AutoMapper;
using System.Linq;
using BCrypt.Net;

namespace DevTracker.Application.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IMapper _mapper;

        public TaskService(ITaskRepository taskRepository, IMapper mapper)
        {
            _taskRepository = taskRepository;
            _mapper = mapper;
        }

        public async Task<TaskDTO> GetTaskByIdAsync(int id)
        {
            var task = await _taskRepository.GetTaskByIdAsync(id);
            if (task == null) throw new Exception("Task not found");
            return _mapper.Map<TaskDTO>(task);
        }

        public async Task<List<TaskDTO>> GetAllTasksAsync()
        {
            var tasks = await _taskRepository.GetAllTasksAsync();
            return tasks.Select(t => _mapper.Map<TaskDTO>(t)).ToList();
        }

        public async Task<TaskDTO> CreateTaskAsync(CreateTaskDTO createTaskDTO)
        {
            var task = _mapper.Map<FeatureTask>(createTaskDTO);
            task.CreatedAt = DateTime.UtcNow;
            task.UpdatedAt = DateTime.UtcNow;

            await _taskRepository.AddTaskAsync(task);
            return _mapper.Map<TaskDTO>(task);
        }

        public async Task UpdateTaskAsync(int id, UpdateTaskDTO updateTaskDTO)
        {
            var task = await _taskRepository.GetTaskByIdAsync(id);
            if (task == null) throw new Exception("Task not found");

            _mapper.Map(updateTaskDTO, task);
            task.UpdatedAt = DateTime.UtcNow;

            await _taskRepository.UpdateTaskAsync(task);
        }

        public async Task DeleteTaskAsync(int id)
        {
            await _taskRepository.DeleteTaskAsync(id);
        }
    }
}