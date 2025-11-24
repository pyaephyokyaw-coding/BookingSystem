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
public class PaymentController(PaymentService paymentService, ResponseService responseService) : Controller
{
    [HttpGet("{id}")]
    public async Task<IActionResult> GetPaymentById(int id)
    {
        try
        {
            var payment = await paymentService.GetPaymentByIdAsync(id);
            if (payment == null)
            {
                return NotFound(responseService.ResponseMessage(HttpStatusCode.NotFound));
            }
            return Ok(responseService.ResponseDataMessage(HttpStatusCode.OK, payment));
        }
        catch (Exception ex)
        {
            return StatusCode(500, responseService.ResponseMessage(ex.Message));
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAllPayments()
    {
        try
        {
            var payments = await paymentService.GetAllPaymentsAsync();
            return Ok(responseService.ResponseDataMessage(HttpStatusCode.OK, payments));
        }
        catch (Exception ex)
        {
            return StatusCode(500, responseService.ResponseMessage(ex.Message));
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreatePayment([FromBody] PaymentModel payment)
    {
        try
        {
            var createdPayment = await paymentService.CreatePaymentAsync(payment);
            return CreatedAtAction(nameof(GetPaymentById), new { id = createdPayment.PaymentId }, responseService.ResponseDataMessage(HttpStatusCode.Created, createdPayment));
        }
        catch (Exception ex)
        {
            return StatusCode(500, responseService.ResponseMessage(ex.Message));
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePayment(int id, [FromBody] PaymentModel payment)
    {
        try
        {
            var updated = await paymentService.UpdatePaymentAsync(id, payment);
            if (!updated)
            {
                return NotFound(responseService.ResponseMessage(HttpStatusCode.NotFound));
            }
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, responseService.ResponseMessage(ex.Message));
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePayment(int id)
    {
        try
        {
            var deleted = await paymentService.DeletePaymentAsync(id);
            if (!deleted)
            {
                return NotFound(responseService.ResponseMessage(HttpStatusCode.NotFound));
            }
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, responseService.ResponseMessage(ex.Message));
        }
    }
}
