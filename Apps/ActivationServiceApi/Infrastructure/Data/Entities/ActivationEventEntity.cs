namespace Infrastructure.Data.Entities;

/// <summary>
/// сущность ef события активации
/// </summary>
public class ActivationEventEntity
{
    public Guid Id { get; set; }
    public Guid ActivationId { get; set; }
    public string EventType { get; set; } = default!;
    public string Message { get; set; } = default!;
    public DateTime CreatedAt { get; set; }
}