using Services.ActivatedCards.Models;

namespace Services.Interfaces;

/// <summary>
/// Сервис для получения активированных карточек пользователя
/// </summary>
public interface IActivatedCardsService
{
    /// <summary>
    /// Получить список активированных карточек пользователя
    /// </summary>
    Task<List<ActivatedCardDto>> GetUserActivatedCardsAsync(Guid userId, CancellationToken cancellationToken = default);
}