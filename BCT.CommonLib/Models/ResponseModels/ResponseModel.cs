namespace BCT.CommonLib.Models.ResponseModels;

public class ResponseModel
{
    private string Code { get; set; } = Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper();
    public string Message { get; set; } = string.Empty;
}
