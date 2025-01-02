using System.ComponentModel.DataAnnotations;

namespace DevTracker.Application.DTOs
{
    public class ProjectDTO
    {
        [Required]
        public string Name { get; set; }
        
        public string Description { get; set; }
    }
}