using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskFlow.Core.DTOs;
using TaskFlow.Data.Entities;
using TaskFlow.Services;
using TaskFlow.Services.Project;

namespace TaskFlow.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [Authorize]
        [HttpPost("AddProject")]
        public async Task<IActionResult> AddProject(ProjectAddRequestDto projectAddRequestDto)
        {
            var res = await _projectService.AddProject(projectAddRequestDto);
            return Ok(res);
        }

        [Authorize]
        [HttpPost("GetProject")]
        public async Task<IActionResult> GetProject(ProjectGetRequestDto projectGetRequestDto)
        {
            var res = await _projectService.GetProject(projectGetRequestDto);
            return Ok(res);
        }

        [Authorize]
        [HttpPost("UpdateProject")]
        public async Task<IActionResult> UpdateProject(
            ProjectUpdateRequestDto projectUpdateRequestDto
        )
        {
            var res = await _projectService.UpdateProject(projectUpdateRequestDto);
            return Ok(res);
        }

        [Authorize]
        [HttpPost("AddMemeberToProject")]
        public async Task<IActionResult> AddMemeberToProject(
            ProjectAddMemberRequestDto projectAddMemberRequestDto
        )
        {
            var res = await _projectService.AddMemeberToProject(projectAddMemberRequestDto);
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
        [HttpPost("AddRoleToProject")]
        public async Task<IActionResult> AddRoleToProjects(ProjectAndRoleRequestDto projectAndRoles)
        {
            var res = await _projectService.AddRoleToProjects(projectAndRoles);
            return Ok(res != null);
        }

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
        [HttpPost("AddProjectRolesToMembers")]
        public async Task<IActionResult> AddProjectRolesToMembers(
            ProjectMemberAndRolesRequestDto projectMembersAndRoles
        )
        {
            var res = await _projectService.AddProjectRolesToMembers(projectMembersAndRoles);
            return Ok(res != null);
        }

        [Authorize]
        [HttpPost("GetAllProjectForUser")]
        public async Task<IActionResult> GetAllProjectByUser(
            GettAllProjectByUser gettAllProjectByUser
        )
        {
            var res = await _projectService.GetAllProjectByUser(gettAllProjectByUser);
            return Ok(res);
        }
    }
}
