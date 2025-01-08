using DevTracker.Domain.Enums;

namespace DevTracker.Common.DTOs
{
    public class TagSearchDTO
    {
        public int? TagId { get; set; }
        public EntityTypeEnum? EntityType { get; set; }
        public string? Status { get; set; } 
        public int? AssigneeId { get; set; }
        public List<int>? MultipleTagIds { get; set; }
    }
}