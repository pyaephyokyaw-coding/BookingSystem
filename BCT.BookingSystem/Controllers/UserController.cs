using BCT.BusinessRule.LogicServices;
using BCT.CommonLib.Models.DataModels;
using BCT.CommonLib.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BCT.BookingSystem.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController(UserService userService, ResponseService responseService) : ControllerBase
{

    [HttpGet(("{id}"))]
    public IActionResult GetUserById(int id)
    {
        try
        {
            var isUserExit = userService.IsUserExistByUserId(id);
            if (!isUserExit)
            {
                return NotFound(responseService.ResponseMessage(HttpStatusCode.NotFound));
            }

            var user = userService.GetUserById(id);
            return Ok(responseService.ResponseDataMessage(HttpStatusCode.OK, user));
        }
        catch (Exception ex)
        {
            return StatusCode(500, responseService.ResponseMessage(HttpStatusCode.InternalServerError));
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetUsersAsync([FromQuery] UserModel? user)
    {
        try
        {
            var users = await userService.GetUsersAsync(user);

            if (users == null || !users.Any())
            {
                return NotFound(responseService.ResponseMessage(HttpStatusCode.NotFound));
            }

            return Ok(responseService.ResponseDataMessage(HttpStatusCode.OK, users));
        }
        catch (Exception)
        {
            return StatusCode(500, responseService.ResponseMessage(HttpStatusCode.InternalServerError));
        }
    }


    [HttpPost]
    public IActionResult CreateUser([FromBody] UserModel user)
    {
        try
        {
            var isUserExit = userService.IsUserExist(user);
            if (isUserExit)
            {
                return Conflict(responseService.ResponseMessage("User with the same email already exists."));
            }

            var createdUser = userService.CreateUser(user);
            return Ok(responseService.ResponseDataMessage(HttpStatusCode.OK, createdUser));
        }
        catch (Exception ex)
        {
            return StatusCode(500, responseService.ResponseMessage(HttpStatusCode.InternalServerError));
        }
    }

    [Authorize]
    [HttpPut]
    public IActionResult UpdateUser([FromBody] UserModel user)
    {
        try
        {
            var isUserExit = userService.IsUserExistByUserId(user.UserId);
            if (!isUserExit)
            {
                return NotFound(responseService.ResponseMessage(HttpStatusCode.NotFound));
            }

            var result = userService.UpdateUser(user);
            if (!result)
            {
                return StatusCode(500, $"An error occurred while updating the User : {user!.FullName}.");
            }

            return Ok(responseService.ResponseMessage("Successful updated."));
        }
        catch (Exception ex)
        {
            var response = responseService.ResponseMessage(HttpStatusCode.InternalServerError);
            return StatusCode(500, response);
        }
    }

    [Authorize]
    [HttpDelete("{id}")]
    public IActionResult DeleteUser(int id)
    {
        try
        {
            var isUserExit = userService.IsUserExistByUserId(id);
            if (!isUserExit)
            {
                return NotFound(responseService.ResponseMessage(HttpStatusCode.NotFound));
            }

            var user = userService.GetUserById(id);
            var result = userService.DeleteUser(id);
            if (!result)
            {
                return StatusCode(500, $"An error occurred while deleting the User : {user!.FullName}.");
            }

            return Ok(responseService.ResponseMessage($"User : {user!.FullName} deleted successfully."));
        }
        catch (Exception ex)
        {
            return StatusCode(500, responseService.ResponseMessage(HttpStatusCode.InternalServerError));
        }
    }
}
