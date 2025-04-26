SELECT u.*,
       pm.Id              as ProjectId,
       p.ProjectName      as ProjectTitle,
       pm.ProjectRoleId   as ProjectRoleId,
       pr.ProjectRoleName as ProjectRoleName
FROM Users u
         Join ProjectMembers pm on u.Id = pm.UserId
         JOIN Projects p on p.Id = pm.ProjectId
         JOIN ProjectRoles pr on pr.Id = pm.ProjectRoleId
Where pm.ProjectId = @ProjectId;