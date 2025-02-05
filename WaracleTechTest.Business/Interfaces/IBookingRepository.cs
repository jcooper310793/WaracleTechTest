using WaracleTechTest.Business.Requests;
using WaracleTechTest.Data.Models;

namespace WaracleTechTest.Business.Interfaces
{
    public interface IBookingRepository
    {
        Task<Booking> BookRoomAsync(BookingRequest bookingRequest);

        Task<Booking?> GetBookingAsync(int bookingReference);
    }
}