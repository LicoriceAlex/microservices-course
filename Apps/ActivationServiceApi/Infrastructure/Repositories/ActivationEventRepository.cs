using Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Services.Contracts.Models;
using Services.Contracts.Repositories;

namespace Infrastructure.Repositories;

/// <summary>
/// репозиторий событий аудита на ef core
/// </summary>
public class ActivationEventRepository : IActivationEventRepository
{
    private readonly ActivationDbContext _db;

    public ActivationEventRepository(ActivationDbContext db)
    {
        _db = db;
    }

    public async Task AddAsync(ActivationEventData ev)
    {
        var e = new ActivationEventEntity
        {
            Id = ev.Id,
            ActivationId = ev.ActivationId,
            EventType = ev.EventType,
            Message = ev.Message,
            CreatedAt = ev.CreatedAt
        };
        _db.ActivationEvents.Add(e);
        await _db.SaveChangesAsync();
    }

    public async Task<List<ActivationEventData>> GetByActivationAsync(Guid activationId)
    {
        var list = await _db.ActivationEvents.AsNoTracking()
            .Where(x => x.ActivationId == activationId)
            .OrderBy(x => x.CreatedAt)
            .ToListAsync();

        var result = new List<ActivationEventData>(list.Count);
        foreach (var e in list)
        {
            result.Add(new ActivationEventData
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