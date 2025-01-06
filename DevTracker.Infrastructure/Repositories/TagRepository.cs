using DevTracker.Domain.Entities;
using DevTracker.Infrastructure.Repositories.Interfaces;
using DevTracker.Infrastructure.DataContext;
using Microsoft.EntityFrameworkCore;

namespace DevTracker.Infrastructure.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly ApplicationDbContext _context;

        public TagRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Tag>> GetAllAsync()
        {
            return await _context.Tags.ToListAsync();
        }

        public async Task<Tag> GetByIdAsync(int id)
        {
            return await _context.Tags.FindAsync(id);
        }

        public async Task AddAsync(Tag tag)
        {
            await _context.Tags.AddAsync(tag);
            await _context.SaveChangesAsync();
        }

        public async Task AddTaggingAsync(Tagging tagging)
        {
            await _context.Taggings.AddAsync(tagging);
            await _context.SaveChangesAsync();
        }
    }
}