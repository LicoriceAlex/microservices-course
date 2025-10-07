using Dal.Models;
using Dal.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Dal.Repositories.Implementations;

/// <summary>
/// Реализация репозитория для номиналов.
/// </summary>
public class DenominationRepository : IDenominationRepository
{
    private readonly GiftCatalogDbContext _db;

    public DenominationRepository(GiftCatalogDbContext db)
    {
        _db = db;
    }

    /// <inheritdoc />
    public async Task<List<DenominationDal>> GetAllAsync()
    {
        return await _db.Denominations.AsNoTracking().ToListAsync();
    }
    
    /// <inheritdoc />
    public async Task<DenominationDal?> GetByIdAsync(Guid id)
    {
        return await _db.Denominations.AsNoTracking().FirstOrDefaultAsync(d => d.Id == id);
    }

    /// <inheritdoc />
    public async Task<Guid> CreateAsync(DenominationDal denominationDal)
    {
        _db.Denominations.Add(denominationDal);
        await _db.SaveChangesAsync();
        return denominationDal.Id;
    }

    /// <inheritdoc />
    public async Task UpdateAsync(DenominationDal denominationDal)
    {
        _db.Denominations.Update(denominationDal);
        await _db.SaveChangesAsync();
    }

    /// <inheritdoc />
    public async Task DeleteAsync(Guid id)
    {
        var entity = await _db.Denominations.FindAsync(id);
        if (entity != null)
        {
            _db.Denominations.Remove(entity);
            await _db.SaveChangesAsync();
        }
    }
}