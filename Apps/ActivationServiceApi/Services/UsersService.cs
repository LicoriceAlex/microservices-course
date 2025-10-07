using Services.Contracts.Dtos.Users;
using Services.Contracts.Models;
using Services.Contracts.Repositories;
using Services.Interfaces;

namespace Services;

/// <summary>
/// реализация сервиса пользователей
/// </summary>
public class UsersService : IUsersService
{
    private readonly IUserRepository _users;

    public UsersService(IUserRepository users)
    {
        _users = users;
    }

    public async Task<Guid> CreateAsync(UserCreateRequest dto)
    {
        var data = new UserData
        {
            Id = Guid.NewGuid(),
            Email = dto.Email,
            Name = dto.Name,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };
        return await _users.CreateAsync(data);
    }

    public async Task<UserResponse?> GetAsync(Guid id)
    {
        var u = await _users.GetAsync(id);
        if (u is null) return null;

        return new UserResponse
        {
            Id = u.Id,
            Email = u.Email,
            Name = u.Name,
            IsActive = u.IsActive,
            CreatedAt = u.CreatedAt
        };
    }

    public async Task<List<UserResponse>> GetAllAsync()
    {
        var list = await _users.GetAllAsync();
        var result = new List<UserResponse>(list.Count);
        foreach (var u in list)
        {
            result.Add(new UserResponse
            {
                Id = u.Id,
                Email = u.Email,
                Name = u.Name,
                IsActive = u.IsActive,
                CreatedAt = u.CreatedAt
            });
        }
        return result;
    }
}