SELECT Id,
       ProjectName        as Title,
       ProjectDescription as Description,
       ProjectStatus      as Status,
       StartDate          as Created,
       EndDate            as EndDate,
       ModifiedDate       as Modified,
       IsDeleted          as IsDeleted,
       CreatedBy          as CreatedBy
FROM Projects
WHERE CreatedBy = @UserId
  AND IsDeleted = 0;