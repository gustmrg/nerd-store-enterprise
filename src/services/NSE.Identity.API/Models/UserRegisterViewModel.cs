using System.ComponentModel.DataAnnotations;

namespace NSE.Identity.API.Models;

public class UserRegisterViewModel
{
    [Required(ErrorMessage = "Field {0} is required")]
    [EmailAddress(ErrorMessage = "Field {0} is invalid")]
    public string Email { get; set; }
    
    [Required(ErrorMessage = "Field {0} is required")]
    [StringLength(100, ErrorMessage = "Field {0} must be between {2} and {1} characters", MinimumLength = 6)]
    public string Password { get; set; }
    
    [Compare("Password", ErrorMessage = "Passwords do not match")]
    public string ConfirmPassword { get; set; }
}