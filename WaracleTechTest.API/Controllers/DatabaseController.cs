using Microsoft.AspNetCore.Mvc;
using WaracleTechTest.Business.Interfaces;

namespace WaracleTechTest.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DatabaseController : ControllerBase
    {
        private readonly IDatabaseRepository _databaseRepository;

        public DatabaseController(IDatabaseRepository databaseRepository)
        {
            _databaseRepository = databaseRepository;
        }

        [HttpPost]
        [Route("Reset")]
        public async Task<IActionResult> ResetDatabase()
        {
            try
            {
                await _databaseRepository.ResetDatabaseAsync();

                return StatusCode(StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                var errorMessage = "Error resetting database";
                var exceptionMessage = ex.Message;

                return StatusCode(StatusCodes.Status500InternalServerError, $"{errorMessage}: {exceptionMessage}");
            }
        }

        [HttpPost]
        [Route("Seed")]
        public async Task<IActionResult> SeedDatabase()
        {
            try
            {
                await _databaseRepository.SeedHotelsAsync();
                await _databaseRepository.SeedRoomTypesAsync();

                await _databaseRepository.SeedRoomsAsync(); // rooms dependent on hotels and room types

                return StatusCode(StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                var errorMessage = "Error seeding database";
                var exceptionMessage = ex.Message;

                return StatusCode(StatusCodes.Status500InternalServerError, $"{errorMessage}: {exceptionMessage}");
            }
        }
    }
}