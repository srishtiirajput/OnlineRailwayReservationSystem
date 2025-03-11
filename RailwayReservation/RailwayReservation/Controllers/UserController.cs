using Microsoft.AspNetCore.Mvc;
using RailwayReservation.Interfaces;
using RailwayReservation.Models;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RailwayReservation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUsers _userRepository;
        public UserController(IUsers userRepository)
        {
            _userRepository = userRepository;
        }

        // Get all users
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userRepository.GetAllUsersAsync();
            return Ok(users);
        }


        //Get a user by ID
        //[HttpGet("{userId}")]
        //public async Task<IActionResult> GetUserById(string userId)
        //{
        //    var user = await _userRepository.GetUserByIdAsync(userId);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(user);
        //}




        //Update a user
        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateUser([FromBody] UserVM user)
        {

            var updatedUser = await _userRepository.UpdateUserAsync(user);
            if (!updatedUser)
            {
                return NotFound();
            }

            return Ok();
        }

        //Delete a user
        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            var deleted = await _userRepository.DeleteUserAsync(userId);
            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
        
    }
}
