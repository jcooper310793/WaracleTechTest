using WaracleTechTest.Data.Models;

namespace WaracleTechTest.Business.Interfaces
{
    public interface IHotelRepository
    {
        List<Room> GetFreeRooms(Hotel hotel, DateTime startDate, DateTime endDate);

        Task<Hotel?> GetHotelByNameAsync(string? hotelName);
    }
}