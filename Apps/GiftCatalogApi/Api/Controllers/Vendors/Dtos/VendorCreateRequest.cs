namespace Api.Controllers.Vendors.Dtos;

/// <summary>
/// Запрос на создание вендора
/// </summary>
public record VendorCreateRequest(string Name, string Slug, bool IsActive = true);

