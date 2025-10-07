using Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Services.Contracts.Models;
using Services.Contracts.Repositories;

namespace Infrastructure.Repositories;

/// <summary>
/// репозиторий активаций на ef core
/// </summary>
public class ActivationRepository : IActivationRepository
{
    private readonly ActivationDbContext _db;

    public ActivationRepository(ActivationDbContext db)
    {
        _db = db;
    }

    public async Task<Guid> CreateAsync(ActivationData a)
    {
        var e = new ActivationEntity
        {
            Id = a.Id,
            UserId = a.UserId,
            CardCodeHash = a.CardCodeHash,
            IdempotencyKey = a.IdempotencyKey,
            Status = a.Status,
            CreatedAt = a.CreatedAt,
            ConfirmedAt = a.ConfirmedAt
        };
        _db.Activations.Add(e);
        await _db.SaveChangesAsync();
        return e.Id;
    }

    public async Task<ActivationData?> GetAsync(Guid id)
    {
        var e = await _db.Activations.FindAsync(id);
        if (e is null) return null;
        return new ActivationData
        {
            Id = e.Id,
            UserId = e.UserId,
            CardCodeHash = e.CardCodeHash,
            IdempotencyKey = e.IdempotencyKey,
            Status = e.Status,
            CreatedAt = e.CreatedAt,
            ConfirmedAt = e.ConfirmedAt
        };
    }

    public async Task<List<ActivationData>> GetAllAsync()
    {
        var list = await _db.Activations.AsNoTracking().OrderByDescending(x => x.CreatedAt).ToListAsync();
        var result = new List<ActivationData>(list.Count);
        foreach (var e in list)
        {
            result.Add(new ActivationData
            {
                Id = e.Id,
                UserId = e.UserId,
                CardCodeHash = e.CardCodeHash,
                IdempotencyKey = e.IdempotencyKey,
                Status = e.Status,
                CreatedAt = e.CreatedAt,
                ConfirmedAt = e.ConfirmedAt
            });
        }
        return result;
    }

    public async Task<ActivationData?> GetByIdempotencyKeyAsync(string key)
    {
        var e = await _db.Activations.AsNoTracking().FirstOrDefaultAsync(x => x.IdempotencyKey == key);
        if (e is null) return null;
        return new ActivationData
        {
            Id = e.Id,
            UserId = e.UserId,
            CardCodeHash = e.CardCodeHash,
            IdempotencyKey = e.IdempotencyKey,
            Status = e.Status,
            CreatedAt = e.CreatedAt,
            ConfirmedAt = e.ConfirmedAt
        };
    }

    public async Task SetStatusAsync(Guid id, string status, DateTime? confirmedAt = null)
    {
        var e = await _db.Activations.FindAsync(id);
        if (e is null) return;
        e.Status = status;
        e.ConfirmedAt = confirmedAt;
        await _db.SaveChangesAsync();
    }
}
