using System.ComponentModel.DataAnnotations;

namespace GymManager.Models
{
    public class RegisterViewModel
    {
        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [StringLength(255)]
        [Required]
        public string Surname { get; set; }

        [StringLength(255)]
        [Required]
        [Display(Name = "Job Title")]
        public string JobTitle { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d\w\W]{6,}$", ErrorMessage = "Password must contain minimum 6 characters, at least one lower- and uppercase and a number.")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
