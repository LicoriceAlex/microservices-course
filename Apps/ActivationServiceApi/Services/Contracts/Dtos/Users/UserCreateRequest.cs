namespace Services.Contracts.Dtos.Users;

/// <summary>
/// запрос на создание пользователя
/// </summary>
public record UserCreateRequest
{
    /// <summary>электронная почта</summary>
    public required string Email { get; init; }

    /// <summary>имя пользователя</summary>
    public required string Name { get; init; }
}