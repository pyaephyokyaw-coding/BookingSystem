using System.ComponentModel.DataAnnotations;

namespace BCT.CommonLib.Models.DataModels;

public class ApiLogModel
{
    [Key]
    public int ApiLogId { get; set; }

    [MaxLength(50)]
    public string? ApiType { get; set; }

    public string? RequestValue { get; set; }

    public string? ResponseValue { get; set; }

    public int? UserId { get; set; }

    [MaxLength(50)]
    public string? Status { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
