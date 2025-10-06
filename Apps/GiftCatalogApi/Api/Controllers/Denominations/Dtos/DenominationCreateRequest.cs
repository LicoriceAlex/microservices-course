namespace Api.Controllers.Denominations.Dtos;

/// <summary>
/// Запрос на создание номинала
/// </summary>
public record DenominationCreateRequest(decimal Amount, string Currency, bool IsActive = true);

