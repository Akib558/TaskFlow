using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskFlow.Core.DTOs;
using TaskFlow.Core.Exceptions;
using TaskFlow.Services;

namespace TaskFlow.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;

        public UserController(ILogger<UserController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        // Get user by id
        [Authorize]
        [HttpPost("GetUserById")]
        public async Task<IActionResult> GetUserById(
            [FromBody] UserGetByUserIdRequestDto userGetByIdRequestDto
        )
        {
            var user = await _userService.GetUserById(userGetByIdRequestDto.UserId);

            if (user == null)
            {
                throw new NotFoundException("User not found.");
            }

            return Ok(user);
        }


        [Authorize]
        [HttpPost("UpdateUser")]
        public async Task<IActionResult> UpdateUser(UserUpdateRequestDto user)
        {
            var updatedUser = await _userService.UpdateUser(user);
            return Ok(updatedUser);
        }

        // [HttpDelete("DeleteUser")]
        // public async Task<IActionResult> DeleteUser(int id)
        // {
        //     await _userService.DeleteUser(id);
        //     return NoContent();
        // }
    }
}