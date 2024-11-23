using System;
using Microsoft.EntityFrameworkCore;
using TaskFlow.Data;
using TaskFlow.Data.Entities;

namespace TaskFlow.Repositories;

public class AuthRepository : IAuthRepository
{
    private TaskFlowDbContext _context;

    public AuthRepository(TaskFlowDbContext dbContext)
    {
        _context = dbContext;
    }

    public async Task<UserEntity> Register(UserEntity user)
    {
        var entity = await _context.Set<UserEntity>().AddAsync(user);
        await _context.SaveChangesAsync();
        return entity.Entity;
    }

    public async Task<UserEntity> Login(string email, string password)
    {
        var res = await _context.Set<UserEntity>().FirstOrDefaultAsync(u => u.Email == email);
        return res;
    }
}

