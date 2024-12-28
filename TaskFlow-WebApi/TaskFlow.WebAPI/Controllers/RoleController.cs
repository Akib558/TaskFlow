using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskFlow.Core.DTOs;
using TaskFlow.Services.Role;

namespace TaskFlow.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpPost("AddRole")]
        public async Task<IActionResult> AddRole(RoleAddRequestDto roleAddRequestDto)
        {
            var res = await _roleService.AddRole(roleAddRequestDto);
            return Ok(res);
        }

        [HttpPut("UpdateRole")]
        public async Task<IActionResult> RoleUpdate(RoleUpdateRequestDto roleUpdateRequestDto)
        {
            var res = await _roleService.UpdateRole(roleUpdateRequestDto);
            return Ok(res);
        }

        [HttpGet("GetAllRole")]
        public async Task<IActionResult> GetAllRole()
        {
            var res = await _roleService.GetAllRole();
            return Ok(res);
        }

        [HttpPost("RoleAddOperation")]
        public async Task<IActionResult> RoleAddOperation(
            RoleAddOperationRequestDto roleAddOperationRequestDto
        )
        {
            var res = await _roleService.RoleAddOperation(roleAddOperationRequestDto);
            return Ok(res);
        }

        [HttpGet("GetAllOperation")]
        public async Task<IActionResult> GetAllOperation()
        {
            var res = await _roleService.GetAllProjectOperation();
            return Ok(res);
        }

        [HttpPost("AddAllProjectOperation")]
        public async Task<IActionResult> AddAllProjectOperation()
        {
            var res = await _roleService.AddAllProjectOperation();
            return Ok(res);
        }
    }
}
