using TaskFlow.Core.DTOs;
using TaskFlow.Data.Entities;

namespace TaskFlow.Repositories
{
    public interface IUserRepository
    {
        Task<UserInfoResponseDto> GetUserByUsername(string username);
        Task<UserInfoResponseDto> GetUserById(string GuidId);
        Task<List<string>> GetUserRoles(string GuidId);
        Task<UserEntity> CreateUser(UserEntity user);
        Task<UserInfoResponseDto> UpdateUser(UserUpdateRequestDto user);
        Task DeleteUser(int id);
    }
}