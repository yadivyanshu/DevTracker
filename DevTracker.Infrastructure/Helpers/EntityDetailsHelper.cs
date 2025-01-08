using DevTracker.Domain.Enums;
using DevTracker.Infrastructure.DataContext;
using DevTracker.Common.DTOs;
using Microsoft.EntityFrameworkCore;

namespace DevTracker.Infrastructure.Helpers
{
    public static class EntityDetailsHelper
    {
        public static async Task<TagSearchDTO> GetEntityDetailsAsync(ApplicationDbContext context, EntityTypeEnum entityType, int entityId)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));

            return entityType switch
            {
                EntityTypeEnum.Task => await context.TaskItems
                    .Where(ti => ti.Id == entityId)
                    .Select(ti => new TagSearchDTO
                    {
                        TagId = null, 
                        TagName = null, 
                        EntityType = EntityTypeEnum.Task,
                        EntityId = ti.Id,
                        Status = ti.Status.ToString(),
                        AssigneeId = ti.AssigneeId,
                        Title = ti.Title,
                        Description = ti.Description,
                        Severity = null, 
                        MultipleTagIds = null 
                    })
                    .FirstOrDefaultAsync(),

                EntityTypeEnum.Bug => await context.Bugs
                    .Where(b => b.Id == entityId)
                    .Select(b => new TagSearchDTO
                    {
                        TagId = null,
                        TagName = null,
                        EntityType = EntityTypeEnum.Bug,
                        EntityId = b.Id,
                        Status = b.Status.ToString(),
                        AssigneeId = b.AssigneeId,
                        Title = b.Title,
                        Description = b.Description,
                        Severity = b.Severity.ToString(),
                        MultipleTagIds = null
                    })
                    .FirstOrDefaultAsync(),

                EntityTypeEnum.Feature => await context.Features
                    .Where(f => f.Id == entityId)
                    .Select(f => new TagSearchDTO
                    {
                        TagId = null,
                        TagName = null,
                        EntityType = EntityTypeEnum.Feature,
                        EntityId = f.Id,
                        Status = f.Status.ToString(),
                        AssigneeId = null,
                        Title = f.Title,
                        Description = f.Description,
                        Severity = null,
                        MultipleTagIds = null
                    })
                    .FirstOrDefaultAsync(),

                _ => null
            };
        }
    }
}