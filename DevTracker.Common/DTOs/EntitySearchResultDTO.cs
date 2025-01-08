using DevTracker.Domain.Enums;

namespace DevTracker.Common.DTOs
{
    public class EntitySearchResultDTO
    {
        public int EntityId { get; set; }
        public EntityTypeEnum EntityType { get; set; }
        public string Title { get; set; }
        public string? Status { get; set; }
        public int? AssigneeId { get; set; }
    }
}