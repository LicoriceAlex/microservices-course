using Api.Controllers.Cards.Dtos;

namespace Api.UseCases.Interfaces;

/// <summary>
/// Интерфейс юзкейс-менеджера для карт
/// </summary>
public interface ICardUseCaseManager
{
    Task<List<CardResponse>> GetAllAsync();
    Task<CardResponse?> GetByIdAsync(Guid id);
    Task BlockAsync(Guid id);
    Task UnblockAsync(Guid id);
}