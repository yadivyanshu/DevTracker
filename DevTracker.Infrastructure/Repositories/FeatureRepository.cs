using DevTracker.Domain.Entities;
using DevTracker.Infrastructure.DataContext;
using Microsoft.EntityFrameworkCore;
using DevTracker.Infrastructure.Repositories.Interfaces;

namespace DevTracker.Infrastructure.Repositories
{
    public class FeatureRepository : IFeatureRepository
    {
        private readonly ApplicationDbContext _context;

        public FeatureRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Feature>> GetAllAsync()
        {
            return await _context.Features.Include(f => f.Project).ToListAsync();
        }

        public async Task<Feature> GetByIdAsync(int id)
        {
            return await _context.Features
                .Include(f => f.Project)
                .FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task<Feature> AddAsync(Feature feature)
        {
            await _context.Features.AddAsync(feature);
            await _context.SaveChangesAsync();
            return feature;
        }

        public async Task<Feature> UpdateAsync(Feature feature)
        {
            _context.Features.Update(feature);
            await _context.SaveChangesAsync();
            return feature;
        }

        public async Task<bool> DeleteAsync(Feature feature)
        {
            _context.Features.Remove(feature);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EntityExistsAsync(int featureId)
        {
            return await _context.Features.AnyAsync(f => f.Id == featureId);
        }
    }
}