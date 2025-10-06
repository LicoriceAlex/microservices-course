using Dal.Models;
using Dal.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Dal.Repositories;

/// <summary>
/// Реализация репозитория для вендоров.
/// </summary>
public class VendorRepository : IVendorRepository
{
    private readonly GiftCatalogDbContext _db;

    public VendorRepository(GiftCatalogDbContext db)
    {
        _db = db;
    }

    public async Task<List<VendorDal>> GetAllAsync()
    {
        return await _db.Vendors.AsNoTracking().ToListAsync();
    }

    public async Task<VendorDal?> GetByIdAsync(Guid id)
    {
        return await _db.Vendors.AsNoTracking().FirstOrDefaultAsync(v => v.Id == id);
    }

    public async Task<Guid> CreateAsync(VendorDal vendorDal)
    {
        _db.Vendors.Add(vendorDal);
        await _db.SaveChangesAsync();
        return vendorDal.Id;
    }

    public async Task UpdateAsync(VendorDal vendorDal)
    {
        _db.Vendors.Update(vendorDal);
        await _db.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await _db.Vendors.FindAsync(id);
        if (entity != null)
        {
            _db.Vendors.Remove(entity);
            await _db.SaveChangesAsync();
        }
    }
}