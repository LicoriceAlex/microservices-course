namespace Api.Controllers.Batches.Dtos;

/// <summary>
/// Запрос на создание партии подарочных карт
/// </summary>
public record BatchCreateRequest(Guid VendorId, Guid DenominationId, int Count, DateTime ExpireAtUtc);