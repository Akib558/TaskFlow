select *
from Users
where UserEmail = @UserEmail
  and UserPasswordHash = @UserPasswordHash;