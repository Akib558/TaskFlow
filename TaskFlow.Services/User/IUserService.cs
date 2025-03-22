using TaskFlow.Core.DTOs;

namespace TaskFlow.Services
{
    public interface IUserService
    {
        // Task<UserInfoResponseDto> GetUserByUsername(string username);
        Task<UserInfoResponseDto> GetUserById(int UserId);

        Task<bool> UpdateUser(UserUpdateRequestDto user);
        // Task DeleteUser(int id);
    }
}