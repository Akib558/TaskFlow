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
            return await _context.Set<UserEntity>().FirstOrDefaultAsync(u => u.Username == username);
        }


        public async Task<UserInfoResponseDto> GetUserById(int id)
        {
            var res = await _context.Set<UserEntity>().FindAsync(id);

            return new UserInfoResponseDto
            {
                Id = res.Id,
                Username = res.Username,
                PasswordHash = res.PasswordHash,
                Role = res.Role,
                CreatedDate = res.CreatedDate
            };
        }

        public async Task<UserEntity> CreateUser(UserAddModifiedRequestDto user)
        {
            var userAddObject = new UserEntity
            {
                GuidId = user.GuidId,
                Username = user.Username,
                PasswordHash = user.Password,
                Email = user.Email,
                Role = user.Role,
                CreatedDate = user.CreatedDate
            };
            var entity = await _context.Set<UserEntity>().AddAsync(userAddObject);
            await _context.SaveChangesAsync();
            return entity.Entity;
        }

        public async Task<UserEntity> UpdateUser(UserEntity user)
        {
            var entity = _context.Set<UserEntity>().Update(user);
            await _context.SaveChangesAsync();
            return entity.Entity;
        }

        public async Task DeleteUser(int id)
        {
            var user = await GetUserById(id);
            _context.Set<UserEntity>().Remove(new UserEntity { Id = id });
            await _context.SaveChangesAsync();
        }


    }
}