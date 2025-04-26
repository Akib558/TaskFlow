namespace TaskFlow.Core.Entities;

public class UserEntity
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public string UserEmail { get; set; }
    public string UserPasswordHash { get; set; }
    public int IsDeleted { get; set; }
    public DateTime CreatedDate { get; set; }
}