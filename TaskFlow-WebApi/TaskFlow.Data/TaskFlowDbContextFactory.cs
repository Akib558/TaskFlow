using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace TaskFlow.Data
{
    public class TaskFlowDbContextFactory : IDesignTimeDbContextFactory<TaskFlowDbContext>
    {
        public TaskFlowDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<TaskFlowDbContext>();
            optionsBuilder.UseSqlServer("Server=localhost,4001;Database=TaskFlow;User ID=sa;Password=@M1janinaok;Trusted_Connection=False;Encrypt=True;TrustServerCertificate=True;");

            return new TaskFlowDbContext(optionsBuilder.Options);
        }
    }
}