using Api.Controllers.Cards.Dtos;
using Api.UseCases.Interfaces;
using Logic.Managers.Interfaces;

namespace Api.UseCases;

/// <summary>
/// Юзкейс-менеджер карт
/// </summary>
public class CardUseCaseManager : ICardUseCaseManager
{
    private readonly ICardsManager _cardsManager;
    
    public CardUseCaseManager(ICardsManager cardsManager)
    {
        _cardsManager = cardsManager;
    }

    /// <inheritdoc />
    public async Task<List<CardResponse>> GetAllAsync()
    {
        var entities = await _cardsManager.GetAllAsync();
        var result = new List<CardResponse>(entities.Count);

        foreach (var giftCardDal in entities)
        {
            var dto = new CardResponse
            {
                Id = giftCardDal.Id,
                BatchId = giftCardDal.BatchId,
                MaskedCode = giftCardDal.MaskedCode,
                Status = giftCardDal.Status.ToString(),
                ExpireAt = giftCardDal.ExpireAt
            };
            result.Add(dto);
        }

        return result;
    }

    /// <inheritdoc />
    public async Task<CardResponse?> GetByIdAsync(Guid id)
    {
        var entity = await _cardsManager.GetByIdAsync(id);
        if (entity is null)
        {
            return null;
        }

        var dto = new CardResponse
        {
            Id = entity.Id,
            BatchId = entity.BatchId,
            MaskedCode = entity.MaskedCode,
            Status = entity.Status.ToString(),
            ExpireAt = entity.ExpireAt
        };

        return dto;
    }

    /// <inheritdoc />
    public async Task BlockAsync(Guid id)
    {
        await _cardsManager.BlockAsync(id);
    }

    /// <inheritdoc />
    public async Task UnblockAsync(Guid id)
    {
        await _cardsManager.UnblockAsync(id);
    }
}