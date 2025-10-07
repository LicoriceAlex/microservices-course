using Dal.Models;

namespace Logic.Managers.Interfaces;

/// <summary>
/// Интерфейс менеджера для подарочных карт
/// </summary>
public interface ICardsManager
{
    /// <summary>
    /// Получить все подарочные карты
    /// </summary>
    Task<List<GiftCardDal>> GetAllAsync();

    /// <summary>
    /// Получить карту по идентификатору
    /// </summary>
    Task<GiftCardDal?> GetByIdAsync(Guid id);

    /// <summary>
    /// Заблокировать карту
    /// </summary>
    Task BlockAsync(Guid id);

    /// <summary>
    /// Разблокировать карту
    /// </summary>
    Task UnblockAsync(Guid id);
}