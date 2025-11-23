using BCT.BusinessRule.LogicServices;
using BCT.CommonLib.Models.DataModels;
using BCT.CommonLib.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BCT.BookingSystem.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly UserService _userService;
    private readonly ResponseService _responseService;
    public UserController(UserService userService, ResponseService responseService)
    {
        _userService = userService;
        _responseService = responseService;
    }

    [HttpGet(("{id}"))]
    public IActionResult GetUserById(int id)
    {
        try
        {
            var isUserExit = _userService.IsUserExist(id);
            if (!isUserExit)
            {
                return NotFound(_responseService.ResponseMessage(HttpStatusCode.NotFound));
            }

            var user = _userService.GetUserById(id);
            return Ok(_responseService.ResponseDataMessage(HttpStatusCode.OK, user));
        }
        catch (Exception ex)
        {
            return StatusCode(500, _responseService.ResponseMessage(HttpStatusCode.InternalServerError));
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetUsersAsync([FromQuery] UserModel? user)
    {
        try
        {
            var users = await _userService.GetUsersAsync(user);

            if (users == null || !users.Any())
            {
                return NotFound(_responseService.ResponseMessage(HttpStatusCode.NotFound));
            }

            return Ok(_responseService.ResponseDataMessage(HttpStatusCode.OK, users));
        }
        catch (Exception)
        {
            return StatusCode(500, _responseService.ResponseMessage(HttpStatusCode.InternalServerError));
        }
    }


    [HttpPost]
    public IActionResult CreateUser([FromBody] UserModel user)
    {
        try
        {
            var isUserExit = _userService.IsUserExist(user.UserId);
            if (isUserExit)
            {
                return Conflict(_responseService.ResponseMessage("User with the same email already exists."));
            }

            var createdUser = _userService.CreateUser(user);
            return Ok(_responseService.ResponseDataMessage(HttpStatusCode.OK, createdUser));
        }
        catch (Exception ex)
        {
            return StatusCode(500, _responseService.ResponseMessage(HttpStatusCode.InternalServerError));
        }
    }

    [Authorize]
    [HttpPut]
    public IActionResult UpdateUser([FromBody] UserModel user)
    {
        try
        {
            var isUserExit = _userService.IsUserExist(user.UserId);
            if (!isUserExit)
            {
                return NotFound(_responseService.ResponseMessage(HttpStatusCode.NotFound));
            }

            var result = _userService.UpdateUser(user);
            if (!result)
            {
                return StatusCode(500, $"An error occurred while updating the User : {user!.FullName}.");
            }

            return Ok(_responseService.ResponseMessage(HttpStatusCode.OK));
        }
        catch (Exception ex)
        {
            var response = _responseService.ResponseMessage(HttpStatusCode.InternalServerError);
            return StatusCode(500, response);
        }
    }

    [Authorize]
    [HttpDelete("{id}")]
    public IActionResult DeleteUser(int id)
    {
        try
        {
            var isUserExit = _userService.IsUserExist(id);
            if (!isUserExit)
            {
                return NotFound(_responseService.ResponseMessage(HttpStatusCode.NotFound));
            }

            var user = _userService.GetUserById(id);
            var result = _userService.DeleteUser(id);
            if (!result)
            {
                return StatusCode(500, $"An error occurred while deleting the User : {user!.FullName}.");
            }

            return Ok(_responseService.ResponseMessage($"User : {user!.FullName} deleted successfully."));
        }
        catch (Exception ex)
        {
            return StatusCode(500, _responseService.ResponseMessage(HttpStatusCode.InternalServerError));
        }
    }
}
