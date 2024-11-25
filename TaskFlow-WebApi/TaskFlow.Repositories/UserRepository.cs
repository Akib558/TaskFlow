using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskFlow.Core.DTOs;
using TaskFlow.Data;
using TaskFlow.Data.Entities;

namespace TaskFlow.Repositories
{
    public class UserRepository : IUserRepository
    {

        private DbContext _context;
        public UserRepository(TaskFlowDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<UserInfoResponseDto> GetUserByUsername(string username)
        {
            var res = await _context.Set<UserEntity>().FirstOrDefaultAsync(u => u.Username == username);

            return new UserInfoResponseDto
            {
                Id = res.Id,
                GuidId = res.GuidId,
                Username = res.Username,
                Email = res.Email,
                Role = res.Role,
                CreatedDate = res.CreatedDate
            };
        }


        public async Task<List<string>> GetUserRoles(string GuidId)
        {
            var user = await _context.Set<UserEntity>().Where(u => u.GuidId == GuidId).Select(u => u.Role).ToListAsync();
            return user;
        }

        public async Task<UserInfoResponseDto> GetUserById(string GuidId)
        {
            var res = await _context.Set<UserEntity>().FirstOrDefaultAsync(u => u.GuidId == GuidId);

            return new UserInfoResponseDto
            {
                Id = res.Id,
                GuidId = res.GuidId,
                Username = res.Username,
                Email = res.Email,
                Role = res.Role,
                CreatedDate = res.CreatedDate
            };
        }

        public async Task<UserEntity> CreateUser(UserEntity user)
        {
            var entity = await _context.Set<UserEntity>().AddAsync(user);
            await _context.SaveChangesAsync();
            return entity.Entity;
        }

        public async Task<UserInfoResponseDto> UpdateUser(UserUpdateRequestDto user)
        {
            var entity = await _context.Set<UserEntity>().FirstOrDefaultAsync(u => u.GuidId == user.GuidId);
            if (entity == null)
            {
                return null;
            }
            entity.Username = user.Username;
            entity.Email = user.Email;
            entity.Role = user.Role;
            // entity.UpdatedDate = DateTime.Now;
            await _context.SaveChangesAsync();
            return new UserInfoResponseDto
            {
                Id = entity.Id,
                GuidId = entity.GuidId,
                Username = entity.Username,
                Role = entity.Role,
                CreatedDate = entity.CreatedDate
            };
        }

        public async Task DeleteUser(int id)
        {
            var user = await GetUserById("");
            _context.Set<UserEntity>().Remove(new UserEntity { Id = id });
            await _context.SaveChangesAsync();
        }


    }
}