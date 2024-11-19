using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskFlow.Data.Entities;

namespace TaskFlow.Services
{
    public interface IUserService
    {
        Task<UserEntity> GetUserByUsername(string username);
        Task<UserEntity> GetUserById(int id);
        Task<UserEntity> CreateUser(UserEntity user);
        Task<UserEntity> UpdateUser(UserEntity user);
        Task DeleteUser(int id);

    }
}