using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskFlow.Core.DTOs
{
	public class UserRequestByIdDto
	{
		public int Id { get; set; }
	}

	public class UserAddRequestDto
	{
		public string Username { get; set; }
		public string Password { get; set; }
		public string Email { get; set; }
		public string Role { get; set; }
	}

	public class UserGetByGuidRequestDto
	{
		public string GuidId { get; set; }
	}

	public class UserGetByUsernameRequestDto
	{
		public string Username { get; set; }
	}

	public class UserUpdateRequestDto
	{
		public string Username { get; set; }
		// public string Password { get; set; }
		public string Email { get; set; }
		public string Role { get; set; }
		public string GuidId { get; set; }
		public DateTime CreatedDate { get; set; }
	}


	public class UserAddModifiedRequestDto : UserAddRequestDto
	{
		public string Role { get; set; }
		public string GuidId { get; set; }
		public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
	}
}