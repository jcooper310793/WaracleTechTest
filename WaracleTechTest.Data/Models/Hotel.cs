using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WaracleTechTest.Data.Models
{
    public class Hotel
    {
        public Hotel()
        {
            Rooms = [];
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? HotelId { get; set; }

        public int? MaxRoomCount { get; set; }

        public string? Name { get; set; }

        [ForeignKey("HotelId")]
        public ICollection<Room> Rooms { get; set; }
    }
}