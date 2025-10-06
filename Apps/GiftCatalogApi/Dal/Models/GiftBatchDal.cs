namespace Dal.Models;

/// <summary>
/// Партия подарочных карт
/// </summary>
public class GiftBatchDal
{
    /// <summary>
    /// Уникальный идентификатор партии
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Идентификатор вендора
    /// </summary>
    public required Guid VendorId { get; set; }

    /// <summary>
    /// Идентификатор номинала
    /// </summary>
    public required Guid DenominationId { get; set; }

    /// <summary>
    /// Количество карт в партии
    /// </summary>
    public required int Count { get; set; }

    /// <summary>
    /// Дата истечения срока действия карт
    /// </summary>
    public required DateTime ExpireAt { get; set; }

    /// <summary>
    /// Текущий статус партии
    /// </summary>
    public required GiftBatchStatus Status { get; set; } = GiftBatchStatus.Issued;

    /// <summary>
    /// Дата создания партии
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

/// <summary>
/// Статус партии подарочных карт
/// </summary>
public enum GiftBatchStatus
{
    /// <summary>
    /// Черновик — партия ещё не выпущена
    /// </summary>
    Draft = 0,

    /// <summary>
    /// Выпущена — карты созданы и активны
    /// </summary>
    Issued = 1,

    /// <summary>
    /// Закрыта — карты больше не выпускаются
    /// </summary>
    Closed = 2
}