using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TaskFlow.Core.DTOs;
using TaskFlow.Data.Entities;
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


        [Authorize]
        [HttpGet("GetUserByName")]
        public async Task<IActionResult> GetUserByUsername(
            UserGetByUsernameRequestDto userGetByUsernameRequestDto
        )
        {
            var user = await _userService.GetUserByUsername(userGetByUsernameRequestDto.Username);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
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
                return NotFound();
            }

            return Ok(user);
        }

        // [HttpPost("AddUser")]
        // public async Task<IActionResult> CreateUser([FromBody] UserAddRequestDto user)
        // {
        //     var createdUser = await _userService.CreateUser(user);
        //     return CreatedAtAction(nameof(GetUserById), new { id = createdUser.Id }, createdUser);
        // }

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