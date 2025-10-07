namespace Api.Controllers.Vendors.Dtos;

/// <summary>
/// Запрос на обновление вендора
/// </summary>
public record VendorUpdateRequest
{
    /// <summary>
    /// Название вендора
    /// </summary>
    public required string Name { get; init; }

    /// <summary>
    /// Уникальный slug вендора
    /// </summary>
    public required string Slug { get; init; }

    /// <summary>
    /// Активен ли вендор
    /// </summary>
    public required bool IsActive { get; init; }
}