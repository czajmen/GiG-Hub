using System.ComponentModel.DataAnnotations;

namespace GigHub.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = " {0} Musi byæ d³u¿esze ni¿ {2} znaki", MinimumLength = 6)]
        [Display(Name = "Imie i Nazwisko")]
        public string Name { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = " {0} Musi byæ d³u¿esze ni¿ {2} znaki", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Has³o")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "PotwierdŸ has³o")]
        [Compare("Password", ErrorMessage = "Has³a nie pasuj¹ do siebie.")]
        public string ConfirmPassword { get; set; }
    }
}