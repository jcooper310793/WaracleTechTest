using Microsoft.AspNetCore.Mvc;
using WaracleTechTest.Business.Interfaces;
using WaracleTechTest.Business.Requests;
using WaracleTechTest.Business.Responses;

namespace WaracleTechTest.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly IBookingRepository _bookingRepository;

        public BookingController(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        [HttpPost]
        public async Task<IActionResult> BookRoom([FromBody] BookingRequest bookingRequest)
        {
            var hotelName = bookingRequest.HotelName;
            var guestName = bookingRequest.GuestName;

            try
            {
                var booking = await _bookingRepository.BookRoomAsync(bookingRequest);

                if (booking == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "Cannot book a room!");
                }

                return StatusCode(StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                var errorMessage = $"Unable to book a room in {hotelName} for {guestName}";
                var exceptionMessage = ex.Message;

                return StatusCode(StatusCodes.Status500InternalServerError, $"{errorMessage}: {exceptionMessage}");
            }
        }

        [HttpGet]
        [Route("{bookingReference}")]
        public async Task<IActionResult> GetBooking([FromRoute] int bookingReference)
        {
            try
            {
                var booking = await _bookingRepository.GetBookingAsync(bookingReference);

                if (booking == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "No booking found in the database!");
                }

                var bookingResponse = new BookingResponse(booking);

                return StatusCode(StatusCodes.Status200OK, bookingResponse);
            }
            catch (Exception ex)
            {
                var errorMessage = $"Error getting booking with reference {bookingReference}";
                var exceptionMessage = ex.Message;

                return StatusCode(StatusCodes.Status500InternalServerError, $"{errorMessage}: {exceptionMessage}");
            }
        }
    }
}