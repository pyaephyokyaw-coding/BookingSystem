using System.ComponentModel.DataAnnotations;

namespace BCT.CommonLib.Models.AuthModels;

public class LoginModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }
}
