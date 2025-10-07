using Dal.Models;
using Dal.Repositories.Interfaces;
using Logic.Managers.Interfaces;

namespace Logic.Managers;

/// <summary>
/// Менеджер бизнес-логики для работы с номиналами.
/// </summary>
public class DenominationManager : IDenominationManager
{
    private readonly IDenominationRepository _repo;
    
    public DenominationManager(IDenominationRepository repo)
    {
        _repo = repo;
    }
    
    /// <inheritdoc />
    public async Task<List<DenominationDal>> GetAllAsync()
    {
        return await _repo.GetAllAsync();
    }

    /// <inheritdoc />
    public async Task<DenominationDal?> GetByIdAsync(Guid id)
    {
        return await _repo.GetByIdAsync(id);
    }

    /// <inheritdoc />
    public async Task<Guid> CreateAsync(decimal amount, string currency, bool isActive = true)
    {
        if (amount <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), "Amount must be greater than 0.");
        }

        if (string.IsNullOrWhiteSpace(currency) || currency.Length != 3)
        {
            throw new ArgumentException("Currency must be 3-letter ISO code.", nameof(currency));
        }

        var entity = new DenominationDal
        {
            Amount = decimal.Round(amount, 2),
            Currency = currency.ToUpperInvariant(),
            IsActive = isActive
        };

        return await _repo.CreateAsync(entity);
    }

    /// <inheritdoc />
    public async Task UpdateAsync(Guid id, decimal amount, string currency, bool isActive)
    {
        var existing = await _repo.GetByIdAsync(id);
        if (existing is null)
        {
            throw new KeyNotFoundException($"Denominations {id} not found.");
        }

        if (amount <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), "Amount must be greater than 0.");
        }

        if (string.IsNullOrWhiteSpace(currency) || currency.Length != 3)
        {
            throw new ArgumentException("Currency must be 3-letter ISO code.", nameof(currency));
        }

        existing.Amount = decimal.Round(amount, 2);
        existing.Currency = currency.ToUpperInvariant();
        existing.IsActive = isActive;

        await _repo.UpdateAsync(existing);
    }

    /// <inheritdoc />
    public async Task DeleteAsync(Guid id)
    {
        await _repo.DeleteAsync(id);
    }
}
