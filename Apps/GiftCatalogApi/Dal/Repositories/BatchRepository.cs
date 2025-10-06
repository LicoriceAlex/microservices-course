using Dal.Models;
using Dal.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Dal.Repositories.Implementations;

/// <summary>
/// Реализация репозитория для партий подарочных карт.
/// </summary>
public class BatchRepository : IBatchRepository
{
    private readonly GiftCatalogDbContext _db;

    public BatchRepository(GiftCatalogDbContext db)
    {
        _db = db;
    }

    public async Task<List<GiftBatchDal>> GetAllAsync()
    {
        return await _db.Batches.AsNoTracking().ToListAsync();
    }

    public async Task<GiftBatchDal?> GetByIdAsync(Guid id)
    {
        return await _db.Batches.AsNoTracking().FirstOrDefaultAsync(b => b.Id == id);
    }

    public async Task<Guid> CreateWithCardsAsync(GiftBatchDal batchDal, IEnumerable<GiftCardDal> cards)
    {
        using var tx = await _db.Database.BeginTransactionAsync();
        _db.Batches.Add(batchDal);
        await _db.SaveChangesAsync();

        foreach (var card in cards)
        {
            card.BatchId = batchDal.Id;
            _db.Cards.Add(card);
        }

        await _db.SaveChangesAsync();
        await tx.CommitAsync();

        return batchDal.Id;
    }

    public async Task CloseAsync(Guid id)
    {
        var batch = await _db.Batches.FindAsync(id);
        if (batch != null)
        {
            batch.Status = GiftBatchStatus.Closed;
            await _db.SaveChangesAsync();
        }
    }
}