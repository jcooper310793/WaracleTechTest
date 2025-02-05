using Microsoft.EntityFrameworkCore;
using WaracleTechTest.Business.Interfaces;
using WaracleTechTest.Data.Models;
using WaracleTechTest.Migrations;

namespace WaracleTechTest.Business.Repositories
{
    public class HotelRepository : IHotelRepository
    {
        private readonly WaracleContext _context;
        private readonly IRoomRepository _roomRepository;

        public HotelRepository(WaracleContext context, IRoomRepository roomRepository)
        {
            _context = context;
            _roomRepository = roomRepository;
        }

        public List<Room> GetFreeRooms(Hotel hotel, DateTime startDate, DateTime endDate)
        {
            return hotel.Rooms.Where(x => _roomRepository.IsRoomFreeDuringDates(x, startDate, endDate)).ToList();
        }

        public async Task<Hotel?> GetHotelByNameAsync(string? hotelName)
        {
            return await _context.Hotels
                .Include(x => x.Rooms).ThenInclude(x => x.Bookings)
                .Include(x => x.Rooms).ThenInclude(x => x.RoomType)
                .AsSplitQuery()
                .SingleOrDefaultAsync(x => x.Name == hotelName);
        }
    }
}