using DevTracker.Domain.Enums;

namespace DevTracker.Domain.Entities
{
    public class Discussion
    {
        public int Id { get; set; }
        public int EntityId { get; set; }
        public EntityTypeEnum EntityType { get; set; }

        public User CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int? ParentDiscussionId { get; set; }

        public string Content { get; set; }

        // Navigation properties
        public ICollection<Mention> Mentions { get; set; } = new List<Mention>();
        public ICollection<Reaction> Reactions { get; set; } = new List<Reaction>();
    }
}