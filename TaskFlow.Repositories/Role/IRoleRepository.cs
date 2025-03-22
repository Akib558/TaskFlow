using TaskFlow.Core.Records;

namespace TaskFlow.Repositories.Roles;

public interface IRoleRepository
{
    Task<bool> AddPathToRole(List<PathProjectRoleRecord> pathProjectRoleRecords);
    Task<bool> AddPath(List<PathRecord> pathRecords);
    Task<List<PathRecord>> GetAllPath();
    Task<List<PathRecord>> GetAllowedPathForRole(int projectRoleId, int projectId);
}