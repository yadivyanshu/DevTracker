namespace DevTracker.Domain.Entities
{
    public class Mention
    {
        public int Id { get; set; }
        public int DiscussionId { get; set; } 
        public int MentionedUserId { get; set; } 

        public Discussion Discussion { get; set; } 
        public User MentionedUser { get; set; } 
    }
}