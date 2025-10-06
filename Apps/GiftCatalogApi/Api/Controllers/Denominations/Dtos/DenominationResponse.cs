namespace Api.Controllers.Denominations.Dtos;

/// <summary>
/// Ответ с информацией о номинале
/// </summary>
public record DenominationResponse(Guid Id, decimal Amount, string Currency, bool IsActive, DateTime CreatedAt);