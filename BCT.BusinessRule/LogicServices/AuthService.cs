using BCT.CommonLib.Models.AuthModels;
using BCT.CommonLib.Models.DataModels;
using BCT.DataAccess.DataRepositories;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BCT.BusinessRule.LogicServices;

public class AuthService(User userRepository, IConfiguration configuration)
{
    public async Task<bool> IsUserExistByEmailAsync(string email)
    {
        return await userRepository.IsUserExistByEmailAsync(email);
    }

    public async Task<LoginResponseModel?> LoginAsync(string email, string password)
    {
        var user = await userRepository.GetUserByEmailAsync(email);

        if (user == null)
            return null;

        bool isPasswordValid = BCrypt.Net.BCrypt.Verify(password, user.Password);

        if (!isPasswordValid)
            return null;

        return GenerateJWTToken(user);
    }

    public async Task<UserModel?> RegisterAsync(UserModel user)
    {
        return await userRepository.CreateUserAsync(user);
    }

    private LoginResponseModel GenerateJWTToken(UserModel user)
    {
        var claims = new[]
        {
            new Claim("UserId", user.UserId.ToString()),
            new Claim(ClaimTypes.Role, user.Role)
        };

        string secret = configuration["Jwt:Secret"]!;
        var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(secret));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: configuration["Jwt:Issuer"],
            audience: configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: creds);

        return new LoginResponseModel
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            TokenExpired = token.ValidTo
        };
    }
}
