using WaracleTechTest.Data.Models;

namespace WaracleTechTest.Business.Responses
{
    public class BookingResponse
    {
        public BookingResponse(Booking booking)
        {
            BookingReference = booking.BookingId;
            GuestName = booking.GuestName;
            EndDate = booking.EndDate;
            StartDate = booking.StartDate;
        }

        public int? BookingReference { get; set; }

        public string? GuestName { get; set; }

        public DateTime? EndDate { get; set; }

        public DateTime? StartDate { get; set; }
    }
}