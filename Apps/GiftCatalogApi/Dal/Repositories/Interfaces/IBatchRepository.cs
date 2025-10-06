using Dal.Models;

namespace Dal.Repositories.Interfaces;

/// <summary>
/// Репозиторий для работы с партиями подарочных карт
/// </summary>
public interface IBatchRepository
{
    /// <summary>
    /// Получить все партии
    /// </summary>
    Task<List<GiftBatchDal>> GetAllAsync();

    /// <summary>
    /// Получить партию по идентификатору
    /// </summary>
    Task<GiftBatchDal?> GetByIdAsync(Guid id);

    /// <summary>
    /// Создать новую партию и связанные с ней карты
    /// </summary>
    Task<Guid> CreateWithCardsAsync(GiftBatchDal batchDal, IEnumerable<GiftCardDal> cards);

    /// <summary>
    /// Закрыть партию (изменить статус на Closed)
    ///</summary>
    Task CloseAsync(Guid id);
}