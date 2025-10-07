using Dal.Models;
using Dal.Repositories.Interfaces;
using Logic.Managers.Interfaces;

namespace Logic.Managers;

/// <summary>
/// Менеджер для подарочных карт
/// </summary>
public class CardsManager : ICardsManager
{
    private readonly ICardRepository _repo;
    
    public CardsManager(ICardRepository repo)
    {
        _repo = repo;
    }

    /// <inheritdoc />
    public async Task<List<GiftCardDal>> GetAllAsync()
    {
        return await _repo.GetAllAsync();
    }

    /// <inheritdoc />
    public async Task<GiftCardDal?> GetByIdAsync(Guid id)
    {
        return await _repo.GetByIdAsync(id);
    }

    /// <inheritdoc />
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

    /// <inheritdoc />
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