using Microsoft.EntityFrameworkCore;
using TaskFlow.Data.Entities;

namespace TaskFlow.Data;

public class TaskFlowDbContext : DbContext
{
    public TaskFlowDbContext(DbContextOptions<TaskFlowDbContext> options) : base(options) { }


    public DbSet<UserEntity> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserEntity>()
            .HasIndex(ind => ind.Username)
            .IsUnique();
    }
}