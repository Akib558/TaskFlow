using Dapper;
using TaskFlow.Core.Records;
using TaskFlow.Data;

namespace TaskFlow.Repositories.User
{
    public class UserRepository : IUserRepository
    {
        private readonly TaskFlowDbContext _dbContext;

        public UserRepository(TaskFlowDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<UserRecord> GetUserByUserId(int userId)
        {
            using var connection = _dbContext.CreateConnection();
            var query = QueryCollection.LoadQuery("Task", "GetTaskById");

            try
            {
                var res = await connection.QueryFirstOrDefaultAsync<UserRecord>(query, new
                {
                    Id = userId
                });
                return res;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }


        public async Task<bool> UpdateUser(UserRecord userRecord)
        {
            using var connection = _dbContext.CreateConnection();
            var query = QueryCollection.LoadQuery("Task", "UpdateUser");

            try
            {
                await connection.ExecuteAsync(query, userRecord);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<bool> DeleteUser(int userId)
        {
            using var connection = _dbContext.CreateConnection();
            var query = QueryCollection.LoadQuery("Task", "DeleteUser");

            try
            {
                await connection.ExecuteAsync(query, new
                {
                    Id = userId
                });
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}