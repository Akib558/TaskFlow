select Id,
       UserName         as Username,
       UserPasswordHash as Password,
       UserEmail        as Email,
       0                as Role
from Users
where UserEmail = @UserEmail;
--   and UserPasswordHash = @UserPasswordHash;

