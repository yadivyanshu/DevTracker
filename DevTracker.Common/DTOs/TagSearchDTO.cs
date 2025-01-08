using DevTracker.Domain.Enums;

namespace DevTracker.Common.DTOs
{
    public class TagSearchDTO
    {
        public int? TagId { get; set; }
        public string? TagName { get; set; }
        public EntityTypeEnum? EntityType { get; set; }
        public string? Status { get; set; } 
        public int? AssigneeId { get; set; }
        public int? EntityId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Severity { get; set; }
        public List<int>? MultipleTagIds { get; set; }
    }
}