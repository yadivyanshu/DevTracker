using System;
using System.ComponentModel.DataAnnotations;
using DevTracker.Common.Validators;
using DevTracker.Domain.Enums;

namespace DevTracker.Application.DTOs{
    public class UpdateTaskDTO
    {
        [Required]
        [StringLength(255)]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required]
        [EnumValidation<FeatureTaskStatus>]
        public FeatureTaskStatus Status { get; set; } 

        [Required]
        public int AssigneeId { get; set; }

        public DateTime DueDate { get; set; }
    }
}