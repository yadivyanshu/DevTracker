using DevTracker.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace DevTracker.Application.DTOs
{
    public class UpdateUserDTO
    {
        [Required]
        [StringLength(255)]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [StringLength(200)]
        public string FullName { get; set; }

        [Required]
        public UserRole Role { get; set; }
    }
}