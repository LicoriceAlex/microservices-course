using CoreLib.DistributedSync.Abstractions;
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
    private readonly IDistributedSemaphoreFactory _semFactory;

    public CardsManager(ICardRepository repo, IDistributedSemaphoreFactory semFactory)
    {
        _repo = repo;
        _semFactory = semFactory;
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
        var distributedSemaphore = _semFactory.Create($"gc:card:{id}:sem", maxCount: 1, lease: TimeSpan.FromSeconds(30));
        await using (await distributedSemaphore.AcquireAsync(TimeSpan.FromSeconds(2)))
        {
            var card = await _repo.GetByIdAsync(id) ?? throw new KeyNotFoundException($"Card {id} not found.");
            if (card.Status is GiftCardStatus.Activated or GiftCardStatus.Expired)
            {
                throw new InvalidOperationException($"Card {id} cannot be blocked in status {card.Status}.");
            }

            await _repo.SetStatusAsync(id, GiftCardStatus.Blocked);
        }
    }

    /// <inheritdoc />
    public async Task UnblockAsync(Guid id)
    {
        var distributedSemaphore = _semFactory.Create($"gc:card:{id}:sem", 1, TimeSpan.FromSeconds(30));
        await using (await distributedSemaphore.AcquireAsync(TimeSpan.FromSeconds(2)))
        {
            var card = await _repo.GetByIdAsync(id) ?? throw new KeyNotFoundException($"Card {id} not found.");
            if (card.Status != GiftCardStatus.Blocked)
            {
                throw new InvalidOperationException($"Card {id} is not Blocked.");
            }

            await _repo.SetStatusAsync(id, GiftCardStatus.Available);
        }
    }

    /// <inheritdoc />
    public async Task ActivateAsync(Guid id)
    {
        var distributedSemaphore = _semFactory.Create($"gc:card:{id}:sem", 1, TimeSpan.FromSeconds(30));
        await using (await distributedSemaphore.AcquireAsync(TimeSpan.FromSeconds(2)))
        {
            var card = await _repo.GetByIdAsync(id) ?? throw new KeyNotFoundException($"Card {id} not found.");
            if (card.Status is not (GiftCardStatus.Reserved or GiftCardStatus.Blocked))
            {
                throw new InvalidOperationException($"Card {id} cannot be activated from status {card.Status}.");
            }

            await _repo.SetStatusAsync(id, GiftCardStatus.Activated);
        }
    }
}
