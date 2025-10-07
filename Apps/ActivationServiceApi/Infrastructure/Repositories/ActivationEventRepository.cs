using Domain.Entities;
using Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Services.Contracts.Repositories;

namespace Infrastructure.Repositories;

/// <summary>
/// Репозиторий событий аудита на ef core
/// </summary>
public class ActivationEventRepository : IActivationEventRepository
{
    private readonly ActivationDbContext _db;

    public ActivationEventRepository(ActivationDbContext db)
    {
        _db = db;
    }

    /// <inheritdoc />
    public async Task AddAsync(ActivationEvent ev)
    {
        var activationEventEntity = new ActivationEventEntity
        {
            Id = ev.Id,
            ActivationId = ev.ActivationId,
            EventType = ev.EventType,
            Message = ev.Message,
            CreatedAt = ev.CreatedAt
        };
        _db.ActivationEvents.Add(activationEventEntity);
        await _db.SaveChangesAsync();
    }

    /// <inheritdoc />
    public async Task<List<ActivationEvent>> GetByActivationAsync(Guid activationId)
    {
        var list = await _db.ActivationEvents.AsNoTracking()
            .Where(x => x.ActivationId == activationId)
            .OrderBy(x => x.CreatedAt)
            .ToListAsync();

        var result = new List<ActivationEvent>(list.Count);
        foreach (var e in list)
        {
            result.Add(new ActivationEvent
            {
                Id = e.Id,
                ActivationId = e.ActivationId,
                EventType = e.EventType,
                Message = e.Message,
                CreatedAt = e.CreatedAt
            });
        }
        return result;
    }
}