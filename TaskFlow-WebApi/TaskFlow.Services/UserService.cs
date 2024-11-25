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

        public async Task<UserInfoResponseDto> GetUserByUsername(string username)
        {
            return await _userRepository.GetUserByUsername(username);
        }

        public async Task<UserInfoResponseDto> GetUserById(string GuidId)
        {
            return await _userRepository.GetUserById(GuidId);
        }

        public async Task<UserEntity> CreateUser(UserAddRequestDto user)
        {
            var addUserObj = new UserEntity
            {
                GuidId = Guid.NewGuid().ToString(),
                Username = user.Username,
                PasswordHash = PasswordHelper.HashPassword(user.Password),
                Email = user.Email,
                Role = user.Role,
                CreatedDate = DateTime.Now
            };

            return await _userRepository.CreateUser(addUserObj);
        }

        public async Task<UserInfoResponseDto> UpdateUser(UserUpdateRequestDto user)
        {
            return await _userRepository.UpdateUser(user);
        }

        public async Task DeleteUser(int id)
        {
            await _userRepository.DeleteUser(id);
        }

    }
}