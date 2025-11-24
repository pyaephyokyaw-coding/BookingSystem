using BCT.CommonLib.Models.DataModels;
using BCT.DataAccess.Data;
using Microsoft.EntityFrameworkCore;

namespace BCT.DataAccess.DataRepositories;

public class Payment(BookingSystemDbContext dbContext)
{
    public async Task<List<PaymentModel>> GetAllAsync()
    {
        return await dbContext.Payments
            .ToListAsync();
    }

    public async Task<PaymentModel?> GetByIdAsync(int id)
    {
        return await dbContext.Payments
            .FirstOrDefaultAsync(x => x.PaymentId == id);
    }

    public async Task<PaymentModel> CreateAsync(PaymentModel payment)
    {
        payment.CreatedAt = DateTime.Now;
        dbContext.Payments.Add(payment);
        await dbContext.SaveChangesAsync();
        return payment;
    }

    public async Task<bool> UpdateAsync(PaymentModel payment)
    {
        var exist = await dbContext.Payments.FindAsync(payment.PaymentId);
        if (exist == null) return false;

        exist.BookingId = payment.BookingId;
        exist.Amount = payment.Amount;
        exist.PaymentType = payment.PaymentType;
        exist.PaidAt = payment.PaidAt;
        exist.Status = payment.Status;
        exist.UpdatedAt = DateTime.Now;

        await dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var exist = await dbContext.Payments.FindAsync(id);
        if (exist == null) return false;

        dbContext.Payments.Remove(exist);
        await dbContext.SaveChangesAsync();
        return true;
    }
}
