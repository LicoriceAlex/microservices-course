namespace Services.Contracts.Dtos.Activations;

/// <summary>
/// ответ с информацией об активации
/// </summary>
public record ActivationResponse
{
    public required Guid Id { get; init; }
    public required Guid UserId { get; init; }
    public required string CardCodeHash { get; init; }
    public required string Status { get; init; }
    public required DateTime CreatedAt { get; init; }
    public DateTime? ConfirmedAt { get; init; }
}