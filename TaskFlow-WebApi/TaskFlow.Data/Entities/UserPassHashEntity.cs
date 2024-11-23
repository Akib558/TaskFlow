using System;

namespace TaskFlow.Data.Entities;

public class UserPassHashEntity
{
    public string Username { get; set; }
    public string PasswordHash { get; set; }
}
