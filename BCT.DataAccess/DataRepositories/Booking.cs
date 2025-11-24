using BCT.CommonLib.Models.DataModels;
using BCT.DataAccess.Data;
using Microsoft.EntityFrameworkCore;

namespace BCT.DataAccess.DataRepositories;

public class Booking(BookingSystemDbContext dbContext)
{
    public async Task<List<BookingModel>> GetAllAsync()
    {
        return await dbContext.Bookings.Where(x => true)
            .ToListAsync();
    }

    public async Task<BookingModel?> GetByIdAsync(int id)
    {
        return await dbContext.Bookings
            .FirstOrDefaultAsync(x => x.BookingId == id);
    }

    public async Task<BookingModel> CreateAsync(BookingModel booking)
    {
        dbContext.Bookings.Add(booking);
        await dbContext.SaveChangesAsync();
        return booking;
    }

    public async Task<bool> UpdateAsync(BookingModel booking)
    {
        var exist = await dbContext.Bookings.FindAsync(booking.BookingId);
        if (exist == null) return false;

        exist.BookingNumber = booking.BookingNumber;
        exist.ScheduleDate = booking.ScheduleDate;
        exist.Status = booking.Status;
        exist.UserId = booking.UserId;

        await dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var exist = await dbContext.Bookings.FindAsync(id);
        if (exist == null) return false;

        var payment = await dbContext.Payments
            .FirstOrDefaultAsync(x => x.BookingId == id);

        if (payment != null)
        {
            dbContext.Payments.Remove(payment);
            await dbContext.SaveChangesAsync();
        }

        dbContext.Bookings.Remove(exist);
        await dbContext.SaveChangesAsync();
        return true;
    }
}
