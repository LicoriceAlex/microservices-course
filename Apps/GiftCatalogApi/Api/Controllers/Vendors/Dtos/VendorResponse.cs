namespace Api.Controllers.Vendors.Dtos;

/// <summary>
/// Ответ с информацией о вендоре
/// </summary>
public record VendorResponse(Guid Id, string Name, string Slug, bool IsActive, DateTime CreatedAt);