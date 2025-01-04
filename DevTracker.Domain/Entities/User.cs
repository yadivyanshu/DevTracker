using DevTracker.Domain.Enums;

namespace DevTracker.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }  
        public string Email { get; set; }     
        public string PasswordHash { get; set; }  
        public string FullName { get; set; }
        public UserRole Role { get; set; }  
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public ICollection<Task> AssignedTasks { get; set; }
    }
}