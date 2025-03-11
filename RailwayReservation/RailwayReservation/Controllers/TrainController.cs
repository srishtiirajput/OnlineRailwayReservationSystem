using Microsoft.AspNetCore.Mvc;
using RailwayReservation.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RailwayReservation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainController : ControllerBase
    {
        private readonly ITrain trainRepo;

        public TrainController(ITrain _trainRepo)
        {
            trainRepo = _trainRepo;
        }

        [HttpGet("GetAllTrain")]
        public async Task<IActionResult> GetAllTrains()
        {
            var trains = await trainRepo.GetAllTrainsAsync();
            return Ok(trains);
        }


        [HttpGet("{trainId}")]
        public async Task<IActionResult> GetTrainById(string trainId)
        {
            var train = await trainRepo.GetTrainByIdAsync(trainId);
            if (train == null)
            {
                return NotFound("Train not found.");
            }
            return Ok(train);
        }

        [HttpGet("by-name/{trainName}")]
        public async Task<IActionResult> GetTrainByName(string trainName)
        {
            var train = await trainRepo.GetTrainByNameAsync(trainName);
            if (train == null)
            {
                return NotFound($"Train with name '{trainName}' not found.");
            }

            return Ok(train);
        }

        
    }
}
