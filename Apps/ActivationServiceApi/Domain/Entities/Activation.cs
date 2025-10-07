using Domain.Enums;

namespace Domain.Entities;

/// <summary>
/// Активация подарочной карты
/// </summary>
public class Activation
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Идентификатор пользователя
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// sha256 хэш кода карты
    /// </summary>
    public required string CardCodeHash { get; set; }

    /// <summary>
    /// Ключ идемпотентности
    /// </summary>
    public required string IdempotencyKey { get; set; }

    /// <summary>
    /// Статус
    /// </summary>
    public ActivationStatus Status { get; set; } = ActivationStatus.Pending;

    /// <summary>
    /// Дата создания
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Дата подтверждения
    /// </summary>
    public DateTime? ConfirmedAt { get; set; }
}