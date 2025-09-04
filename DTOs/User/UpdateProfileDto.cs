using System.ComponentModel.DataAnnotations;
namespace CS478_EventPlannerProject.DTOs.User
{
    public class UpdateProfileDto
    {
        [MaxLength(50)]
        public string? FirstName { get; set; }

        [MaxLength(50)]
        public string? LastName { get; set; }

        [MaxLength(100)]
        public string? DisplayName { get; set; }

        [MaxLength(1000)]
        public string? Bio { get; set; }

        [MaxLength(500)]
        public string? ProfileImageUrl { get; set; }

        public DateTime? DateOfBirth { get; set; }

        [MaxLength(200)]
        public string? Location { get; set; }

        [MaxLength(300)]
        public string? Website { get; set; }

        [MaxLength(300)]
        [Url]
        public string? FacebookUrl { get; set; }

        [MaxLength(300)]
        [Url]
        public string? TwitterUrl { get; set; }

        [MaxLength(300)]
        [Url]
        public string? LinkedInUrl { get; set; }

        public bool IsPublic { get; set; } = true;
    }
}
