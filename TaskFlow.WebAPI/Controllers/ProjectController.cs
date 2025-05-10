using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Controllers;
using TaskFlow.Core.DTOs;
using TaskFlow.Services.Project;

namespace TaskFlow.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;
        private readonly IApiDescriptionGroupCollectionProvider _apiExplorer;


        public ProjectController(IProjectService projectService, IApiDescriptionGroupCollectionProvider apiExplorer)
        {
            _projectService = projectService;
            _apiExplorer = apiExplorer;
        }


        [HttpGet("all-routes")]
        public IActionResult GetAllRoutes()
        {
            var routes = _apiExplorer.ApiDescriptionGroups.Items
                .SelectMany(group => group.Items)
                .Select(apiDescription => new
                {
                    Path = apiDescription.RelativePath,
                    Method = apiDescription.HttpMethod,
                    Action = (apiDescription.ActionDescriptor as ControllerActionDescriptor)?.ActionName,
                    Controller = (apiDescription.ActionDescriptor as ControllerActionDescriptor)?.ControllerName
                })
                .ToList();

            return Ok(routes);
        }

        [Authorize]
        [HttpGet("GetProject/{projectId}")]
        public async Task<IActionResult> GetProject(int projectId)
        {
            var res = await _projectService.GetProject(projectId);
            return Ok(res);
        }

        [Authorize]
        [HttpPost("AddProject")]
        public async Task<IActionResult> AddProject(ProjectAddRequestDto projectAddRequestDto)
        {
            var res = await _projectService.AddProject(projectAddRequestDto);
            return Ok(res);
        }

        [Authorize]
        [HttpPut("UpdateProject")]
        public async Task<IActionResult> UpdateProject(
            ProjectUpdateRequestDto projectUpdateRequestDto
        )
        {
            var res = await _projectService.UpdateProject(projectUpdateRequestDto);
            return Ok(res);
        }


        [Authorize]
        [HttpDelete("DeleteProject/{projectId}")]
        public async Task<IActionResult> DeleteProject(int projectId)
        {
            var res = await _projectService.DeleteProject(projectId);
            return Ok(res);
        }


        [Authorize]
        [HttpGet("GetAllProjectForUser")]
        public async Task<IActionResult> GetAllProjectByUser()
        {
            var res = await _projectService.GetAllProjectByUser();
            return Ok(res);
        }

        [Authorize]
        [HttpPost("GetAllMemeberOfProject")]
        public async Task<IActionResult> GetAllProjectMemebers(
            ProjectGetAllMembersRequestDto projectGetAllMembersRequestDto
        )
        {
            var res = await _projectService.GetAllMembers(projectGetAllMembersRequestDto);
            return Ok(res);
        }


        [Authorize]
        [HttpPost("AddMemeberToProject")]
        public async Task<IActionResult> AddMemeberToProject(
            ProjectAddMemberRequestDto projectAddMemberRequestDto
        )
        {
            var res = await _projectService.AddMemberToProject(
                projectAddMemberRequestDto.projectAddMemberListRequestDto);
            return Ok(res);
        }

        [Authorize]
        [HttpPost("UpdateMemberOfProject")]
        public async Task<IActionResult> UpdateMemberOfProject(
            ProjectUpdateMemberRequestDto projectUpdateMemberRequestDto
        )
        {
            var res = await _projectService.UpdateMmeberOfProject(projectUpdateMemberRequestDto);
            return Ok(res);
        }

        //TODO: Delete Member from Project

        [Authorize]
        [HttpPost("GetAllProjetRoles")]
        public async Task<IActionResult> GetAllProjetRoles(
            GetAllProjectRolesRequestDto getAllProjectRolesRequestDto
        )
        {
            var res = await _projectService.GetAllProjetRoles(getAllProjectRolesRequestDto);
            return Ok(res);
        }

        [Authorize]
        [HttpPost("AddRoleToProject")]
        public async Task<IActionResult> AddRoleToProjects(ProjectAndRoleRequestDto projectAndRoles)
        {
            var res = await _projectService.AddRoleToProjects(projectAndRoles);
            return Ok(res != null);
        }

        //TODO: Delete Role To Project

        [Authorize]
        [HttpPost("GetPermissionsForRole")]
        public async Task<IActionResult> GetPermissionsForRole(
            GetPermissionsForRoleDto getPermissionsForRoleDto)
        {
            var res = await _projectService.GetPermissionsForRole(getPermissionsForRoleDto);
            return Ok(res);
        }

        [Authorize]
        [HttpPost("AddPermissionToRole")]
        public async Task<IActionResult> AddPermissionToRole(List<RolePathRequestDto> rolePathRequestDtoList)
        {
            var res = await _projectService.AddPermissionsToRole(rolePathRequestDtoList);
            return Ok(res);
        }

        //TODO: Delete PermissionToRole
        //


        // [Authorize]
        // [HttpPost("AddProjectRolesToMembers")]
        // public async Task<IActionResult> AddProjectRolesToMembers(
        //     ProjectMemberAndRolesRequestDto projectMembersAndRoles
        // )
        // {
        //     var res = await _projectService.AddProjectRolesToMembers(projectMembersAndRoles);
        //     return Ok(res != null);
        // }
    }
}