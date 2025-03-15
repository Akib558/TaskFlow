using System.ComponentModel.DataAnnotations;

namespace TaskFlow.Core.DTOs
{
    public class UserRequestByIdDto
    {
        public int Id { get; set; }
    }

    public class UserGetByUsernameRequestDto
    {
        [Required] public string Username { get; set; } = String.Empty;
    }

    public class UserGetByUserIdRequestDto
    {
        [Required] public int UserId { get; set; }
    }

    public class UserUpdateRequestDto
    {
        [Required] public string Username { get; set; } = String.Empty;
        [Required] public string Email { get; set; } = String.Empty;
        [Required] public int Role { get; set; }
        [Required] public string GuidId { get; set; } = String.Empty;
        [Required] public DateTime CreatedDate { get; set; }
    }
}