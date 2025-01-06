using DevTracker.Domain.Enums;

namespace DevTracker.Application.DTOs
{
    public class BugDTO
    {
        public int Id { get; set; }
        public int FeatureId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public BugSeverity Severity { get; set; }
        public BugStatus Status { get; set; }
        public int AssigneeId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}