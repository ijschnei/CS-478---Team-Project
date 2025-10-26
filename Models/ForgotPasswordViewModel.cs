using System.ComponentModel.DataAnnotations;

namespace CS478_EventPlannerProject.Models
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}