using Dal.Models;
using Dal.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Dal.Repositories;

/// <summary>
/// Реализация репозитория для подарочных карт.
/// </summary>
public class CardRepository : ICardRepository
{
    private readonly GiftCatalogDbContext _db;

    public CardRepository(GiftCatalogDbContext db)
    {
        _db = db;
    }

    /// <inheritdoc />
    public async Task<List<GiftCardDal>> GetAllAsync()
    {
        return await _db.Cards.AsNoTracking().ToListAsync();
    }

    /// <inheritdoc />
    public async Task<GiftCardDal?> GetByIdAsync(Guid id)
    {
        return await _db.Cards.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
    }

    /// <inheritdoc />
    public async Task SetStatusAsync(Guid id, GiftCardStatus status)
    {
        var card = await _db.Cards.FindAsync(id);
        if (card != null)
        {
            card.Status = status;
            await _db.SaveChangesAsync();
        }
    }
}