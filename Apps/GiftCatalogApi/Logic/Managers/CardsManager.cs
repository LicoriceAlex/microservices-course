using Dal.Models;
using Dal.Repositories.Interfaces;

namespace Logic.Managers;

/// <summary>
/// Менеджер для подарочных карт
/// </summary>
public class CardsManager
{
    private readonly ICardRepository _repo;

    /// <summary>
    /// Конструктор
    /// </summary>
    public CardsManager(ICardRepository repo)
    {
        _repo = repo;
    }

    /// <summary>
    /// Получить все карты.
    /// </summary>
    public Task<List<GiftCardDal>> GetAllAsync()
    {
        return _repo.GetAllAsync();
    }

    /// <summary>
    /// Получить карту по идентификатору.
    /// </summary>
    public Task<GiftCardDal?> GetByIdAsync(Guid id)
    {
        return _repo.GetByIdAsync(id);
    }

    /// <summary>
    /// Заблокировать карту (запрещено для Activated и Expired).
    /// </summary>
    public async Task BlockAsync(Guid id)
    {
        var card = await _repo.GetByIdAsync(id);
        if (card is null)
        {
            throw new KeyNotFoundException($"Card {id} not found.");
        }

        if (card.Status is GiftCardStatus.Activated or GiftCardStatus.Expired)
        {
            throw new InvalidOperationException($"Card {id} cannot be blocked in status {card.Status}.");
        }

        await _repo.SetStatusAsync(id, GiftCardStatus.Blocked);
    }

    /// <summary>
    /// Разблокировать карту (только из состояния Blocked → Available).
    /// </summary>
    public async Task UnblockAsync(Guid id)
    {
        var card = await _repo.GetByIdAsync(id);
        if (card is null)
        {
            throw new KeyNotFoundException($"Card {id} not found.");
        }

        if (card.Status != GiftCardStatus.Blocked)
        {
            throw new InvalidOperationException($"Card {id} is not Blocked.");
        }

        await _repo.SetStatusAsync(id, GiftCardStatus.Available);
    }
}