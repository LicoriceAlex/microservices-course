namespace Api.Controllers.Denominations.Dtos;

/// <summary>
/// Запрос на создание номинала
/// </summary>
public record DenominationCreateRequest
{
    /// <summary>
    /// Сумма номинала
    /// </summary>
    public required decimal Amount { get; init; }

    /// <summary>
    /// Валюта номинала в формате ISO (например RUB)
    /// </summary>
    public required string Currency { get; init; }

    /// <summary>
    /// Активен ли номинал
    /// </summary>
    public required bool IsActive { get; init; } = true;
}