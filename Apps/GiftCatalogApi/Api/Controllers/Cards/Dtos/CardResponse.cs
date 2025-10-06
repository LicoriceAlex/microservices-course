namespace Api.Controllers.Cards.Dtos;

/// <summary>
/// Ответ с информацией о подарочной карте
/// </summary>
public record CardResponse(Guid Id, Guid BatchId, string MaskedCode, string Status, DateTime ExpireAt);