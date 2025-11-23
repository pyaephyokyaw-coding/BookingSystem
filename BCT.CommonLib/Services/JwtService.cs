using BCT.CommonLib.Models.DataModels;

namespace BCT.CommonLib.Services;

public interface IJwtService
{
    string GenerateToken(UserModel user);
}

public class JwtService : IJwtService
{
    public JwtService()
    {

    }

    public string GenerateToken(UserModel user)
    {
        return "";
    }
}
