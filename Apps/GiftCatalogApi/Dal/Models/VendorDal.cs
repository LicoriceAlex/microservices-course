namespace Dal.Models;

/// <summary>
/// Вендор — компания, выпускающая подарочные карты
/// </summary>
public class VendorDal
{
    /// <summary>
    /// Уникальный идентификатор
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Название вендора
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Короткий уникальный идентификатор (slug)
    /// </summary>
    public required string Slug { get; set; }

    /// <summary>
    /// Активен ли вендор
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// Дата регистрации вендора
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}