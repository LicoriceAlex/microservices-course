using Services.Contracts.Dtos.Users;

namespace Services.Interfaces;

/// <summary>
/// сервис пользователей
/// </summary>
public interface IUsersService
{
    Task<Guid> CreateAsync(UserCreateRequest dto);
    Task<UserResponse?> GetAsync(Guid id);
    Task<List<UserResponse>> GetAllAsync();
}