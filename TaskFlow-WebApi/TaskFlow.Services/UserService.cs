using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskFlow.Core.DTOs;
using TaskFlow.Data.Entities;
using TaskFlow.Helpers;
using TaskFlow.Repositories;

namespace TaskFlow.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserEntity> GetUserByUsername(string username)
        {
            return await _userRepository.GetUserByUsername(username);
        }

        public async Task<UserInfoResponseDto> GetUserById(int id)
        {
            return await _userRepository.GetUserById(id);
        }

        public async Task<UserEntity> CreateUser(UserAddRequestDto user)
        {
            var addUserObj = new UserAddModifiedRequestDto
            {
                GuidId = Guid.NewGuid().ToString(),
                Username = user.Username,
                Password = PasswordHelper.HashPassword(user.Password),
                Email = user.Email,
                Role = user.Role,
                CreatedDate = DateTime.Now
            };

            return await _userRepository.CreateUser(addUserObj);
        }

        public async Task<UserEntity> UpdateUser(UserEntity user)
        {
            return await _userRepository.UpdateUser(user);
        }

        public async Task DeleteUser(int id)
        {
            await _userRepository.DeleteUser(id);
        }

    }
}