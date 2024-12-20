using System;
using Microsoft.EntityFrameworkCore;
using TaskFlow.Data;
using TaskFlow.Data.Entities;
using static TaskFlow.Data.Entities.JwtEntity;

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
        var res = await _context.Set<UserEntity>().FirstOrDefaultAsync(u => u.UserEmail == email);
        return res;
    }

    public async Task<JwtRefreshTokenEntity> ValidateRefreshToken(string refreshToken)
    {
        var res = await _context
            .Set<JwtRefreshTokenEntity>()
            .FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
        return res;
    }

    public async Task<JwtRefreshTokenEntity> DeactivateAndAddRefreshToken(
        string refreshToken,
        JwtRefreshTokenEntity jwtRefreshTokenEntitys
    )
    {
        var res = await _context
            .Set<JwtRefreshTokenEntity>()
            .FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);

        res.Status = Core.Enums.RefreshTokenStatusEnum.Used;
        var res2 = await _context.Set<JwtRefreshTokenEntity>().AddAsync(jwtRefreshTokenEntitys);
        await _context.SaveChangesAsync();
        return res2.Entity;
    }
}
