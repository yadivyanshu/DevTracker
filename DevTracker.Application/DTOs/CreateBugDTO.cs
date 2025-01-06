using DevTracker.Domain.Enums;

namespace DevTracker.Application.DTOs
{
    public class CreateBugDTO
    {
        public int FeatureId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public BugSeverity Severity { get; set; }
        public int AssigneeId { get; set; }
    }
}