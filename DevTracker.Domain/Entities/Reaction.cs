namespace DevTracker.Domain.Entities{
    public class Reaction
    {
        public int Id { get; set; }
        public int DiscussionId { get; set; } 
        public int UserId { get; set; } 
        public string ReactionType { get; set; } 
        public DateTime ReactedAt { get; set; }

        public Discussion Discussion { get; set; }
        public User User { get; set; } 
    }
}