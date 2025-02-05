namespace WaracleTechTest.Business.Interfaces
{
    public interface IDatabaseRepository
    {
        Task ResetDatabaseAsync();

        Task SeedHotelsAsync();

        Task SeedRoomsAsync();

        Task SeedRoomTypesAsync();
    }
}