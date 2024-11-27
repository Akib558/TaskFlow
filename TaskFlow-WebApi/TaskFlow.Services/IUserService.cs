using TaskFlow.Core.DTOs;
using TaskFlow.Data.Entities;

namespace TaskFlow.Services
{
    public interface IUserService
    {
        Task<UserInfoResponseDto> GetUserByUsername(string username);
        Task<UserInfoResponseDto> GetUserById(string GuidId);
        Task<UserEntity> CreateUser(UserAddRequestDto user);
        Task<UserInfoResponseDto> UpdateUser(UserUpdateRequestDto user);
        Task DeleteUser(int id);

    }
}