using Services.Contracts.Models;

namespace Services.Contracts.Repositories;

/// <summary>
/// репозиторий пользователей
/// </summary>
public interface IUserRepository
{
    Task<Guid> CreateAsync(UserData user);
    Task<UserData?> GetAsync(Guid id);
    Task<List<UserData>> GetAllAsync();
}