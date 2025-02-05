using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WaracleTechTest.Data.Models
{
    public class Room
    {
        public Room()
        {
            Bookings = [];
        }

        [ForeignKey("RoomId")]
        public ICollection<Booking> Bookings { get; set; }

        public Hotel? Hotel { get; set; }

        public int? HotelId { get; set; }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? RoomId { get; set; }

        public RoomType? RoomType { get; set; }

        public int? RoomTypeId { get; set; }
    }
}