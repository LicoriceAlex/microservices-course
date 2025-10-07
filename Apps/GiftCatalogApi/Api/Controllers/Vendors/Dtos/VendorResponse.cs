namespace Api.Controllers.Vendors.Dtos;

/// <summary>
/// Ответ с информацией о вендоре
/// </summary>
public record VendorResponse
{
    /// <summary>
    /// Идентификатор вендора
    /// </summary>
    public required Guid Id { get; init; }

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

    /// <summary>
    /// Дата создания вендора
    /// </summary>
    public required DateTime CreatedAt { get; init; }
}