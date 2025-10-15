namespace Services.Contracts.Dtos.Activations;

/// <summary>
/// запрос на создание активации
/// </summary>
public record ActivationCreateRequest
{
    /// <summary>идентификатор пользователя</summary>
    public required Guid UserId { get; init; }

    /// <summary>sha256 хэш кода карты</summary>
    public required string CardCodeHash { get; init; }
    
    /// <summary>id карты</summary>
    public required Guid CardId { get; init; }

    /// <summary>ключ идемпотентности</summary>
    public required string IdempotencyKey { get; init; }
}