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
        private readonly IConfiguration _configuration;
        public TaskFlowDbContextFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public TaskFlowDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<TaskFlowDbContext>();
            optionsBuilder.UseSqlServer(
                _configuration["ConnectionStrings:DefaultConnection"]
            );

            return new TaskFlowDbContext(optionsBuilder.Options);
        }
    }
}