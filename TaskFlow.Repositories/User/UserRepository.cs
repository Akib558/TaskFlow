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

        public async Task<UserEntity> GetUserByUsername(string username)
        {
            var res = await _context
                .Set<UserEntity>()
                .FirstOrDefaultAsync(u => u.UserName == username);
            return res;
        }

        public async Task<List<string>> GetUserRoles(string GuidId)
        {
            var user = await _context
                .Set<UserEntity>()
                .Where(u => u.UserGuidId == GuidId)
                .Select(u => u.UserRole)
                .ToListAsync();
            return user;
        }

        public async Task<UserEntity> GetUserById(string GuidId)
        {
            var res = await _context
                .Set<UserEntity>()
                .FirstOrDefaultAsync(u => u.UserGuidId == GuidId);

            return res;
        }

        public async Task<UserEntity> CreateUser(UserEntity user)
        {
            var entity = await _context.Set<UserEntity>().AddAsync(user);
            await _context.SaveChangesAsync();
            return entity.Entity;
        }

        public async Task<UserEntity> UpdateUser(UserUpdateRequestDto user)
        {
            var entity = await _context
                .Set<UserEntity>()
                .FirstOrDefaultAsync(u => u.UserGuidId == user.GuidId);
            if (entity == null)
            {
                return null;
            }
            entity.UserName = user.Username;
            entity.UserEmail = user.Email;
            entity.UserRole = user.Role;
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteUser(int id)
        {
            var user = await GetUserById("");
            _context.Set<UserEntity>().Remove(new UserEntity { Id = id });
            await _context.SaveChangesAsync();
        }
    }
}
