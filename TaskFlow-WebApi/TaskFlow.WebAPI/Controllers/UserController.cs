using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TaskFlow.Core.DTOs;
using TaskFlow.Data.Entities;
using TaskFlow.Services;

namespace TaskFlow.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;

        public UserController(ILogger<UserController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        // Get user by username
        [HttpGet("GetUserByName/{username}")]
        public async Task<IActionResult> GetUserByUsername(string username)
        {
            var user = await _userService.GetUserByUsername(username);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        // Get user by id
        [HttpGet("GetUserById/{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _userService.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost("AddUser")]
        public async Task<IActionResult> CreateUser([FromBody] UserAddRequestDto user)
        {
            var createdUser = await _userService.CreateUser(user);
            return CreatedAtAction(nameof(GetUserById), new { id = createdUser.Id }, createdUser);
        }

        [HttpPut("UpdateUser")]
        public async Task<IActionResult> UpdateUser([FromBody] UserEntity user)
        {
            var updatedUser = await _userService.UpdateUser(user);
            return Ok(updatedUser);
        }

        [HttpDelete("DeleteUser")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            await _userService.DeleteUser(id);
            return NoContent();
        }
    }

}