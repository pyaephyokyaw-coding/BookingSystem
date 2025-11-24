using BCT.BusinessRule.LogicServices;
using BCT.CommonLib.Models.DataModels;
using BCT.CommonLib.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BCT.BookingSystem.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class BookingController(BookingService bookingService, ResponseService responseService) : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<IActionResult> GetBookingById(int id)
    {
        try
        {
            var response = await bookingService.GetBookingByIdAsync(id);
            if (response == null)
            {
                return NotFound(responseService.ResponseMessage(HttpStatusCode.NotFound));
            }
            return Ok(responseService.ResponseDataMessage(HttpStatusCode.OK, response));
        }
        catch (Exception ex)
        {
            return StatusCode(500, responseService.ResponseMessage(ex.Message));
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAllBookings()
    {
        try
        {
            var response = await bookingService.GetAllBookingsAsync();
            if (response == null)
            {
                return NotFound(responseService.ResponseMessage(HttpStatusCode.NotFound));
            }

            return Ok(responseService.ResponseDataMessage(HttpStatusCode.OK, response));
        }
        catch (Exception ex)
        {
            return StatusCode(500, responseService.ResponseMessage(ex.Message));
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateBooking([FromBody] BookingModel booking)
    {
        try
        {
            var response = await bookingService.CreateBookingAsync(booking);
            return CreatedAtAction(nameof(GetBookingById), new { id = response.BookingId }, responseService.ResponseDataMessage(HttpStatusCode.Created, response));
        }
        catch (Exception ex)
        {
            return StatusCode(500, responseService.ResponseMessage(ex.Message));
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBooking(int id, [FromBody] BookingModel booking)
    {
        try
        {
            var isUpdated = await bookingService.UpdateBookingAsync(id, booking);
            if (!isUpdated)
            {
                return NotFound(responseService.ResponseMessage(HttpStatusCode.NotFound));
            }
            return Ok(responseService.ResponseMessage("Booking updated successfully."));
        }
        catch (Exception ex)
        {
            return StatusCode(500, responseService.ResponseMessage(ex.Message));
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBooking(int id)
    {
        try
        {
            var isDeleted = await bookingService.DeleteBookingAsync(id);
            if (!isDeleted)
            {
                return NotFound(responseService.ResponseMessage(HttpStatusCode.NotFound));
            }
            return Ok(responseService.ResponseMessage("Booking deleted successfully."));
        }
        catch (Exception ex)
        {
            return StatusCode(500, responseService.ResponseMessage(ex.Message));
        }
    }
}
