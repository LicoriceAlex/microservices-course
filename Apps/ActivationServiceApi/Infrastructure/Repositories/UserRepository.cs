using Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Services.Contracts.Models;
using Services.Contracts.Repositories;

namespace Infrastructure.Repositories;

/// <summary>
/// репозиторий пользователей на ef core
/// </summary>
public class UserRepository : IUserRepository
{
    private readonly ActivationDbContext _db;

    public UserRepository(ActivationDbContext db)
    {
        _db = db;
    }

    public async Task<Guid> CreateAsync(UserData user)
    {
        var e = new UserEntity
        {
            Id = user.Id,
            Email = user.Email,
            Name = user.Name,
            IsActive = user.IsActive,
            CreatedAt = user.CreatedAt
        };
        _db.Users.Add(e);
        await _db.SaveChangesAsync();
        return e.Id;
    }

    public async Task<UserData?> GetAsync(Guid id)
    {
        var e = await _db.Users.FindAsync(id);
        if (e is null) return null;
        return new UserData { Id = e.Id, Email = e.Email, Name = e.Name, IsActive = e.IsActive, CreatedAt = e.CreatedAt };
    }

    public async Task<List<UserData>> GetAllAsync()
    {
        var list = await _db.Users.AsNoTracking().OrderBy(x => x.CreatedAt).ToListAsync();
        var result = new List<UserData>(list.Count);
        foreach (var e in list)
        {
            result.Add(new UserData { Id = e.Id, Email = e.Email, Name = e.Name, IsActive = e.IsActive, CreatedAt = e.CreatedAt });
        }
        return result;
    }
}