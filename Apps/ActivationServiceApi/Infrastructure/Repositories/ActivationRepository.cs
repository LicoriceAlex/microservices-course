using Domain.Entities;
using Domain.Enums;
using Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Services.Contracts.Repositories;

namespace Infrastructure.Repositories;

/// <summary>
/// Репозиторий активаций на ef core
/// </summary>
public class ActivationRepository : IActivationRepository
{
    private readonly ActivationDbContext _db;

    public ActivationRepository(ActivationDbContext db)
    {
        _db = db;
    }

    /// <inheritdoc />
    public async Task<Guid> CreateAsync(Activation a)
    {
        var activationEntity = new ActivationEntity
        {
            Id = a.Id,
            UserId = a.UserId,
            CardCodeHash = a.CardCodeHash,
            IdempotencyKey = a.IdempotencyKey,
            Status = a.Status.ToString(),
            CreatedAt = a.CreatedAt,
            ConfirmedAt = a.ConfirmedAt
        };
        _db.Activations.Add(activationEntity);
        await _db.SaveChangesAsync();
        return activationEntity.Id;
    }

    /// <inheritdoc />
    public async Task<Activation?> GetAsync(Guid id)
    {
        var activationEntity = await _db.Activations.FindAsync(id);
        if (activationEntity is null)
        {
            return null;
        }

        return new Activation
        {
            Id = activationEntity.Id,
            UserId = activationEntity.UserId,
            CardCodeHash = activationEntity.CardCodeHash,
            IdempotencyKey = activationEntity.IdempotencyKey,
            Status = Enum.Parse<ActivationStatus>(activationEntity.Status, true),
            CreatedAt = activationEntity.CreatedAt,
            ConfirmedAt = activationEntity.ConfirmedAt
        };
    }

    /// <inheritdoc />
    public async Task<List<Activation>> GetAllAsync()
    {
        var list = await _db.Activations.AsNoTracking().OrderByDescending(x => x.CreatedAt).ToListAsync();
        var result = new List<Activation>(list.Count);
        foreach (var activationEntity in list)
        {
            result.Add(new Activation
            {
                Id = activationEntity.Id,
                UserId = activationEntity.UserId,
                CardCodeHash = activationEntity.CardCodeHash,
                IdempotencyKey = activationEntity.IdempotencyKey,
                Status = Enum.Parse<ActivationStatus>(activationEntity.Status, true),
                CreatedAt = activationEntity.CreatedAt,
                ConfirmedAt = activationEntity.ConfirmedAt
            });
        }
        return result;
    }

    /// <inheritdoc />
    public async Task<Activation?> GetByIdempotencyKeyAsync(string key)
    {
        var activationEntity = await _db.Activations.AsNoTracking().FirstOrDefaultAsync(x => x.IdempotencyKey == key);
        if (activationEntity is null)
        {
            return null;
        }

        return new Activation
        {
            Id = activationEntity.Id,
            UserId = activationEntity.UserId,
            CardCodeHash = activationEntity.CardCodeHash,
            IdempotencyKey = activationEntity.IdempotencyKey,
            Status = Enum.Parse<ActivationStatus>(activationEntity.Status, true),
            CreatedAt = activationEntity.CreatedAt,
            ConfirmedAt = activationEntity.ConfirmedAt
        };
    }

    /// <inheritdoc />
    public async Task SetStatusAsync(Guid id, ActivationStatus status, DateTime? confirmedAt = null)
    {
        var activationEntity = await _db.Activations.FindAsync(id);
        if (activationEntity is null)
        {
            return;
        }

        activationEntity.Status = status.ToString();
        activationEntity.ConfirmedAt = confirmedAt;
        await _db.SaveChangesAsync();
    }
}
