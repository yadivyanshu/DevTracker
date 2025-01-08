using DevTracker.Domain.Enums;

namespace DevTracker.API.DTOs
{
    public class CreateDiscussionDTO
    {
        public int EntityId { get; set; }
        public EntityTypeEnum EntityType { get; set; }
        public string CreatedByUser { get; set; }
        public int? ParentDiscussionId { get; set; }
        public string Content { get; set; }
    }
}