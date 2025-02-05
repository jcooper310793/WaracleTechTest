using System.ComponentModel.DataAnnotations;

namespace WaracleTechTest.Data.Enums
{
    public enum RoomTypeCategory
    {
        [Display(Name = "Single")]
        Single = 1,

        [Display(Name = "Double")]
        Double = 2,

        [Display(Name = "Deluxe")]
        Deluxe = 3
    }
}