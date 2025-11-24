using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BCT.CommonLib.Models.DataModels;

public class AuditLogModel
{
    [Key]
    public int AuditLogId { get; set; }

    public int? UserId { get; set; }

    [Required]
    [MaxLength(255)]
    public string Action { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime LogDate { get; set; } = DateTime.Now;

    //public UserModel User { get; set; }
}
