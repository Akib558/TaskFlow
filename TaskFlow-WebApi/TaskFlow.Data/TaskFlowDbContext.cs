using Microsoft.EntityFrameworkCore;
using TaskFlow.Data.Entities;

namespace TaskFlow.Data;

public class TaskFlowDbContext : DbContext
{
    public TaskFlowDbContext(DbContextOptions<TaskFlowDbContext> options) : base(options) { }


    public DbSet<UserEntity> Users { get; set; }
    public DbSet<TaskEntity> Tasks { get; set; }
    public DbSet<TaskUpdate> TasksUpdate { get; set; }
    public DbSet<TaskAssignmentsEntity> TaskAssignments { get; set; }
    public DbSet<ProjectEntity> Projects { get; set; }
    public DbSet<ProjectRolesEntity> ProjectRoles { get; set; }
    public DbSet<ProjectMembers> ProjectMembers { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserEntity>()
            .HasIndex(ind => ind.UserName)
            .IsUnique();


        //Many-to-Many relationship between user and task

        modelBuilder.Entity<TaskAssignmentsEntity>()
            .HasKey(ind => new { ind.UserGuidId, ind.TaskGuidId });
        modelBuilder.Entity<TaskAssignmentsEntity>()
            .HasOne(ta => ta.UserEntity)
            .WithMany(u => u.TaskAssignments)
            .HasForeignKey(f => f.UserGuidId)
            .HasPrincipalKey(f => f.UserGuidId);
        modelBuilder.Entity<TaskAssignmentsEntity>()
            .HasOne(ta => ta.TaskEntity)
            .WithMany(u => u.TaskAssignments)
            .HasForeignKey(f => f.TaskGuidId)
            .HasPrincipalKey(t => t.TaskGuidId);

    }
}