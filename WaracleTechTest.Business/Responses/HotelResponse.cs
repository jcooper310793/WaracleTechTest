using WaracleTechTest.Data.Models;

namespace WaracleTechTest.Business.Responses
{
    public class HotelResponse
    {
        public HotelResponse(Hotel hotel)
        {
            HotelName = hotel.Name;

            Rooms = hotel.Rooms.Select(x => new RoomResponse(x)).ToList();
        }

        public string? HotelName { get; set; }

        public List<RoomResponse> Rooms { get; set; }
    }
}