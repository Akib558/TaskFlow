using TaskFlow.Core.Records;

namespace TaskFlow.Repositories.User
{
    public interface IUserRepository
    {
        Task<UserRecord> GetUserByUserId(int userId);
        Task<bool> UpdateUser(UserRecord userRecord);
        Task<bool> DeleteUser(int userId);
    }
}