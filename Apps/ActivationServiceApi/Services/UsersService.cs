using Domain.Entities;
using Services.Contracts.Dtos.Users;
using Services.Contracts.Repositories;
using Services.Interfaces;

namespace Services;

/// <summary>
/// Реализация сервиса пользователей
/// </summary>
public class UsersService : IUsersService
{
    private readonly IUserRepository _userRepository;

    public UsersService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    /// <inheritdoc />
    public async Task<Guid> CreateAsync(UserCreateRequest dto)
    {
        var entity = new User
        {
            Email = dto.Email,
            Name = dto.Name,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };
        return await _userRepository.CreateAsync(entity);
    }

    /// <inheritdoc />
    public async Task<UserResponse?> GetAsync(Guid id)
    {
        var user = await _userRepository.GetAsync(id);
        if (user is null)
        {
            return null;
        }

        return new UserResponse
        {
            Id = user.Id,
            Email = user.Email,
            Name = user.Name,
            IsActive = user.IsActive,
            CreatedAt = user.CreatedAt
        };
    }

    /// <inheritdoc />
    public async Task<List<UserResponse>> GetAllAsync()
    {
        var list = await _userRepository.GetAllAsync();
        var result = new List<UserResponse>(list.Count);
        foreach (var user in list)
        {
            result.Add(new UserResponse
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.Name,
                IsActive = user.IsActive,
                CreatedAt = user.CreatedAt
            });
        }
        return result;
    }
}