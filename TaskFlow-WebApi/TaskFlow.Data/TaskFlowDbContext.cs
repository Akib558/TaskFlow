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
            .HasPrincipalKey(p => p.UserGuidId);
        modelBuilder.Entity<TaskAssignmentsEntity>()
            .HasOne(ta => ta.TaskEntity)
            .WithMany(u => u.TaskAssignments)
            .HasForeignKey(f => f.TaskGuidId)
            .HasPrincipalKey(p => p.TaskGuidId);


        //Many-to-Many relationship between user and taskupdate

        modelBuilder.Entity<TaskUpdate>()
            .HasKey(ind => new { ind.UserGuidId, ind.TaskGuidId });
        modelBuilder.Entity<TaskUpdate>()
            .HasOne(tu => tu.UserEntity)
            .WithMany(ue => ue.TaskUpdates)
            .HasForeignKey(tu => tu.UserGuidId)
            .HasPrincipalKey(tu => tu.UserGuidId);
        modelBuilder.Entity<TaskUpdate>()
            .HasOne(tu => tu.TaskEntity)
            .WithMany(te => te.TaskUpdates)
            .HasForeignKey(tu => tu.TaskGuidId)
            .HasPrincipalKey(tu => tu.TaskGuidId);


        //Many-to-Many relationship between user and taskupdate

        modelBuilder.Entity<ProjectMembers>()
            .HasKey(ind => new { ind.UserGuidId, ind.ProjectGuidId });
        modelBuilder.Entity<ProjectMembers>()
            .HasOne(ta => ta.UserEntity)
            .WithMany(u => u.ProjectMembers)
            .HasForeignKey(f => f.UserGuidId)
            .HasPrincipalKey(f => f.UserGuidId);
        modelBuilder.Entity<ProjectMembers>()
            .HasOne(ta => ta.ProjectEntity)
            .WithMany(u => u.Members)
            .HasForeignKey(f => f.ProjectGuidId)
            .HasPrincipalKey(t => t.ProjectGuidId);

    }
}