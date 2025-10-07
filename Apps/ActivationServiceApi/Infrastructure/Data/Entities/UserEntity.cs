namespace Infrastructure.Data.Entities;

/// <summary>
/// сущность ef пользователя
/// </summary>
public class UserEntity
{
    public Guid Id { get; set; }
    public string Email { get; set; } = default!;
    public string Name { get; set; } = default!;
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
}