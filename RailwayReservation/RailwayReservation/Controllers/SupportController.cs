using Microsoft.AspNetCore.Mvc;
using RailwayReservation.Interfaces;
using RailwayReservation.Models;
using RailwayReservation.ViewModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RailwayReservation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupportController : ControllerBase
    {
        private readonly ISupport _supportRepository;

        public SupportController(ISupport supportRepository)
        {
            _supportRepository = supportRepository;
        }



        // POST: api/Support
        [HttpPost("create")]
        public async Task<IActionResult> CreateSupport([FromBody] SupportRequest request)
        {
            if (string.IsNullOrEmpty(request.UserQuery) || string.IsNullOrEmpty(request.UserId))
            {
                return BadRequest("Query and UserId cannot be empty.");
            }

            // Call the repository method to create a support ticket
            var support = await _supportRepository.CreateSupportAsync(request.UserQuery, request.UserId);

            return Ok(support); // Return the created support ticket
        }

       

        // GET: api/Support
        [HttpGet("get-all-queries")]
        public async Task<IActionResult> GetAllQueries()
        {
            var supports = await _supportRepository.GetAllQueries();
            return Ok(supports);
        }

        // PUT: api/Support/{supportId}
        [HttpPut("update/{supportId}")]
        public async Task<IActionResult> UpdateSupport(string supportId, [FromBody] UpdateSupportRequest request)
        {
            if (string.IsNullOrEmpty(request.NewQueryText))
            {
                return BadRequest("New query text cannot be empty.");
            }

            // Call the repository method to update the support ticket
            try
            {
                var updatedSupport = await _supportRepository.UpdateSupportAsync(supportId, request.NewQueryText);

                return Ok(updatedSupport); // Return the updated support ticket
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message); // Return error if support ticket not found
            }
        }

        // DELETE: api/Support/{supportId}
        [HttpDelete("{supportId}")]
        public async Task<IActionResult> DeleteSupport(string supportId)
        {
            var deleted = await _supportRepository.DeleteSupportAsync(supportId);
            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }

}
