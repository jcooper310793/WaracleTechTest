using WaracleTechTest.Data.Models;

namespace WaracleTechTest.Business.Responses
{
    public class RoomResponse
    {
        public RoomResponse(Room room, bool populateBookingInfo = true)
        {
            Bookings = populateBookingInfo ? room.Bookings.Select(x => new BookingResponse(x)).ToList() : [];

            var roomType = room.RoomType;

            if (roomType != null)
            {
                Capacity = roomType.Capacity;
                RoomType = roomType.RoomTypeName;
            }
        }

        public List<BookingResponse> Bookings { get; set; }

        public int? Capacity { get; set; }

        public string? RoomType { get; set; }
    }
}