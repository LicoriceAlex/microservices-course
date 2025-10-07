namespace Domain.Entities;

/// <summary>
/// Событие аудита активации
/// </summary>
public class ActivationEvent
{
    /// <summary>
    /// Идентификатор события
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Идентификатор активации
    /// </summary>
    public Guid ActivationId { get; set; }

    /// <summary>
    /// Тип события
    /// </summary>
    public required string EventType { get; set; }

    /// <summary>
    /// Сообщение
    /// </summary>
    public required string Message { get; set; }

    /// <summary>
    /// Время события
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}