namespace Dal.Models;

/// <summary>
/// Номинал — сумма и валюта подарочной карты
/// </summary>
public class DenominationDal
{
    /// <summary>
    /// Уникальный идентификатор
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Сумма номинала
    /// </summary>
    public required decimal Amount { get; set; }

    /// <summary>
    /// Код валюты (ISO 4217)
    /// </summary>
    public required string Currency { get; set; }

    /// <summary>
    /// Активен ли номинал
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Дата создания записи
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}