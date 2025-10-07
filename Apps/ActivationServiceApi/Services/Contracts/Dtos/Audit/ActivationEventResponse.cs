namespace Services.Contracts.Dtos.Audit;

/// <summary>
/// ответ с информацией о событии активации
/// </summary>
public record ActivationEventResponse
{
    public required Guid Id { get; init; }
    public required Guid ActivationId { get; init; }
    public required string EventType { get; init; }
    public required string Message { get; init; }
    public required DateTime CreatedAt { get; init; }
}