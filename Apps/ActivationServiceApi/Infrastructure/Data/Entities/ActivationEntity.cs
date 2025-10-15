namespace Infrastructure.Data.Entities;

/// <summary>
/// сущность ef активации
/// </summary>
public class ActivationEntity
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    
    public Guid CardId { get; set; }
    public string CardCodeHash { get; set; } = default!;
    public string IdempotencyKey { get; set; } = default!;
    public string Status { get; set; } = default!;
    public DateTime CreatedAt { get; set; }
    public DateTime? ConfirmedAt { get; set; }
}