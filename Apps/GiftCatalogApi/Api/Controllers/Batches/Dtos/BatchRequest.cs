namespace Api.Controllers.Batches.Dtos;

/// <summary>
/// Запрос на создание партии подарочных карт
/// </summary>
public record BatchCreateRequest
{
    /// <summary>
    /// Id вендора
    /// </summary>
    public required Guid VendorId { get; init; }
    
    /// <summary>
    /// Id номинала
    /// </summary>
    public required Guid DenominationId { get; init; }
    
    /// <summary>
    /// Кол-во
    /// </summary>
    public required int Count { get; init; }
    
    /// <summary>
    /// Когда истекает
    /// </summary>
    public required DateTime ExpireAtUtc { get; init; }
}

