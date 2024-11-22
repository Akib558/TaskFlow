using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskFlow.Core.DTOs;
using TaskFlow.Data.Entities;

namespace TaskFlow.Services
{
    public interface IUserService
    {
        Task<UserEntity> GetUserByUsername(string username);
        Task<UserInfoResponseDto> GetUserById(string GuidId);
        Task<UserEntity> CreateUser(UserAddRequestDto user);
        Task<UserEntity> UpdateUser(UserEntity user);
        Task DeleteUser(int id);

    }
}