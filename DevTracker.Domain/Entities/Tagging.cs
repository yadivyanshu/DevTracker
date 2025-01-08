using DevTracker.Domain.Enums;

namespace DevTracker.Domain.Entities
{
    public class Tagging
    {
        public int Id { get; set; }
        public int TagId { get; set; }
        public Tag Tag { get; set; }
        public int EntityId { get; set; }
        public EntityTypeEnum EntityType { get; set; } // Task, Bug, Feature
    }
}