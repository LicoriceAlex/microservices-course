namespace Services.Contracts.Dtos.Activations;

/// <summary>
/// запрос на подтверждение активации
/// </summary>
public record ActivationConfirmRequest
{
    /// <summary>идентификатор пользователя подтверждающего</summary>
    public required Guid UserId { get; init; }
}