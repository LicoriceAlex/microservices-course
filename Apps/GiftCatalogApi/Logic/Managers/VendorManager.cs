using Dal.Models;
using Dal.Repositories.Interfaces;
using Logic.Managers.Interfaces;

namespace Logic.Managers;

/// <summary>
/// Менеджер бизнес-логики для работы с вендорами.
/// </summary>
public class VendorManager : IVendorManager
{
    private readonly IVendorRepository _repo;
    
    public VendorManager(IVendorRepository repo)
    {
        _repo = repo;
    }

    /// <inheritdoc />

    public async Task<List<VendorDal>> GetAllAsync()
    {
        return await _repo.GetAllAsync();
    }

    /// <inheritdoc />
    public async Task<VendorDal?> GetByIdAsync(Guid id)
    {
        return await _repo.GetByIdAsync(id);
    }

    /// <inheritdoc />
    public async Task<Guid> CreateAsync(string name, string slug, bool isActive = true)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Name is required.", nameof(name));
        }

        if (string.IsNullOrWhiteSpace(slug))
        {
            throw new ArgumentException("Slug is required.", nameof(slug));
        }

        var entity = new VendorDal
        {
            Name = name.Trim(),
            Slug = slug.Trim().ToLowerInvariant(),
            IsActive = isActive
        };

        return await _repo.CreateAsync(entity);
    }

    /// <inheritdoc />
    public async Task UpdateAsync(Guid id, string name, string slug, bool isActive)
    {
        var existing = await _repo.GetByIdAsync(id);
        if (existing is null)
        {
            throw new KeyNotFoundException($"Vendor {id} not found.");
        }

        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Name is required.", nameof(name));
        }

        if (string.IsNullOrWhiteSpace(slug))
        {
            throw new ArgumentException("Slug is required.", nameof(slug));
        }

        existing.Name = name.Trim();
        existing.Slug = slug.Trim().ToLowerInvariant();
        existing.IsActive = isActive;

        await _repo.UpdateAsync(existing);
    }

    /// <inheritdoc />
    public async Task DeleteAsync(Guid id)
    {
        await _repo.DeleteAsync(id);
    }
}
