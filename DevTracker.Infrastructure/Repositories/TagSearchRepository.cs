using DevTracker.Domain.Enums;
using DevTracker.Common.DTOs;
using DevTracker.Infrastructure.DataContext;
using DevTracker.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using DevTracker.Infrastructure.Helpers;

namespace DevTracker.Infrastructure.Repositories
{
    public class TagSearchRepository : ITagSearchRepository
    {
        private readonly ApplicationDbContext _context;

        public TagSearchRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<EntitySearchResultDTO>> SearchEntitiesAsync(TagSearchDTO searchCriteria)
{
    var query = _context.Taggings.AsQueryable();

    // Apply filtering based on tag IDs
    if (searchCriteria.TagId.HasValue)
    {
        query = query.Where(t => t.TagId == searchCriteria.TagId);
    }

    if (searchCriteria.MultipleTagIds != null && searchCriteria.MultipleTagIds.Any())
    {
        query = query.Where(t => searchCriteria.MultipleTagIds.Contains(t.TagId));
    }

    // Handle filtering by entity type
    if (searchCriteria.EntityType.HasValue)
    {
        query = query.Where(t => t.EntityType == searchCriteria.EntityType);
    }

    // Combine all queries into one before projection
    var combinedQuery = query
        .Where(t => t.EntityType == EntityTypeEnum.Task)
        .Join(_context.TaskItems, tagging => tagging.EntityId, task => task.Id,
            (tagging, task) => new
            {
                EntityId = task.Id,
                EntityType = EntityTypeEnum.Task,
                Title = task.Title,
                Status = task.Status.ToString(),
                AssigneeId = (int?)task.AssigneeId // Ensure AssigneeId is nullable
            })
        .Concat(query
            .Where(t => t.EntityType == EntityTypeEnum.Bug)
            .Join(_context.Bugs, tagging => tagging.EntityId, bug => bug.Id,
                (tagging, bug) => new
                {
                    EntityId = bug.Id,
                    EntityType = EntityTypeEnum.Bug,
                    Title = bug.Title,
                    Status = bug.Status.ToString(),
                    AssigneeId = (int?)bug.AssigneeId // Ensure AssigneeId is nullable
                }))
        .Concat(query
            .Where(t => t.EntityType == EntityTypeEnum.Feature)
            .Join(_context.Features, tagging => tagging.EntityId, feature => feature.Id,
                (tagging, feature) => new
                {
                    EntityId = feature.Id,
                    EntityType = EntityTypeEnum.Feature,
                    Title = feature.Title,
                    Status = feature.Status.ToString(),
                    AssigneeId = (int?)null // Features don’t have assignees
                }));

    // Apply additional filters after combining
    if (!string.IsNullOrEmpty(searchCriteria.Status))
    {
        combinedQuery = combinedQuery.Where(r => r.Status == searchCriteria.Status);
    }

    if (searchCriteria.AssigneeId.HasValue)
    {
        combinedQuery = combinedQuery.Where(r => r.AssigneeId == searchCriteria.AssigneeId);
    }

    // Perform projection to DTO after filtering
    var results = await combinedQuery
        .Select(r => new EntitySearchResultDTO
        {
            EntityId = r.EntityId,
            EntityType = r.EntityType,
            Title = r.Title,
            Status = r.Status,
            AssigneeId = r.AssigneeId
        })
        .ToListAsync();

    return results;
}

        public async Task<List<TagUsageFrequencyDTO>> GetTagUsageFrequencyAsync()
        {
            return await _context.Taggings
                .GroupBy(t => t.TagId)
                .Select(g => new TagUsageFrequencyDTO
                {
                    TagId = g.Key,
                    TagName = g.First().Tag.Name,
                    UsageCount = g.Count()
                })
                .OrderByDescending(t => t.UsageCount)
                .ToListAsync();
        }

        public async Task<List<TagSearchDTO>> GetTagsAssignedToEntityAsync(int entityId, EntityTypeEnum entityType)
        {
            var taggings = await _context.Taggings
            .Where(t => t.EntityId == entityId && t.EntityType.Equals(entityType))
            .Select(t => new
            {
                t.TagId,
                TagName = t.Tag.Name,
                t.EntityType
            })
            .ToListAsync();

            // Step 2: Process statuses asynchronously
            var result = new List<TagSearchDTO>();
            foreach (var tagging in taggings)
            {
                var entityDetails = await EntityDetailsHelper.GetEntityDetailsAsync(_context, tagging.EntityType, entityId);

                result.Add(new TagSearchDTO
                {
                    TagId = tagging.TagId,
                    TagName = tagging.TagName,
                    EntityType = tagging.EntityType,
                    EntityId = entityId,
                    Status = entityDetails.Status,
                    AssigneeId = entityDetails.AssigneeId,
                    Title = entityDetails.Title,
                    Description = entityDetails.Description,
                    Severity = entityDetails.Severity
                });
            }

            return result;
        }

        public async Task<List<TagSearchDTO>> GetTagsByEntityTypeAsync(EntityTypeEnum entityType)
        {
            var taggings = await _context.Taggings
                .Where(t => t.EntityType == entityType)
                .Select(t => new TagSearchDTO
                {
                    TagId = t.TagId,
                    TagName = t.Tag.Name,
                    EntityType = t.EntityType,
                    EntityId = t.EntityId
                })
                .ToListAsync();

                // Step 2: Process statuses asynchronously
            var result = new List<TagSearchDTO>();
            foreach (var tagging in taggings)
            {
                var entityDetails = await EntityDetailsHelper.GetEntityDetailsAsync(_context, (EntityTypeEnum)tagging.EntityType, tagging.EntityId.Value);
                                
                result.Add(new TagSearchDTO
                {
                    TagId = tagging.TagId,
                    TagName = tagging.TagName,
                    EntityType = tagging.EntityType,
                    EntityId = tagging.EntityId,
                    AssigneeId = entityDetails.AssigneeId,
                    Status = entityDetails.Status,
                    Title = entityDetails.Title,
                    Description = entityDetails.Description,
                    Severity = entityDetails.Severity
                });
            }

            return result;
        }


    }
}