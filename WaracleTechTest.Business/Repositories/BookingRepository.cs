using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using WaracleTechTest.Business.Interfaces;
using WaracleTechTest.Business.Requests;
using WaracleTechTest.Data.Models;
using WaracleTechTest.Migrations;

namespace WaracleTechTest.Business.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly WaracleContext _context;
        private readonly IHotelRepository _hotelRepository;
        private readonly IRoomRepository _roomRepository;

        public BookingRepository(
            WaracleContext context,
            IHotelRepository hotelRepository,
            IRoomRepository roomRepository)
        {
            _context = context;
            _hotelRepository = hotelRepository;
            _roomRepository = roomRepository;
        }

        public async Task<Booking> BookRoomAsync(BookingRequest bookingRequest)
        {
            var hotel = await _hotelRepository.GetHotelByNameAsync(bookingRequest.HotelName) ??
                throw new JsonException("No hotel found in the database!");

            var freeRooms = _hotelRepository.GetFreeRooms(hotel, bookingRequest.StartDate, bookingRequest.EndDate);

            if (freeRooms.Count == 0)
            {
                throw new JsonException("No free rooms found in the hotel!");
            }

            var availableRooms = freeRooms
                .Where(x => _roomRepository.DoesRoomHaveSpaceAvailable(x, bookingRequest.NumberOfBeds))
                .OrderBy(x => x.RoomTypeId)
                .ToList();

            if (availableRooms.Count == 0)
            {
                throw new JsonException("No rooms found with enough capacity to support this booking!");
            }

            var booking = new Booking
            {
                EndDate = bookingRequest.EndDate,
                GuestName = bookingRequest.GuestName,
                RoomId = availableRooms[0].RoomId,
                StartDate = bookingRequest.StartDate
            };

            await _context.Bookings.AddAsync(booking);

            await _context.SaveChangesAsync();

            return booking;
        }

        public async Task<Booking?> GetBookingAsync(int bookingReference)
        {
            return await _context.Bookings.SingleOrDefaultAsync(x => x.BookingId == bookingReference);
        }
    }
}