using BCT.BusinessRule.LogicServices;
using BCT.CommonLib.Models.AuthModels;
using BCT.CommonLib.Models.DataModels;
using BCT.CommonLib.Services;
using Microsoft.AspNetCore.Mvc;

namespace BCT.BookingSystem.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController(AuthService authService, ResponseService responseService) : ControllerBase
{
    [HttpPost("register")]
    public IActionResult Register([FromBody] UserModel user)
    {
        try
        {
            var hasData = authService.IsUserExistByEmail(user.Email);
            if (hasData)
            {
                return BadRequest(responseService.ResponseMessage("User already exists."));
            }

            var userData = authService.Register(user);
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
    public IActionResult Login([FromBody] LoginModel login)
    {
        if (login == null)
            return BadRequest(new { message = "Request body is invalid" });

        try
        {
            var loginResponseModel = authService.Login(login.Email, login.Password);

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
