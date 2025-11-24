namespace BCT.CommonLib.Models.AuthModels;

public class LoginResponseModel
{
    public string Token { get; set; } = string.Empty;
    public DateTime TokenExpired { get; set; }
}
