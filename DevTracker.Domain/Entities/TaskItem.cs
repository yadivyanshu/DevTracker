using DevTracker.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace DevTracker.Domain.Entities
{
    public class TaskItem
    {
        public int Id { get; set; }
        public int FeatureId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public TaskItemStatus Status { get; set; }
        public int AssigneeId { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Navigation Properties
        public User Assignee { get; set; }
        public Feature Feature { get; set; }
    }
}