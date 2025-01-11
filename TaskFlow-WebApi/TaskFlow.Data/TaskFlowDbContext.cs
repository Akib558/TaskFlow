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

    // public DbSet<ProjectPrivileges> ProjectPrivileges { get; set; }
    // public DbSet<ProjectOperations> ProjectOperations { get; set; }

    public DbSet<ProjectAndRoles> ProjectAndRoles { get; set; }
    public DbSet<ProjectMembersAndRoles> ProjectMembersAndRoles { get; set; }
    public DbSet<RolePathEntity> RolePaths { get; set; }
    public DbSet<PathEntity> Paths { get; set; }

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

        //Many-toMany relationship between ProjectRolesEntity & ProjectOperations

        // modelBuilder
        //     .Entity<ProjectPrivileges>()
        //     .HasKey(ind => new { ind.ProjectRoleGuidId, ind.ProjectOperationsGuidId });

        // modelBuilder
        //     .Entity<ProjectPrivileges>()
        //     .HasOne(pp => pp.ProjectRole)
        //     .WithMany(pr => pr.ProjectPrivileges)
        //     .HasForeignKey(pp => pp.ProjectRoleGuidId)
        //     .HasPrincipalKey(pp => pp.ProjectRoleGuidId);

        // modelBuilder
        //     .Entity<ProjectPrivileges>()
        //     .HasOne(pp => pp.ProjectOperation)
        //     .WithMany(pr => pr.ProjectPrivileges)
        //     .HasForeignKey(pp => pp.ProjectOperationsGuidId)
        //     .HasPrincipalKey(pp => pp.ProjectOperationsGuidId);

        modelBuilder
            .Entity<RolePathEntity>()
            .HasKey(ind => new { ind.ProjectRoleGuidId, ind.PathGuidId });
        modelBuilder
            .Entity<RolePathEntity>()
            .HasOne(pp => pp.ProjectRoles)
            .WithMany(pr => pr.ProjectRoleWiseAccesses)
            .HasForeignKey(pr => pr.ProjectRoleGuidId)
            .HasPrincipalKey(pp => pp.ProjectRoleGuidId);
        modelBuilder
            .Entity<RolePathEntity>()
            .HasOne(pp => pp.Paths)
            .WithMany(p => p.ProjectRoleWiseAccesses)
            .HasForeignKey(pp => pp.PathGuidId)
            .HasPrincipalKey(pp => pp.PathGuidId);

        modelBuilder
            .Entity<ProjectAndRoles>()
            .HasKey(ind => new { ind.ProjectGuidId, ind.ProjectRoleGuidId });
        modelBuilder
            .Entity<ProjectAndRoles>()
            .HasOne(pp => pp.Projects)
            .WithMany(pr => pr.ProjectAndRoles)
            .HasForeignKey(pr => pr.ProjectGuidId)
            .HasPrincipalKey(pp => pp.ProjectGuidId);
        modelBuilder
            .Entity<ProjectAndRoles>()
            .HasOne(pp => pp.ProjectRoles)
            .WithMany(p => p.ProjectAndRoles)
            .HasForeignKey(pp => pp.ProjectRoleGuidId)
            .HasPrincipalKey(pp => pp.ProjectRoleGuidId);

        modelBuilder
            .Entity<ProjectMembersAndRoles>()
            .HasKey(ind => new { ind.ProjectMemeberGuidId, ind.ProjectRoleGuidId });
        modelBuilder
            .Entity<ProjectMembersAndRoles>()
            .HasOne(pp => pp.ProjectMembers)
            .WithMany(pr => pr.ProjectMembersAndRoles)
            .HasForeignKey(pr => pr.ProjectMemeberGuidId)
            .HasPrincipalKey(pp => pp.ProjectMemberGuidId);
        modelBuilder
            .Entity<ProjectMembersAndRoles>()
            .HasOne(pp => pp.ProjectRoles)
            .WithMany(p => p.ProjectMembersAndRoles)
            .HasForeignKey(pp => pp.ProjectRoleGuidId)
            .HasPrincipalKey(pp => pp.ProjectRoleGuidId);
    }
}
