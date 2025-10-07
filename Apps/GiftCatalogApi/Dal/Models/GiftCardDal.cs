namespace Dal.Models;

/// <summary>
/// Подарочная карта
/// </summary>
public class GiftCardDal
{
    /// <summary>
    /// Уникальный идентификатор карты
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Хэш кода карты (SHA256)
    /// </summary>
    public required string CodeHash { get; set; }

    /// <summary>
    /// Маскированный код для отображения
    /// </summary>
    public required string MaskedCode { get; set; }

    /// <summary>
    /// Статус карты
    /// </summary>
    public required GiftCardStatus Status { get; set; } = GiftCardStatus.Available;

    /// <summary>
    /// Дата истечения срока действия карты
    /// </summary>
    public required DateTime ExpireAt { get; set; }

    /// <summary>
    /// Дата создания карты
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Дата активации (если применимо)
    /// </summary>
    public DateTime? ActivatedAt { get; set; }
    
    /// <summary>
    /// Идентификатор партии, к которой принадлежит карта
    /// </summary>
    public Guid BatchId { get; set; }
}


/// <summary>
/// Статус подарочной карты
/// </summary>
public enum GiftCardStatus
{
    /// <summary>
    /// Доступна для активации
    /// </summary>
    Available = 0,

    /// <summary>
    /// Зарезервирована для операции
    /// </summary>
    Reserved = 1,

    /// <summary>
    /// Активирована пользователем
    /// </summary>
    Activated = 2,

    /// <summary>
    /// Заблокирована
    /// </summary>
    Blocked = 3,

    /// <summary>
    /// Просрочена
    /// </summary>
    Expired = 4
}