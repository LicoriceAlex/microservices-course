namespace Api.Controllers.Denominations.Dtos;

/// <summary>
/// Запрос на обновление номинала
/// </summary>
public record DenominationUpdateRequest(decimal Amount, string Currency, bool IsActive);