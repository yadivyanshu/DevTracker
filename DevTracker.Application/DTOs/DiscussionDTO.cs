namespace DevTracker.API.DTOs
{
    public class DiscussionDTO
    {
        public int Id { get; set; }
        public int EntityId { get; set; }
        public string EntityType { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int? ParentDiscussionId { get; set; }
        public string Content { get; set; }
        public ICollection<string> Mentions { get; set; } = new List<string>(); // Avoid null
        public ICollection<string> Reactions { get; set; } =  new List<string>(); // Avoid null
    }
}