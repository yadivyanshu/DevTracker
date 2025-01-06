using DevTracker.Domain.Enums;

namespace DevTracker.Application.DTOs
{
    public class UpdateBugDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public BugSeverity Severity { get; set; }
        public BugStatus Status { get; set; }
        public int AssigneeId { get; set; }
    }
}