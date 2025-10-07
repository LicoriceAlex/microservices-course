namespace Services.Contracts.Models;

/// <summary>
/// плоская модель события активации для слоя данных
/// </summary>
public record ActivationEventData
{
    public required Guid Id { get; init; }
    public required Guid ActivationId { get; init; }
    public required string EventType { get; init; }
    public required string Message { get; init; }
    public required DateTime CreatedAt { get; init; }
}