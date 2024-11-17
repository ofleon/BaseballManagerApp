using System.ComponentModel.DataAnnotations;

namespace BaseballManagerApp.Identity;

public sealed class UserRequest
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;

    [Display(Name = "Remember me?")]
    public bool RememberMe { get; set; }
}
