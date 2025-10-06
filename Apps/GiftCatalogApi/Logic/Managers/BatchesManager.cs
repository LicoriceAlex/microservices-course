using Dal.Models;
using Dal.Repositories.Interfaces;
using Logic.Utilities;

namespace Logic.Managers;

/// <summary>
/// Менеджер для партий подарочных карт
/// </summary>
public class BatchesManager
{
    private readonly IBatchRepository _batches;

    /// <summary>
    /// Конструктор
    /// </summary>
    public BatchesManager(IBatchRepository batches)
    {
        _batches = batches;
    }

    /// <summary>
    /// Получить все партии
    /// </summary>
    public Task<List<GiftBatchDal>> GetAllAsync()
    {
        return _batches.GetAllAsync();
    }

    /// <summary>
    /// Получить партию по идентификатору
    /// </summary>
    public Task<GiftBatchDal?> GetByIdAsync(Guid id)
    {
        return _batches.GetByIdAsync(id);
    }

    /// <summary>
    /// Создать партию и сгенерировать карты
    /// </summary>
    public async Task<Guid> CreateAsync(Guid vendorId, Guid denominationId, int count, DateTime expireAtUtc)
    {
        if (count <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(count), "Count must be greater than 0.");
        }

        if (count > 100_000)
        {
            throw new ArgumentOutOfRangeException(nameof(count), "Count must be <= 100000 for this course project.");
        }

        if (expireAtUtc <= DateTime.UtcNow.AddMinutes(1))
        {
            throw new ArgumentOutOfRangeException(nameof(expireAtUtc), "ExpireAt must be in the future.");
        }

        var batch = new GiftBatchDal
        {
            VendorId = vendorId,
            DenominationId = denominationId,
            Count = count,
            ExpireAt = expireAtUtc,
            Status = GiftBatchStatus.Issued
        };

        // Генерация карточек с хэшом и маской кода
        var cards = new List<GiftCardDal>(count);
        for (int i = 0; i < count; i++)
        {
            var code = CodeTools.GenerateCode();
            var (hash, mask) = CodeTools.HashAndMask(code);

            cards.Add(new GiftCardDal
            {
                CodeHash = hash,
                MaskedCode = mask,
                Status = GiftCardStatus.Available,
                ExpireAt = expireAtUtc
            });
        }

        return await _batches.CreateWithCardsAsync(batch, cards);
    }

    /// <summary>
    /// Закрыть партию (перевод статуса в Closed)
    /// </summary>
    public async Task CloseAsync(Guid id)
    {
        var existing = await _batches.GetByIdAsync(id);
        if (existing is null)
        {
            throw new KeyNotFoundException($"Batch {id} not found.");
        }

        if (existing.Status == GiftBatchStatus.Closed)
        {
            return;
        }

        await _batches.CloseAsync(id);
    }
}
