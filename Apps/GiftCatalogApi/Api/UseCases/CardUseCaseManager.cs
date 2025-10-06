using Api.Controllers.Cards.Dtos;
using Api.UseCases.Interfaces;
using Logic.Managers;

namespace Api.UseCases;

/// <summary>Юзкейс-менеджер карт.</summary>
public class CardUseCaseManager : ICardUseCaseManager
{
    private readonly CardsManager _mgr;
    public CardUseCaseManager(CardsManager mgr) { _mgr = mgr; }

    public async Task<List<CardResponse>> GetAllAsync() =>
        (await _mgr.GetAllAsync()).Select(x => new CardResponse(x.Id, x.BatchId, x.MaskedCode, x.Status.ToString(), x.ExpireAt)).ToList();

    public async Task<CardResponse?> GetByIdAsync(Guid id)
    {
        var c = await _mgr.GetByIdAsync(id);
        return c is null ? null : new CardResponse(c.Id, c.BatchId, c.MaskedCode, c.Status.ToString(), c.ExpireAt);
    }

    public Task BlockAsync(Guid id) => _mgr.BlockAsync(id);
    public Task UnblockAsync(Guid id) => _mgr.UnblockAsync(id);
}