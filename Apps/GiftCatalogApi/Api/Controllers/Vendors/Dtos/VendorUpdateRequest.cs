namespace Api.Controllers.Vendors.Dtos;

/// <summary>
/// Запрос на обновление вендора
/// </summary>
public record VendorUpdateRequest(string Name, string Slug, bool IsActive);