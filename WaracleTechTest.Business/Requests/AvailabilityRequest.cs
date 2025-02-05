namespace WaracleTechTest.Business.Requests
{
    public class AvailabilityRequest
    {
        public DateTime? EndDate { get; set; }

        public string? HotelName { get; set; }

        public int NumberOfPeople { get; set; }

        public DateTime? StartDate { get; set; }
    }
}
