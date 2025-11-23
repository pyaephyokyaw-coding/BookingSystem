using BCT.CommonLib.Models.ResponseModels;
using System.Net;

namespace BCT.CommonLib.Services;

public class ResponseService
{
    public ResponseModel ResponseMessage(HttpStatusCode statusCode)
    {
        return new ResponseModel
        {
            Message = GetDefaultMessage(statusCode)
        };
    }

    public ResponseModel ResponseMessage(string message)
    {
        return new ResponseModel
        {
            Message = message
        };
    }

    public ResponseDataModel ResponseDataMessage<T>(HttpStatusCode statusCode, T data)
    {
        return new ResponseDataModel
        {
            Message = GetDefaultMessage(statusCode),
            Data = data
        };
    }

    public ResponseDataModel ResponseDataMessage<T>(string message, T data)
    {
        return new ResponseDataModel
        {
            Message = message,
            Data = data
        };
    }

    private string GetDefaultMessage(HttpStatusCode statusCode)
    {
        return statusCode switch
        {
            HttpStatusCode.OK => "Success!",
            HttpStatusCode.NotFound => "No data Found!",
            HttpStatusCode.InternalServerError => "Please contact to support.",
            HttpStatusCode.Created => "Successful created.",
            HttpStatusCode.BadRequest => "Bad request.",
            _ => string.Empty,
        };
    }
}
