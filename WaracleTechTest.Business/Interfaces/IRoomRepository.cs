using WaracleTechTest.Data.Models;

namespace WaracleTechTest.Business.Interfaces
{
    public interface IRoomRepository
    {
        bool DoesRoomHaveSpaceAvailable(Room room, int capacity);

        bool IsRoomFreeDuringDates(Room room, DateTime startDate, DateTime endDate);
    }
}