using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RailwayReservation.Interfaces;
using RailwayReservation.Models;
using RailwayReservation.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RailwayReservation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassController : ControllerBase
    {
        private readonly IClass classRepo;

        public ClassController(IClass _classRepo)
        {
            classRepo = _classRepo;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            var classes = await classRepo.GetAll();
            if (!classes.Any())
            {
                return NotFound();
            }
            return Ok(classes);
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string className)
        {
            var result = await classRepo.SearchByClassName(className);
            if (!result.Any())
            {
                return NotFound();
            }
            return Ok(result);
        }

        // Filter classes by class type
        [HttpGet("filter")]
        public async Task<IActionResult> FilterByClassType([FromQuery] string classType)
        {
            var result = await classRepo.GetByClassType(classType);
            if (!result.Any())
            {
                return NotFound();
            }
            return Ok(result);
        }

    }
}
