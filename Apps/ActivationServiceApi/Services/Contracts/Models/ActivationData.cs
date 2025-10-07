namespace Services.Contracts.Models;

/// <summary>
/// плоская модель активации для слоя данных
/// </summary>
public record ActivationData
{
    public required Guid Id { get; init; }
    public required Guid UserId { get; init; }
    public required string CardCodeHash { get; init; }
    public required string IdempotencyKey { get; init; }
    public required string Status { get; init; }
    public required DateTime CreatedAt { get; init; }
    public DateTime? ConfirmedAt { get; init; }
}