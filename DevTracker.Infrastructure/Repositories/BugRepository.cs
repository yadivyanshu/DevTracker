using DevTracker.Domain.Entities;
using DevTracker.Infrastructure.DataContext;
using DevTracker.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DevTracker.Infrastructure.Repositories
{
    public class BugRepository : IBugRepository
    {
        private readonly ApplicationDbContext _context;

        public BugRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Bug> GetByIdAsync(int id)
        {
            return await _context.Bugs.Include(b => b.Feature).FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<List<Bug>> GetByFeatureIdAsync(int featureId)
        {
            return await _context.Bugs.Where(b => b.FeatureId == featureId).ToListAsync();
        }

        public async Task<List<Bug>> GetAllAsync()
        {
            return await _context.Bugs.ToListAsync();
        }

        public async Task AddAsync(Bug bug)
        {
            await _context.Bugs.AddAsync(bug);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Bug bug)
        {
            _context.Bugs.Update(bug);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var bug = await _context.Bugs.FindAsync(id);
            if (bug != null)
            {
                _context.Bugs.Remove(bug);
                await _context.SaveChangesAsync();
            }
        }
    }
}