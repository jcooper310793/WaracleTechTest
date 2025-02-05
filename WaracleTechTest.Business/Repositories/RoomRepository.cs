using WaracleTechTest.Business.Interfaces;
using WaracleTechTest.Data.Models;

namespace WaracleTechTest.Business.Repositories
{
    public class RoomRepository : IRoomRepository
    {
        public RoomRepository()
        {
        }

        public bool DoesRoomHaveSpaceAvailable(Room room, int capacity)
        {
            var roomType = room.RoomType;

            return roomType != null && roomType.Capacity >= capacity;
        }

        public bool IsRoomFreeDuringDates(Room room, DateTime startDate, DateTime endDate)
        {
            var bookings = room.Bookings.ToList();

            if (bookings.Count == 0)
            {
                return true;
            }

            foreach (var booking in bookings)
            {
                var bookingEndDate = booking.EndDate;

                if (bookingEndDate != null && bookingEndDate.Value.CompareTo(startDate) == -1)
                {
                    continue;
                }

                var bookingStartDate = booking.StartDate;

                if (bookingStartDate != null && bookingStartDate.Value.CompareTo(endDate) == 1)
                {
                    continue;
                }

                return false;
            }

            return true;
        }
    }
}