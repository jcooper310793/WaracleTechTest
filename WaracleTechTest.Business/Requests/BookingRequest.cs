namespace WaracleTechTest.Business.Requests
{
    public class BookingRequest
    {
        public int NumberOfBeds { get; set; }

        public DateTime EndDate { get; set; }

        public string GuestName { get; set; }

        public string HotelName { get; set; }

        public DateTime StartDate { get; set; }
    }
}