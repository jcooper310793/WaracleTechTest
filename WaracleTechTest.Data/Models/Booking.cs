using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WaracleTechTest.Data.Models
{
    public class Booking
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? BookingId { get; set; }

        public DateTime? EndDate { get; set; }

        public string? GuestName { get; set; }

        public Room? Room { get; set; }

        public int? RoomId { get; set; }

        public DateTime? StartDate { get; set; }
    }
}