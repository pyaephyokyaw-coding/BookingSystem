using BCT.CommonLib.Models.DataModels;
using Microsoft.EntityFrameworkCore;

namespace BCT.DataAccess.Data;

public class BookingSystemDbContext : DbContext
{
    public BookingSystemDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
    {

    }

    public DbSet<UserModel> Users { get; set; }

    public DbSet<BookingModel> Bookings { get; set; }

    public DbSet<PaymentModel> Payments { get; set; }

    public DbSet<AuditLogModel> AuditLogs { get; set; }

}
