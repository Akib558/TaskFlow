-- SELECT pr.Id AS ProjectRoleId,
--        pr.ProjectId,
--        rp.Id AS RolePathId,
--        rp.ProjectRoleId,
--        p.Id  AS PathId,
--        p.PathName,
--        p.PathValue
-- FROM ProjectRoles pr
--          JOIN RolePaths rp ON pr.Id = rp.ProjectRoleId
--          JOIN Paths p ON p.Id = rp.PathId
-- WHERE pr.ProjectId = @ProjectId

SELECT *
FROM ProjectRoles
WHERE ProjectId = @ProjectId;