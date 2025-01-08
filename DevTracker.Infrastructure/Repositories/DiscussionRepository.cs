using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevTracker.Domain.Entities;
using DevTracker.Domain.Enums;
using DevTracker.Infrastructure.DataContext;
using DevTracker.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DevTracker.Infrastructure.Repositories
{
    public class DiscussionRepository : IDiscussionRepository
    {
        private readonly ApplicationDbContext _context;

        public DiscussionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Discussion> CreateAsync(Discussion discussion)
        {
            // Validate the entityId against the entityType
            bool isEntityValid = discussion.EntityType switch
            {
                EntityTypeEnum.Task => await _context.TaskItems.AnyAsync(t => t.Id == discussion.EntityId),
                EntityTypeEnum.Bug => await _context.Bugs.AnyAsync(b => b.Id == discussion.EntityId),
                EntityTypeEnum.Feature => await _context.Features.AnyAsync(f => f.Id == discussion.EntityId),
                _ => false
            };

            if (!isEntityValid)
            {
                throw new Exception($"Invalid {discussion.EntityType} ID: {discussion.EntityId}");
            }

            await _context.Discussions.AddAsync(discussion);
            await _context.SaveChangesAsync();
            return discussion;
        }

        public async Task<Discussion> GetByIdAsync(int id)
        {
            return await _context.Discussions
                .Include(d => d.Mentions)
                .Include(d => d.Reactions)
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<List<Discussion>> GetByEntityAsync(int entityId, EntityTypeEnum entityType)
        {
            return await _context.Discussions
                        .Include(d => d.Mentions)
                        .Include(d => d.Reactions)
                        .Include(d => d.CreatedBy) // Ensure this is loaded
                        .Where(d => d.EntityId == entityId && d.EntityType == entityType)
                        .ToListAsync();
        }

        public async Task UpdateAsync(Discussion discussion)
        {
            _context.Discussions.Update(discussion);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var discussion = await GetByIdAsync(id);
            if (discussion != null)
            {
                _context.Discussions.Remove(discussion);
                await _context.SaveChangesAsync();
            }
        }
    }
}