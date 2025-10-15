using Services.External.Dto;

namespace Services.External.Interfaces;

/// <summary>
/// Клиент запросов к GiftCatalogApi
/// </summary>
public interface IGiftCatalogClient
{
    /// <summary>
    /// Получить карту по ее id
    /// </summary>
    Task<GiftCardResponse> GetCardByIdAsync(Guid cardId);
}