using Dal.Models;
using Dal.Repositories.Interfaces;

namespace Logic.Managers;

/// <summary>
/// Менеджер бизнес-логики для работы с вендорами.
/// </summary>
public class VendorManager
{
    private readonly IVendorRepository _repo;

    /// <summary>
    /// Конструктор.
    /// </summary>
    public VendorManager(IVendorRepository repo)
    {
        _repo = repo;
    }

    /// <summary>
    /// Получить всех вендоров.
    /// </summary>
    public Task<List<VendorDal>> GetAllAsync()
    {
        return _repo.GetAllAsync();
    }

    /// <summary>
    /// Получить вендора по идентификатору.
    /// </summary>
    public Task<VendorDal?> GetByIdAsync(Guid id)
    {
        return _repo.GetByIdAsync(id);
    }

    /// <summary>
    /// Создать вендора.
    /// </summary>
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

    /// <summary>
    /// Обновить вендора.
    /// </summary>
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

    /// <summary>
    /// Удалить вендора.
    /// </summary>
    public Task DeleteAsync(Guid id)
    {
        return _repo.DeleteAsync(id);
    }
}
