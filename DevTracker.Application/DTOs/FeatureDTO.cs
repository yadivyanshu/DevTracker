using DevTracker.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace DevTracker.API.DTOs
{
    public class FeatureDTO
    {
        public int Id { get; set; }
        [Required]
        public int ProjectId { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        [Required]
        public FeatureStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}