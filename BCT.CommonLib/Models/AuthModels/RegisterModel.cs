using System.ComponentModel.DataAnnotations;

namespace BCT.CommonLib.Models.AuthModels;

public class RegisterModel
{
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
}
