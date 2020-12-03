using System.ComponentModel.DataAnnotations;

namespace GymManager.Models
{
    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
