using Microsoft.AspNetCore.Mvc;
using RailwayReservation.Interfaces;
using RailwayReservation.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RailwayReservation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeatController : ControllerBase
    {
        private readonly ISeat seatRepository;

        public SeatController(ISeat _seatRepository)
        {
            seatRepository = _seatRepository;
        }


        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var seats = await seatRepository.GetAllAsync();
            if (!seats.Any())
            {
                return NotFound();
            }
            return Ok(seats);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var seat = await seatRepository.GetByIdAsync(id);
            if (seat == null)
            {
                return NotFound();
            }
            return Ok(seat);
        }
        [HttpGet("coach/{coachId}")]
        public async Task<IActionResult> GetByCoachId(string coachId)
        {
            var seats = await seatRepository.GetByCoachIdAsync(coachId);
            if (!seats.Any())
            {
                return NotFound();
            }
            return Ok(seats);
        }

        [HttpPut("{id}/availability")]
        public async Task<IActionResult> UpdateAvailability(string id, [FromBody] bool availabilityStatus)
        {
            var seat = await seatRepository.GetByIdAsync(id);
            if (seat == null)
            {
                return NotFound();
            }
            await seatRepository.UpdateAvailabilityStatusAsync(id, availabilityStatus);
            return NoContent();
        }
    }
}
