using TaskFlow.Core.DTOs;
using TaskFlow.Data.Entities;

namespace TaskFlow.Repositories
{
    public interface IUserRepository
    {
        Task<UserEntity> GetUserByUsername(string username);
        Task<UserInfoResponseDto> GetUserById(int id);
        Task<UserEntity> CreateUser(UserAddModifiedRequestDto user);
        Task<UserEntity> UpdateUser(UserEntity user);
        Task DeleteUser(int id);
    }
}