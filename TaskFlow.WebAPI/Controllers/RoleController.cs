using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using TaskFlow.Core.DTOs;
using TaskFlow.Core.Entities;
using TaskFlow.Services.Role;

namespace TaskFlow.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;
        private readonly IActionDescriptorCollectionProvider _actionDescriptorCollectionProvider;

        public RoleController(
            IRoleService roleService,
            IActionDescriptorCollectionProvider actionDescriptorCollectionProvider
        )
        {
            _roleService = roleService;
            _actionDescriptorCollectionProvider = actionDescriptorCollectionProvider;
        }

        [Authorize]
        [HttpGet("GetPermissionList")]
        public async Task<List<PathEntity>> GetPermissionList()
        {
            return await _roleService.GetPermissionList();
        }

        // [HttpPost("AddRole")]
        // public async Task<IActionResult> AddRole(RoleAddRequestDto roleAddRequestDto)
        // {
        //     var res = await _roleService.AddRole(roleAddRequestDto);
        //     return Ok(res);
        // }

        // [HttpPut("UpdateRole")]
        // public async Task<IActionResult> RoleUpdate(RoleUpdateRequestDto roleUpdateRequestDto)
        // {
        //     var res = await _roleService.UpdateRole(roleUpdateRequestDto);
        //     return Ok(res);
        // }

        // [HttpGet("GetAllRole")]
        // public async Task<IActionResult> GetAllRole()
        // {
        //     var res = await _roleService.GetAllRole();
        //     return Ok(res);
        // }

        // [HttpPost("RoleAddOperation")]
        // public async Task<IActionResult> RoleAddOperation(
        //     RoleAddOperationRequestDto roleAddOperationRequestDto
        // )
        // {
        //     var res = await _roleService.RoleAddOperation(roleAddOperationRequestDto);
        //     return Ok(res);
        // }

        // [HttpGet("GetAllOperation")]
        // public async Task<IActionResult> GetAllOperation()
        // {
        //     var res = await _roleService.GetAllProjectOperation();
        //     return Ok(res);
        // }

        // [HttpPost("AddAllProjectOperation")]
        // public async Task<IActionResult> AddAllProjectOperation()
        // {
        //     var res = await _roleService.AddAllProjectOperation();
        //     return Ok(res);
        // }

        // [HttpGet("routes")]
        // public IActionResult GetAvailableApiPaths()
        // {
        //     var routes = _actionDescriptorCollectionProvider
        //         .ActionDescriptors.Items.Where(ad => ad is ControllerActionDescriptor)
        //         .Cast<ControllerActionDescriptor>()
        //         .Select(ad => new
        //         {
        //             Path = $"/{ad.AttributeRouteInfo?.Template}",
        //             HttpMethods = ad.ActionConstraints?.OfType<HttpMethodActionConstraint>()
        //                 .FirstOrDefault()
        //                 ?.HttpMethods ?? new List<string>(),
        //         })
        //         .ToList();
        //
        //     return Ok(routes);
        // }

        [HttpPost("AddPathToRole")]
        public async Task<IActionResult> AddPathToRole(
            AddPathToRoleRequestDto addPathToRoleRequestDto
        )
        {
            var res = await _roleService.AddPathToRole(addPathToRoleRequestDto);
            return Ok(res);
        }

        [HttpPost("AddPath")]
        public async Task<IActionResult> AddPath(PathAddRequestDto pathAddRequestDto)
        {
            var res = await _roleService.AddPath(pathAddRequestDto);
            return Ok(res);
        }

        [HttpGet("GetAllPath")]
        public async Task<IActionResult> GetAllPath()
        {
            var res = await _roleService.GetAllPath();
            return Ok(res);
        }

        [HttpPost("GetAllowrdPathForRole")]
        public async Task<IActionResult> GetAllowrdPathForRole(
            GetAllowedPathForRoleRequestDto getAllowedPathForRoleRequestDto)
        {
            var res = await _roleService.GetAllowedPathForRole(getAllowedPathForRoleRequestDto);
            return Ok(res);
        }
    }
}