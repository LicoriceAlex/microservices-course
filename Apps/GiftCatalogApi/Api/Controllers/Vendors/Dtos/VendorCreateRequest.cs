namespace Api.Controllers.Vendors.Dtos;

/// <summary>
/// Запрос на создание вендора
/// </summary>
public record VendorCreateRequest
{
    /// <summary>
    /// Название вендора
    /// </summary>
    public required string Name { get; init; }

    /// <summary>
    /// Уникальный идентификатор вендора в виде slug
    /// </summary>
    public required string Slug { get; init; }

    /// <summary>
    /// Активен ли вендор
    /// </summary>
    public required bool IsActive { get; init; } = true;
}