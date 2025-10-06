using Dal.Models;

namespace Dal.Repositories.Interfaces;

/// <summary>
/// Репозиторий для работы с подарочными картами
/// </summary>
public interface ICardRepository
{
    /// <summary>
    /// Получить все карты
    /// </summary>
    Task<List<GiftCardDal>> GetAllAsync();

    /// <summary>
    /// Получить карту по идентификатору
    /// </summary>
    Task<GiftCardDal?> GetByIdAsync(Guid id);

    /// <summary>
    /// Изменить статус карты
    /// </summary>
    Task SetStatusAsync(Guid id, GiftCardStatus status);
}