namespace Api.Controllers.Batches.Dtos;

/// <summary>
/// Ответ с информацией о партии
/// </summary>
public record BatchResponse
{
    /// <summary>
    /// Идентификатор партии
    /// </summary>
    public required Guid Id { get; init; }

    /// <summary>
    /// Идентификатор вендора
    /// </summary>
    public required Guid VendorId { get; init; }

    /// <summary>
    /// Идентификатор номинала
    /// </summary>
    public required Guid DenominationId { get; init; }

    /// <summary>
    /// Количество карт в партии
    /// </summary>
    public required int Count { get; init; }

    /// <summary>
    /// Дата истечения срока действия карт
    /// </summary>
    public required DateTime ExpireAt { get; init; }

    /// <summary>
    /// Текущий статус партии
    /// </summary>
    public required string Status { get; init; }

    /// <summary>
    /// Дата создания партии
    /// </summary>
    public required DateTime CreatedAt { get; init; }
}