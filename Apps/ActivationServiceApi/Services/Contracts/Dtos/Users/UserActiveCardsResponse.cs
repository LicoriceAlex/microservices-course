namespace Services.Contracts.Dtos.Users;

/// <summary>
/// Респонс модель активных карт пользователя
/// </summary>
public record UserActiveCardsResponse
{
    /// <summary>
    /// Идентификатор пользователя
    /// </summary>
    public required Guid UserId { get; init; }
    
    /// <summary>
    /// Список активных кард пользователя
    /// </summary>
    public required List<UserActiveCardsDto> ActiveCards { get; init; }
}

public record UserActiveCardsDto
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