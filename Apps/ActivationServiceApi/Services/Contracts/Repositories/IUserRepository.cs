using Domain.Entities;

namespace Services.Contracts.Repositories;

/// <summary>
/// Репозиторий пользователей
/// </summary>
public interface IUserRepository
{
    /// <summary>
    /// Создать пользователя
    /// </summary>
    Task<Guid> CreateAsync(User user);

    /// <summary>
    /// Получить пользователя
    /// </summary>
    Task<User?> GetAsync(Guid id);

    /// <summary>
    /// Получить всех пользователей
    /// </summary>
    Task<List<User>> GetAllAsync();
}