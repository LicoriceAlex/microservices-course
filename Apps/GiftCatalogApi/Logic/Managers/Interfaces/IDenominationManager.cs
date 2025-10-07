using Dal.Models;

namespace Logic.Managers.Interfaces;

/// <summary>
/// Интерфейс менеджера бизнес-логики для работы с номиналами
/// </summary>
public interface IDenominationManager
{
    /// <summary>
    /// Получить все номиналы
    /// </summary>
    Task<List<DenominationDal>> GetAllAsync();

    /// <summary>
    /// Получить номинал по идентификатору
    /// </summary>
    Task<DenominationDal?> GetByIdAsync(Guid id);

    /// <summary>
    /// Создать номинал
    /// </summary>
    Task<Guid> CreateAsync(decimal amount, string currency, bool isActive = true);

    /// <summary>
    /// Обновить номинал
    /// </summary>
    Task UpdateAsync(Guid id, decimal amount, string currency, bool isActive);

    /// <summary>
    /// Удалить номинал
    /// </summary>
    Task DeleteAsync(Guid id);
}