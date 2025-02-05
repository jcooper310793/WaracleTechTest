using Microsoft.AspNetCore.Mvc;
using WaracleTechTest.Business.Interfaces;
using WaracleTechTest.Business.Requests;
using WaracleTechTest.Business.Responses;

namespace WaracleTechTest.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HotelController : ControllerBase
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly IRoomRepository _roomRepository;

        public HotelController(IHotelRepository hotelRepository, IRoomRepository roomRepository)
        {
            _hotelRepository = hotelRepository;
            _roomRepository = roomRepository;
        }

        [HttpPost]
        [Route("AvailableRooms")]
        public async Task<IActionResult> CheckAvailability([FromBody]AvailabilityRequest availabilityRequest)
        {
            var endDate = availabilityRequest.EndDate ?? DateTime.MaxValue;
            var hotelName = availabilityRequest.HotelName;
            var numberOfPeople = availabilityRequest.NumberOfPeople;
            var startDate = availabilityRequest.StartDate ?? DateTime.MinValue;

            try
            {
                var hotel = await _hotelRepository.GetHotelByNameAsync(hotelName);

                if (hotel == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "No hotel found in the database!");
                }

                var rooms = _hotelRepository.GetFreeRooms(hotel, startDate, endDate);

                if (rooms.Count == 0)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "No rooms are free for the provided dates!");
                }

                var availableRooms = rooms
                    .Where(x => _roomRepository.DoesRoomHaveSpaceAvailable(x, numberOfPeople))
                    .ToList();

                if (availableRooms.Count == 0)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "No rooms have enough space for everyone!");
                }

                var roomResponses = availableRooms.Select(x => new RoomResponse(x, false)).ToList();

                return StatusCode(StatusCodes.Status200OK, roomResponses);
            }
            catch (Exception ex)
            {
                var errorMessage = $"Error getting available rooms in {hotelName} for {numberOfPeople} people between " +
                    $"{startDate.ToShortDateString()} and {endDate.ToShortDateString()}";
                var exceptionMessage = $"{errorMessage}: {ex.Message}";

                return StatusCode(StatusCodes.Status500InternalServerError, $"{errorMessage}: {exceptionMessage}");
            }
        }

        [HttpGet]
        [Route("{hotelName}")]
        public async Task<IActionResult> GetHotelByName([FromRoute] string? hotelName)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(hotelName))
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "No hotel name has been provided!");
                }

                var hotel = await _hotelRepository.GetHotelByNameAsync(hotelName);

                if (hotel == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "No hotels found in the database!");
                }

                var hotelResponse = new HotelResponse(hotel);

                return StatusCode(StatusCodes.Status200OK, hotelResponse);
            }
            catch (Exception ex)
            {
                var errorMessage = $"Error finding hotels with search term {hotelName}";
                var exceptionMessage = ex.Message;

                return StatusCode(StatusCodes.Status500InternalServerError, $"{errorMessage}: {exceptionMessage}");
            }
        }
    }
}