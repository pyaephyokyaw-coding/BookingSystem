using BCT.CommonLib.Models.DataModels;
using BCT.CommonLib.Services;
using BCT.DataAccess.DataRepositories;

namespace BCT.BusinessRule.LogicServices;

public class BookingService(Booking bookingRepository, IRedisCacheService redisCacheService)
{
    public async Task<List<BookingModel>?> GetAllBookingsAsync()
    {
        var cacheKey = "All_Bookings";
        var cacheBookings = redisCacheService.Get<List<BookingModel>>(cacheKey);
        if (cacheBookings != null)
        {
            return cacheBookings;
        }

        var bookingData = await bookingRepository.GetAllAsync();
        if (bookingData != null)
        {
            redisCacheService.Set(cacheKey, bookingData);
        }

        return bookingData;
    }

    public async Task<BookingModel?> GetBookingByIdAsync(int id)
    {
        var cacheKey = $"Booking_{id}";
        var cacheBooking = redisCacheService.Get<BookingModel>(cacheKey);
        if (cacheBooking != null)
        {
            return cacheBooking;
        }

        var bookingData = await bookingRepository.GetByIdAsync(id);
        if (bookingData != null)
        {
            redisCacheService.Set(cacheKey, bookingData);
        }

        return bookingData;
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
