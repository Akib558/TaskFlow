using System;
using TaskFlow.Core.DTOs;
using TaskFlow.Data.Entities;
using TaskFlow.Repositories.Roles;
using static TaskFlow.Data.Entities.JwtEntity;

namespace TaskFlow.Services.Role;

public class RoleService : IRoleService
{
    private IRoleRepository _roleRepository;

    public RoleService(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    public async Task<RoleAddResponseDto> AddRole(RoleAddRequestDto roleAddRequestDto)
    {
        var res = await _roleRepository.AddRole(
            new ProjectRolesEntity
            {
                ProjectRoleGuidId = Guid.NewGuid().ToString(),
                ProjectRoleName = roleAddRequestDto.RoleName,
            }
        );
        if (res != null)
        {
            return new RoleAddResponseDto
            {
                Id = res.Id,
                ProjectRoleGuidId = res.ProjectRoleGuidId,
                ProjectRoleName = res.ProjectRoleName,
            };
        }
        throw new Exception("Error Occured during adding role");
    }

    public async Task<RoleDeleteResponseDto> DeleteRole(RoleDeleteRequestDto roleDeleteRequestDto)
    {
        return null;
        // await _roleRepository.DeleteRole(roleDeleteRequestDto.ProjectRoleGuidId);
    }

    public async Task<RoleUpdateResponseDto> UpdateRole(RoleUpdateRequestDto roleUpdateRequestDto)
    {
        var res = await _roleRepository.UpdateRole(
            new ProjectRolesEntity { ProjectRoleName = roleUpdateRequestDto.RoleName }
        );
        if (res != null)
        {
            return new RoleUpdateResponseDto
            {
                ProjectRoleGuidId = res.ProjectRoleGuidId,
                ProjectRoleName = res.ProjectRoleName,
            };
        }
        throw new Exception("Error Occured during updating role");
    }

    public async Task<RoleAddOperationResponseDto> RoleAddOperation(
        RoleAddOperationRequestDto roleAddOperationRequestDto
    )
    {
        var roleOperationList = roleAddOperationRequestDto
            .RoleOperation.Select(x => new ProjectPrivileges
            {
                ProjectPrivilegesGuidId = Guid.NewGuid().ToString(),
                ProjectRoleGuidId = x.ProjectRoleGuidId,
                ProjectOperationsGuidId = x.ProjectOperationsGuidId,
            })
            .ToList();
        var res = await _roleRepository.AddOperationsToRole(roleOperationList);

        if (res != null)
        {
            return new RoleAddOperationResponseDto
            {
                RoleOperation = roleOperationList
                    .Select(x => new RoleOperationDto
                    {
                        ProjectRoleGuidId = x.ProjectRoleGuidId,
                        ProjectOperationsGuidId = x.ProjectOperationsGuidId,
                    })
                    .ToList(),
            };
        }
        throw new Exception("Error Occured during updating role");
    }

    public async Task<List<ProjectRolesEntity>> GetAllRole()
    {
        var res = await _roleRepository.GetAllRole();
        if (res != null)
        {
            return res;
        }
        throw new Exception("Error Occured during getting all role");
    }

    public async Task<List<ProjectOperations>> GetAllProjectOperation()
    {
        var res = await _roleRepository.GetAllProjectOperation();
        if (res != null)
        {
            return res;
        }
        throw new Exception("Error Occured during getting all operation for project");
    }

    public async Task<bool> AddAllProjectOperation()
    {
        var res = await _roleRepository.AddAllProjectOperation();
        return res;
    }

    public async Task<bool> AddPathToRole(AddPathToRoleRequestDto addPathToRoleRequestDto)
    {
        var res = await _roleRepository.AddPathToRole(addPathToRoleRequestDto);
        return res;
    }

    public async Task<PathEntity> AddPath(PathAddRequestDto pathAddRequestDto)
    {
        var res = await _roleRepository.AddPath(pathAddRequestDto);
        return res;
    }

    public async Task<List<PathEntity>> GetAllPath()
    {
        var res = await _roleRepository.GetAllPath();
        return res;
    }

    public async Task<GetAllowedPathForRoleDto> GetAllowedPathForRole(string roleGuidId)
    {
        var res = await _roleRepository.GetAllowedPathForRole(roleGuidId);
        return res;
    }
}
