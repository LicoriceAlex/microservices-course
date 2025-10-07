namespace Api.Controllers.Denominations.Dtos;

/// <summary>
/// Ответ с информацией о номинале
/// </summary>
public record DenominationResponse
{
    /// <summary>
    /// Идентификатор номинала
    /// </summary>
    public required Guid Id { get; init; }

    /// <summary>
    /// Сумма номинала
    /// </summary>
    public required decimal Amount { get; init; }

    /// <summary>
    /// Валюта номинала
    /// </summary>
    public required string Currency { get; init; }

    /// <summary>
    /// Активен ли номинал
    /// </summary>
    public required bool IsActive { get; init; }

    /// <summary>
    /// Дата создания номинала
    /// </summary>
    public required DateTime CreatedAt { get; init; }
}