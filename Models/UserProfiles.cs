using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace CS478_EventPlannerProject.Models
{
    public class UserProfiles
    {
        [Key]
        public int UserProfileId { get; set; }
        
        [ForeignKey("UserId")]
        public string UserId { get; set; } = string.Empty;
        [MaxLength(50)]
        public string? FirstName {  get; set; }
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

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Required]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public virtual Users? User { get; set; }
        // Computed property
        [NotMapped]
        public string FullName => !string.IsNullOrEmpty(FirstName) && !string.IsNullOrEmpty(LastName)
            ? $"{FirstName} {LastName}"
            : DisplayName ?? "Unknown User";

    }
}
