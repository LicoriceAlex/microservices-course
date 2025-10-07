using Domain.Entities;
using Domain.Enums;

namespace Services.Contracts.Repositories;

/// <summary>
/// Репозиторий активаций
/// </summary>
public interface IActivationRepository
{
    /// <summary>
    /// Создать активацию
    /// </summary>
    Task<Guid> CreateAsync(Activation activation);

    /// <summary>
    /// Получить активацию
    /// </summary>
    Task<Activation?> GetAsync(Guid id);

    /// <summary>
    /// Получить все активации
    /// </summary>
    Task<List<Activation>> GetAllAsync();

    /// <summary>
    /// Получить по ключу идемпотентности
    /// </summary>
    Task<Activation?> GetByIdempotencyKeyAsync(string key);

    /// <summary>
    /// Установить статус
    /// </summary>
    Task SetStatusAsync(Guid id, ActivationStatus status, DateTime? confirmedAt = null);
}