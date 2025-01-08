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

        public async Task<List<TagSearchDTO>> SearchEntitiesByTagNameAsync(string tagName)
        {
            if (string.IsNullOrWhiteSpace(tagName))
                throw new ArgumentException("Tag name cannot be null or empty.", nameof(tagName));

            // Step 1: Find matching tags
            var tagIds = await _context.Tags
                .Where(tag => EF.Functions.Like(tag.Name.ToLower(), $"%{tagName.ToLower()}%"))
                .Select(tag => tag.Id)
                .ToListAsync();

            if (!tagIds.Any())
            {
                // No matching tags found
                return new List<TagSearchDTO>();
            }

            // Step 2: Find entities associated with these tags
            var taggings = await _context.Taggings
                .Where(t => tagIds.Contains(t.TagId))
                .Include(t => t.Tag) // Ensure Tag navigation property is loaded
                .ToListAsync();

            if (taggings == null || !taggings.Any())
            {
                // No taggings found for matching tags
                return new List<TagSearchDTO>();
            }

            // Step 3: Fetch details of associated entities
            var result = new List<TagSearchDTO>();
            foreach (var tagging in taggings)
            {
                // Guard against null navigation properties
                if (tagging.Tag == null)
                {
                    Console.WriteLine($"Warning: Tag with ID {tagging.TagId} not found.");
                    continue;
                }

                var entityDetails = await EntityDetailsHelper.GetEntityDetailsAsync(_context, tagging.EntityType, tagging.EntityId);

                if (entityDetails != null)
                {
                    result.Add(new TagSearchDTO
                    {
                        TagId = tagging.TagId,
                        TagName = tagging.Tag.Name,
                        EntityType = tagging.EntityType,
                        EntityId = tagging.EntityId,
                        Title = entityDetails.Title,
                        Description = entityDetails.Description,
                        Severity = entityDetails.Severity,
                        Status = entityDetails.Status,
                        AssigneeId = entityDetails.AssigneeId
                    });
                }
            }

            return result;
        }

        public async Task<List<TagSearchDTO>> SearchEntitiesByMultipleTagsAsync(List<string> tagNames)
        {
            if (tagNames == null || !tagNames.Any())
                throw new ArgumentException("Tag names cannot be null or empty.", nameof(tagNames));

            // Convert tagNames to lowercase 
            var normalizedTagNames = tagNames.Select(tn => tn.ToLower()).ToList();

            // Step 1: Find matching tag IDs for the provided tag names
            var tagIds = await _context.Tags
                .Where(tag => normalizedTagNames.Contains(tag.Name.ToLower()))
                .Select(tag => tag.Id)
                .ToListAsync();

            if (!tagIds.Any())
            {
                return new List<TagSearchDTO>();
            }

            // Step 2: Find entities tagged with all provided tags
            var entitiesWithAllTags = await _context.Taggings
                .Where(t => tagIds.Contains(t.TagId))
                .GroupBy(t => new { t.EntityType, t.EntityId })
                .Where(group => group.Select(t => t.TagId).Distinct().Count() == tagIds.Count) 
                .Select(group => new
                {
                    group.Key.EntityType,
                    group.Key.EntityId,
                    group.First().Tag.Name
                })
                .ToListAsync();

            if (!entitiesWithAllTags.Any())
            {
                return new List<TagSearchDTO>();
            }

            // Step 3: Fetch details of associated entities
            var result = new List<TagSearchDTO>();
            foreach (var entity in entitiesWithAllTags)
            {
                var entityDetails = await EntityDetailsHelper.GetEntityDetailsAsync(_context, entity.EntityType, entity.EntityId);

                if (entityDetails != null)
                {
                    result.Add(new TagSearchDTO
                    {
                        TagId = entityDetails.TagId,
                        TagName = entity.Name,
                        EntityType = entityDetails.EntityType,
                        EntityId = entityDetails.EntityId,
                        Title = entityDetails.Title,
                        Description = entityDetails.Description,
                        Severity = entityDetails.Severity,
                        Status = entityDetails.Status,
                        AssigneeId = entityDetails.AssigneeId
                    });
                }
            }

            return result;
        }

    }
}