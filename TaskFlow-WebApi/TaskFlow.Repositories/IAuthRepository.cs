using System;
using TaskFlow.Data.Entities;

namespace TaskFlow.Repositories;

public interface IAuthRepository
{
    Task<UserEntity> Register(UserEntity user);
    Task<UserEntity> Login(string email, string password);
}
