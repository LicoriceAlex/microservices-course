using Services.Contracts.Dtos.Users;

namespace Services.Interfaces;

/// <summary>
/// сервис пользователей
/// </summary>
public interface IUsersService
{
    /// <summary>
    /// Создать пользователя
    /// </summary>
    Task<Guid> CreateAsync(UserCreateRequest dto);
    
    /// <summary>
    /// Получить пользователя
    /// </summary>
    Task<UserResponse?> GetAsync(Guid id);
    
    /// <summary>
    /// Получить список всех пользовтелей
    /// </summary>
    /// <returns></returns>
    Task<List<UserResponse>> GetAllAsync();
}