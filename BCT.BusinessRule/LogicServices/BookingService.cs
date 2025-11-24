using BCT.CommonLib.Models.DataModels;
using BCT.DataAccess.DataRepositories;

namespace BCT.BusinessRule.LogicServices;

public class BookingService(Booking bookingRepository)
{
    public async Task<List<BookingModel>> GetAllBookingsAsync()
    {
        return await bookingRepository.GetAllAsync();
    }

    public async Task<BookingModel?> GetBookingByIdAsync(int id)
    {
        return await bookingRepository.GetByIdAsync(id);
    }

    public async Task<BookingModel> CreateBookingAsync(BookingModel booking)
    {
        return await bookingRepository.CreateAsync(booking);
    }

    public async Task<bool> UpdateBookingAsync(int id, BookingModel booking)
    {
        booking.BookingId = id;
        return await bookingRepository.UpdateAsync(booking);
    }

    public async Task<bool> DeleteBookingAsync(int id)
    {
        return await bookingRepository.DeleteAsync(id);
    }
}
