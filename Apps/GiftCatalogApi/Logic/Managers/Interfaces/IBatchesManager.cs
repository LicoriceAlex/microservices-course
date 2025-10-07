using Dal.Models;

namespace Logic.Managers.Interfaces;

/// <summary>
/// Интерфейс менеджера для партий подарочных карт
/// </summary>
public interface IBatchesManager
{
    /// <summary>
    /// Получить все партии подарочных карт
    /// </summary>
    Task<List<GiftBatchDal>> GetAllAsync();

    /// <summary>
    /// Получить партию по идентификатору
    /// </summary>
    Task<GiftBatchDal?> GetByIdAsync(Guid id);

    /// <summary>
    /// Создать новую партию и сгенерировать карты
    /// </summary>
    Task<Guid> CreateAsync(Guid vendorId, Guid denominationId, int count, DateTime expireAtUtc);

    /// <summary>
    /// Закрыть партию подарочных карт
    /// </summary>
    Task CloseAsync(Guid id);
}