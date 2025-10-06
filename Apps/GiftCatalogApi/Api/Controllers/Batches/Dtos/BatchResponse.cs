namespace Api.Controllers.Batches.Dtos;

/// <summary>
/// Ответ с информацией о партии
/// </summary>
public record BatchResponse(Guid Id, Guid VendorId, Guid DenominationId, int Count, DateTime ExpireAt, string Status, DateTime CreatedAt);