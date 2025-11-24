using BCT.BusinessRule.LogicServices;
using BCT.CommonLib.Models.AuthModels;
using BCT.CommonLib.Models.DataModels;
using BCT.CommonLib.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BCT.BookingSystem.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController(AuthService authService, ResponseService responseService) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserModel user)
    {
        try
        {
            var hasData = await authService.IsUserExistByEmailAsync(user.Email);
            if (hasData)
            {
                return BadRequest(responseService.ResponseMessage("User already exists."));
            }

            var userData =await authService.RegisterAsync(user);
            if (userData == null)
            {
                return BadRequest(responseService.ResponseMessage("Registration failed."));
            }

            return Ok(responseService.ResponseMessage("Registration success."));
        }
        catch (Exception ex)
        {
            return StatusCode(500, responseService.ResponseMessage("Internal server error: " + ex.Message));
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel login)
    {
        if (login == null)
            return BadRequest(new { message = "Request body is invalid" });

        try
        {
            var loginResponseModel = await authService.LoginAsync(login.Email, login.Password);

            if (string.IsNullOrEmpty(loginResponseModel?.Token))
                return Unauthorized(new { message = "Invalid email or password" });

            return Ok(loginResponseModel);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Internal server error", error = ex.Message });
        }
    }

}
