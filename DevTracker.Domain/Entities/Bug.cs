using DevTracker.Domain.Enums;
using System;

namespace DevTracker.Domain.Entities
{
    public class Bug
    {
        public int Id { get; set; }
        public int FeatureId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public BugSeverity Severity { get; set; }
        public BugStatus Status { get; set; }
        public int AssigneeId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public Feature Feature { get; set; } // Navigation property
        public User Assignee { get; set; } // Navigation property
    }
}