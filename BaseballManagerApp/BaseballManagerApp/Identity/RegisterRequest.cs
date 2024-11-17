using System.ComponentModel.DataAnnotations;

namespace BaseballManagerApp.Identity;

public class RegisterRequest
{
    [Required]
    [Display(Name = "Fullname")]
    public string Name { get; set; } = "";

    [Required]
    [EmailAddress]
    public string Email { get; set; } = "";

    [Required]
    [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at MAX {1} characters long.", MinimumLength = 6)]
    [DataType(DataType.Password)]
    public string Password { get; set; } = "";

    [DataType(DataType.Password)]
    [Display(Name = "Confirm Password")]
    [Compare("Password", ErrorMessage = "The password and configuration password do not match.")]
    public string ConfirmPassword { get; set; } = "";
}
