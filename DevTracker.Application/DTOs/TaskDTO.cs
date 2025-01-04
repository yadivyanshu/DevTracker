using System.ComponentModel.DataAnnotations;
using DevTracker.Common.Validators;
using DevTracker.Domain.Enums;

namespace DevTracker.Application.DTOs
{
    public class TaskDTO
    {
        public int Id { get; set; }
        public int FeatureId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        [EnumValidation<FeatureTaskStatus>]
        public FeatureTaskStatus Status { get; set; }
        public int AssigneeId { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}