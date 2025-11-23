using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BCT.CommonLib.Models;

public class BookingModel
{
    [Key]
    public int BookingId { get; set; }

    [Required]
    [ForeignKey("User")]
    public int UserId { get; set; }

    [Required]
    [MaxLength(50)]
    public string BookingNumber { get; set; }

    public DateTime? ScheduleDate { get; set; }

    [MaxLength(50)]
    public string Status { get; set; } = "Pending";

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    public UserModel User { get; set; }

    public ICollection<PaymentModel> Payments { get; set; }
}
