namespace Api.Controllers.Denominations.Dtos;

/// <summary>
/// Запрос на обновление номинала
/// </summary>
public record DenominationUpdateRequest
{
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
}