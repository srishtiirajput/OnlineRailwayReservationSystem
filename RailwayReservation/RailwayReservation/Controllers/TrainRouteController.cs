using Microsoft.AspNetCore.Mvc;
using RailwayReservation.Interfaces;
using RailwayReservation.Models;
using RailwayReservation.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RailwayReservation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainRouteController : ControllerBase
    {
        private readonly ITrainRoute _routeRepository;

        // Constructor to inject the repository
        public TrainRouteController(ITrainRoute routeRepository)
        {
            _routeRepository = routeRepository;
        }

        // 1. Add a new route
        [HttpPost("add")]
        public IActionResult AddRoute([FromBody] TrainRoute trainRoute)
        {
            if (trainRoute == null)
            {
                return BadRequest("Route data is null.");
            }

            _routeRepository.AddRoute(trainRoute);
            return Ok();
        }

        // 2. Get all routes
        [HttpGet("all")]
        public IActionResult GetAllRoutes()
        {
            var routes = _routeRepository.GetAllRoutes();
            if (routes == null)
            {
                return NotFound("No routes found.");
            }

            return Ok(routes);
        }

        // 3. Get routes by source station
        [HttpGet("source/{source}")]
        public IActionResult GetRoutesBySource(string source)
        {
            var routes = _routeRepository.GetRoutesBySource(source);
            if (routes == null)
            {
                return NotFound($"No routes found from source: {source}");
            }

            return Ok(routes);
        }

        // 4. Get routes by destination station
        [HttpGet("destination/{destination}")]
        public IActionResult GetRoutesByDestination(string destination)
        {
            var routes = _routeRepository.GetRoutesByDestination(destination);
            if (routes == null)
            {
                return NotFound($"No routes found to destination: {destination}");
            }

            return Ok(routes);
        }

        // 5. Get routes between source and destination stations
        [HttpGet("between/{source}/{destination}")]
        public IActionResult GetRoutesBetweenStations(string source, string destination)
        {
            var routes = _routeRepository.GetRoutesBetweenStations(source, destination);
            if (routes == null)
            {
                return NotFound($"No routes found between {source} and {destination}");
            }

            return Ok(routes);
        }

       
    }
}
