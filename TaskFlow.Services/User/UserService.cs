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
            var res = await _userRepository.GetUserByUsername(username);
            return new UserInfoResponseDto
            {
                Id = res.Id,
                GuidId = res.UserGuidId,
                Username = res.UserName,
                Email = res.UserEmail,
                Role = res.UserRole,
                CreatedDate = res.CreatedDate,
            };
        }

        public async Task<UserInfoResponseDto> GetUserById(string GuidId)
        {
            var res = await _userRepository.GetUserById(GuidId);
            return new UserInfoResponseDto
            {
                Id = res.Id,
                GuidId = res.UserGuidId,
                Username = res.UserName,
                Email = res.UserEmail,
                Role = res.UserRole,
                CreatedDate = res.CreatedDate,
            };
        }

        public async Task<UserInfoResponseDto> CreateUser(UserAddRequestDto user)
        {
            var addUserObj = new UserEntity
            {
                UserGuidId = Guid.NewGuid().ToString(),
                UserName = user.Username,
                UserPasswordHash = PasswordHelper.HashPassword(user.Password),
                UserEmail = user.Email,
                UserRole = user.Role,
                CreatedDate = DateTime.Now,
            };

            var res = await _userRepository.CreateUser(addUserObj);
            return new UserInfoResponseDto
            {
                Id = res.Id,
                GuidId = res.UserGuidId,
                Username = res.UserName,
                Email = res.UserEmail,
                Role = res.UserRole,
                CreatedDate = res.CreatedDate,
            };
        }

        public async Task<UserInfoResponseDto> UpdateUser(UserUpdateRequestDto user)
        {
            var res = await _userRepository.UpdateUser(user);
            return new UserInfoResponseDto
            {
                Id = res.Id,
                GuidId = res.UserGuidId,
                Username = res.UserName,
                Email = res.UserEmail,
                Role = res.UserRole,
                CreatedDate = res.CreatedDate,
            };
        }

        // public async Task DeleteUser(int id)
        // {
        //     await _userRepository.DeleteUser(id);
        // }
    }
}