using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WaracleTechTest.Data.Models
{
    public class RoomType
    {
        public int? Capacity { get; set; }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? RoomTypeId { get; set; }

        public string? RoomTypeName { get; set; }
    }
}