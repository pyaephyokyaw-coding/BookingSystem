using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BCT.CommonLib.Models.DataModels;

public class PaymentModel
{
    [Key]
    public int PaymentId { get; set; }

    [Required]
    public int BookingId { get; set; }

    [Required]
    [Column(TypeName = "decimal(10,2)")]
    public decimal Amount { get; set; }

    [MaxLength(50)]
    public string PaymentType { get; set; }

    public DateTime? PaidAt { get; set; }

    [MaxLength(50)]
    public string Status { get; set; } = "Pending";

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    //public BookingModel Booking { get; set; }
}
