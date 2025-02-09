using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace TaskFlow.Data
{
    public class TaskFlowDbContextFactory : IDesignTimeDbContextFactory<TaskFlowDbContext>
    {
        public TaskFlowDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<TaskFlowDbContext>();

            // var configuration = new ConfigurationBuilder()
            //     .SetBasePath(AppContext.BaseDirectory)
            //     .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            //     .Build();

            // var connectionString = configuration.GetConnectionString("DefaultConnection");

            // optionsBuilder.UseSqlServer(connectionString);

            // Manually specify the connection string here
            optionsBuilder.UseSqlServer(
                "Server=localhost,10001;Database=TaskFlow;User ID=sa;Password=HelloWorld1!;Trusted_Connection=False;Encrypt=True;TrustServerCertificate=True;"
            );

            return new TaskFlowDbContext(optionsBuilder.Options);
        }
    }
}