namespace Services.Contracts.Dtos.Users;

/// <summary>
/// ответ с информацией о пользователе
/// </summary>
public record UserResponse
{
    public required Guid Id { get; init; }
    public required string Email { get; init; }
    public required string Name { get; init; }
    public required bool IsActive { get; init; }
    public required DateTime CreatedAt { get; init; }
}