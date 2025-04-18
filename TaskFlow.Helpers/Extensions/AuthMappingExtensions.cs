using TaskFlow.Core.Entities;
using TaskFlow.Core.Records;

namespace TaskFlow.Helpers.Extensions;

public static class AuthMappingExtensions
{
    public static RegisterUserRecord ToRegisterUserRecord(this RegisterUserEntity user)
    {
        return new RegisterUserRecord(
            user.Id,
            user.Username,
            user.Password,
            user.Email,
            user.Role
        );
    }

    public static RegisterUserEntity ToRegisterUserEntity(this RegisterUserRecord user)
    {
        return new RegisterUserEntity
        {
            Id = user.Id,
            Username = user.Username,
            Password = user.Password,
            Email = user.Email,
            Role = user.Role
        };
    }


    public static LoginUserEntity ToLoginUserEntity(this LoginUserRecord dto)
    {
        return new LoginUserEntity
        {
            UserEmail = dto.Email,
            UserPasswordHash = dto.Password
        };
    }

    public static LoginUserRecord ToUserLoginAuthRequestDto(this LoginUserEntity entity)
    {
        return new LoginUserRecord(
            Email: entity.UserEmail,
            Password: entity.UserPasswordHash
        );
    }
}