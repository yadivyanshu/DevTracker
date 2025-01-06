using DevTracker.Domain.Entities;
using DevTracker.Infrastructure.Repositories.Interfaces;
using DevTracker.Infrastructure.DataContext;
using Microsoft.EntityFrameworkCore;

namespace DevTracker.Infrastructure.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly ApplicationDbContext _context;

        public TaskRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<FeatureTask> GetTaskByIdAsync(int id)
        {
            return await _context.FeatureTasks.Include(t => t.Assignee).Include(t => t.Feature).FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<List<FeatureTask>> GetAllTasksAsync()
        {
            return await _context.FeatureTasks.Include(t => t.Assignee).Include(t => t.Feature).ToListAsync();
        }

        public async Task AddTaskAsync(FeatureTask task)
        {
            await _context.FeatureTasks.AddAsync(task);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTaskAsync(FeatureTask task)
        {
            _context.FeatureTasks.Update(task);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTaskAsync(int id)
        {
            var task = await _context.FeatureTasks.FindAsync(id);
            if (task != null)
            {
                _context.FeatureTasks.Remove(task);
                await _context.SaveChangesAsync();
            }
        }
    }
}