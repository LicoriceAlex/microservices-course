using Domain.Entities;
using Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Services.Contracts.Repositories;

namespace Infrastructure.Repositories;

/// <summary>
/// Репозиторий пользователей на ef core
/// </summary>
public class UserRepository : IUserRepository
{
    private readonly ActivationDbContext _db;

    public UserRepository(ActivationDbContext db)
    {
        _db = db;
    }
    
    /// <inheritdoc />
    public async Task<Guid> CreateAsync(User user)
    {
        var userEntity = new UserEntity
        {
            Id = user.Id,
            Email = user.Email,
            Name = user.Name,
            IsActive = user.IsActive,
            CreatedAt = user.CreatedAt
        };
        _db.Users.Add(userEntity);
        await _db.SaveChangesAsync();
        return userEntity.Id;
    }

    /// <inheritdoc />
    public async Task<User?> GetAsync(Guid id)
    {
        var userEntity = await _db.Users.FindAsync(id);
        if (userEntity is null) return null;

        return new User
        {
            Id = userEntity.Id,
            Email = userEntity.Email,
            Name = userEntity.Name,
            IsActive = userEntity.IsActive,
            CreatedAt = userEntity.CreatedAt
        };
    }

    /// <inheritdoc />
    public async Task<List<User>> GetAllAsync()
    {
        var list = await _db.Users.AsNoTracking().OrderBy(x => x.CreatedAt).ToListAsync();
        var result = new List<User>(list.Count);
        foreach (var e in list)
        {
            result.Add(new User
            {
                Id = e.Id,
                Email = e.Email,
                Name = e.Name,
                IsActive = e.IsActive,
                CreatedAt = e.CreatedAt
            });
        }
        return result;
    }
}