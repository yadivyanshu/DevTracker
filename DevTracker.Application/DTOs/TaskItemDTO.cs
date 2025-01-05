using DevTracker.Common.Validators;
using DevTracker.Domain.Enums;

namespace DevTracker.Application.DTOs
{
    public class TaskItemDTO
    {
        public int Id { get; set; }
        public int FeatureId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        [EnumValidation<TaskItemStatus>]
        public TaskItemStatus Status { get; set; }
        public int AssigneeId { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}