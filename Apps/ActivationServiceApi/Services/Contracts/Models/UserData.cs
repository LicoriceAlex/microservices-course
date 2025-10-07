namespace Services.Contracts.Models;

/// <summary>
/// Модель пользователя для слоя данных
/// </summary>
public record UserData
{
    /// <summary>
    /// Id
    /// </summary>
    public required Guid Id { get; init; }
    
    /// <summary>
    /// Email
    /// </summary>
    public required string Email { get; init; }
    
    /// <summary>
    /// Name
    /// </summary>
    public required string Name { get; init; }
    
    /// <summary>
    /// Активен ли
    /// </summary>
    public required bool IsActive { get; init; }
    
    /// <summary>
    /// Дата регистрации
    /// </summary>
    public required DateTime CreatedAt { get; init; }
}