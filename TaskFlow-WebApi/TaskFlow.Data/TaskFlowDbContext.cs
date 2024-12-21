using Microsoft.EntityFrameworkCore;
using TaskFlow.Data.Entities;
using static TaskFlow.Data.Entities.JwtEntity;

namespace TaskFlow.Data;

public class TaskFlowDbContext : DbContext
{
    public TaskFlowDbContext(DbContextOptions<TaskFlowDbContext> options)
        : base(options) { }

    public DbSet<UserEntity> Users { get; set; }
    public DbSet<TaskEntity> Tasks { get; set; }
    public DbSet<TaskUpdate> TasksUpdate { get; set; }
    public DbSet<TaskAssignmentsEntity> TaskAssignments { get; set; }
    public DbSet<ProjectEntity> Projects { get; set; }
    public DbSet<ProjectRolesEntity> ProjectRoles { get; set; }
    public DbSet<ProjectMembers> ProjectMembers { get; set; }
    public DbSet<JwtRefreshTokenEntity> JwtRefreshTokens { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserEntity>().HasIndex(ind => ind.UserEmail).IsUnique();
        modelBuilder.Entity<UserEntity>().HasIndex(ind => ind.UserName).IsUnique();

        //Many-to-Many relationship between user and task

        modelBuilder
            .Entity<TaskAssignmentsEntity>()
            .HasKey(ind => new { ind.UserGuidId, ind.TaskGuidId });
        modelBuilder
            .Entity<TaskAssignmentsEntity>()
            .HasOne(ta => ta.UserEntity)
            .WithMany(u => u.TaskAssignments)
            .HasForeignKey(f => f.UserGuidId)
            .HasPrincipalKey(f => f.UserGuidId);
        modelBuilder
            .Entity<TaskAssignmentsEntity>()
            .HasOne(ta => ta.TaskEntity)
            .WithMany(u => u.TaskAssignments)
            .HasForeignKey(f => f.TaskGuidId)
            .HasPrincipalKey(t => t.TaskGuidId);

        //Many-to-Many relationship between user and taskupdate

        modelBuilder.Entity<TaskUpdate>().HasKey(ind => new { ind.UserGuidId, ind.TaskGuidId });
        modelBuilder
            .Entity<TaskUpdate>()
            .HasOne(ta => ta.UserEntity)
            .WithMany(u => u.TaskUpdates)
            .HasForeignKey(f => f.UserGuidId)
            .HasPrincipalKey(f => f.UserGuidId);
        modelBuilder
            .Entity<TaskUpdate>()
            .HasOne(ta => ta.TaskEntity)
            .WithMany(u => u.TaskUpdates)
            .HasForeignKey(f => f.TaskGuidId)
            .HasPrincipalKey(t => t.TaskGuidId);

        //Many-to-Many relationship between user and taskupdate

        modelBuilder
            .Entity<ProjectMembers>()
            .HasKey(ind => new { ind.UserGuidId, ind.ProjectGuidId });
        modelBuilder
            .Entity<ProjectMembers>()
            .HasOne(ta => ta.UserEntity)
            .WithMany(u => u.Members)
            .HasForeignKey(f => f.UserGuidId)
            .HasPrincipalKey(f => f.UserGuidId);
        modelBuilder
            .Entity<ProjectMembers>()
            .HasOne(ta => ta.ProjectEntity)
            .WithMany(u => u.Members)
            .HasForeignKey(f => f.ProjectGuidId)
            .HasPrincipalKey(t => t.ProjectGuidId);
    }
}
