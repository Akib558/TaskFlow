using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace TaskFlow.Data;

public class TaskFlowDbContext
{
    private readonly string _connectionString;

    public TaskFlowDbContext(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection") ?? "";
    }

    public IDbConnection CreateConnection()
    {
        return new SqlConnection(_connectionString);
    }
}