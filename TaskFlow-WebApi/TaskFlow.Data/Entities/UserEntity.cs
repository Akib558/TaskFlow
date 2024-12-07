using System.ComponentModel.DataAnnotations;

namespace TaskFlow.Data.Entities;

public class UserEntity
{
    [Key]
    public int Id { get; set; }
    public string UserGuidId { get; set; }
    public string UserName { get; set; }
    public string UserEmail { get; set; }
    public string UserPasswordHash { get; set; }
    public string UserRole { get; set; }
    public int UserDeleted { get; set; } = 0;
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public ICollection<TaskAssignmentsEntity> TaskAssignments { get; set; }
    public ICollection<TaskUpdate> TaskUpdates { get; set; }
    public ICollection<ProjectMembers> Members { get; set; }

}
