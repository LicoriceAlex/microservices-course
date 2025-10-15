namespace Services.External.Dto;

public sealed record GiftCardResponse
{
    /// <summary>
    /// Идентификатор карты
    /// </summary>
    public required Guid Id { get; init; }

    /// <summary>
    /// Идентификатор партии, к которой относится карта
    /// </summary>
    public required Guid BatchId { get; init; }

    /// <summary>
    /// Маскированный код карты
    /// </summary>
    public required string MaskedCode { get; init; }

    /// <summary>
    /// Текущий статус карты
    /// </summary>
    public required string Status { get; init; }

    /// <summary>
    /// Дата истечения срока действия карты
    /// </summary>
    public required DateTime ExpireAt { get; init; }
}