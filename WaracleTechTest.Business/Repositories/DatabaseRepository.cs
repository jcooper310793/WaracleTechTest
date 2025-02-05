using Bogus;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Extensions;
using WaracleTechTest.Business.Interfaces;
using WaracleTechTest.Data.Enums;
using WaracleTechTest.Data.Models;
using WaracleTechTest.Migrations;

namespace WaracleTechTest.Business.Repositories
{
    public class DatabaseRepository : IDatabaseRepository
    {
        private readonly WaracleContext _context;

        public DatabaseRepository(WaracleContext context)
        {
            _context = context;
        }

        public async Task ResetDatabaseAsync()
        {
            var bookings = await _context.Bookings.ToListAsync();

            _context.Bookings.RemoveRange(bookings);

            var rooms = await _context.Rooms.ToListAsync();

            _context.Rooms.RemoveRange(rooms);

            var roomTypes = await _context.RoomTypes.ToListAsync();

            _context.RoomTypes.RemoveRange(roomTypes);

            var hotels = await _context.Hotels.ToListAsync();

            _context.Hotels.RemoveRange(hotels);

            await _context.SaveChangesAsync();
        }

        public async Task SeedHotelsAsync()
        {
            var existingHotels = await _context.Hotels.ToListAsync();

            if (existingHotels.Count > 0)
            {
                return;
            }

            var hotelGenerator = new Faker<Hotel>()
                .RuleFor(x => x.MaxRoomCount, f => 6)
                .RuleFor(x => x.Name, f => $"The {f.Commerce.Color()} {f.Hacker.Noun()}");

            var hotels = hotelGenerator.Generate(10);

            foreach (var hotel in hotels)
            {
                await _context.Hotels.AddAsync(hotel);
            }

            await _context.SaveChangesAsync();
        }

        public async Task SeedRoomsAsync()
        {
            var hotels = await _context.Hotels.ToListAsync();

            var roomTypeIds = await _context.RoomTypes.Select(x => x.RoomTypeId).ToListAsync();

            foreach (var hotel in hotels)
            {
                var roomGenerator = new Faker<Room>()
                    .RuleFor(x => x.HotelId, f => hotel.HotelId)
                    .RuleFor(x => x.RoomTypeId, f => f.PickRandom(roomTypeIds));

                while (hotel.Rooms.Count < 6)
                {
                    var room = roomGenerator.Generate();

                    hotel.Rooms.Add(room);
                }
            }

            await _context.SaveChangesAsync();
        }

        public async Task SeedRoomTypesAsync()
        {
            var roomTypes = GetRoomTypesToSeed();

            foreach (var roomType in roomTypes)
            {
                var existingRoomType = await _context.RoomTypes
                    .FirstOrDefaultAsync(x => x.RoomTypeName == roomType.RoomTypeName);

                if (existingRoomType == null)
                {
                    await _context.RoomTypes.AddAsync(roomType);
                }
                else
                {
                    existingRoomType.Capacity = roomType.Capacity;
                }
            }

            await _context.SaveChangesAsync();
        }

        private static List<RoomType> GetRoomTypesToSeed()
        {
            return
            [
                new RoomType
                {
                    Capacity = 1,
                    RoomTypeName = RoomTypeCategory.Single.GetDisplayName()
                },
                new RoomType
                {
                    Capacity = 2,
                    RoomTypeName = RoomTypeCategory.Double.GetDisplayName()
                },
                new RoomType
                {
                    Capacity = 4,
                    RoomTypeName = RoomTypeCategory.Deluxe.GetDisplayName()
                }
            ];
        }
    }
}