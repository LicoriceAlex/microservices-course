using Domain.Entities;

namespace Services.Contracts.Repositories;

/// <summary>
/// Репозиторий событий аудита
/// </summary>
public interface IActivationEventRepository
{
    /// <summary>
    /// Добавить событие
    /// </summary>
    Task AddAsync(ActivationEvent ev);

    /// <summary>
    /// Получить события по активации
    /// </summary>
    Task<List<ActivationEvent>> GetByActivationAsync(Guid activationId);
}