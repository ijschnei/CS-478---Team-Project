namespace CS478_EventPlannerProject.DTOs.User
{
    public class UserProfileDto
    {
        public string UserId { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? DisplayName { get; set; }
        public string? Bio { get; set; }
        public string? ProfileImageUrl { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Location { get; set; }
        public string? Website { get; set; }
        public string? FacebookUrl { get; set; }
        public string? TwitterUrl { get; set; }
        public string? LinkedInUrl { get; set; }
        public bool IsPublic { get; set; } = true;
        public string FullName { get; set; } = string.Empty;
    }
}
