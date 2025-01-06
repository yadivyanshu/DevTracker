using DevTracker.Domain.Entities;
using DevTracker.Infrastructure.Repositories.Interfaces;
using DevTracker.Infrastructure.DataContext;
using Microsoft.EntityFrameworkCore;

namespace DevTracker.Infrastructure.Repositories
{
    public class TaskItemRepository : ITaskItemRepository
    {
        private readonly ApplicationDbContext _context;

        public TaskItemRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<TaskItem> GetTaskByIdAsync(int id)
        {
            return await _context.TaskItems.Include(t => t.Assignee).Include(t => t.Feature).FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<List<TaskItem>> GetByFeatureIdAsync(int featureId)
        {
            return await _context.TaskItems.Where(t => t.FeatureId == featureId).ToListAsync();
        }

        public async Task<List<TaskItem>> GetAllTasksAsync()
        {
            return await _context.TaskItems.Include(t => t.Assignee).Include(t => t.Feature).ToListAsync();
        }

        public async Task AddTaskAsync(TaskItem taskItem)
        {
            await _context.TaskItems.AddAsync(taskItem);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTaskAsync(TaskItem taskItem)
        {
            _context.TaskItems.Update(taskItem);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTaskAsync(int id)
        {
            var task = await _context.TaskItems.FindAsync(id);
            if (task != null)
            {
                _context.TaskItems.Remove(task);
                await _context.SaveChangesAsync();
            }
        }
    }
}