using TaskFlow.Core.DTOs;
using TaskFlow.Data.Entities;

namespace TaskFlow.Repositories
{
    public interface IUserRepository
    {
        Task<UserEntity> GetUserByUsername(string username);
        Task<UserEntity> GetUserById(string GuidId);
        Task<List<string>> GetUserRoles(string GuidId);
        Task<UserEntity> CreateUser(UserEntity user);
        Task<UserEntity> UpdateUser(UserUpdateRequestDto user);
        Task DeleteUser(int id);
    }
}
