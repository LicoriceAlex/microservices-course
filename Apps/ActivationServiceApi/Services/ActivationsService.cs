using Domain.Enums;
using Services.Contracts.Dtos.Activations;
using Services.Contracts.Models;
using Services.Contracts.Repositories;
using Services.Interfaces;

namespace Services;

/// <summary>
/// реализация сервиса активаций
/// </summary>
public class ActivationsService : IActivationsService
{
    private readonly IActivationRepository _acts;
    private readonly IActivationEventRepository _events;
    private readonly IUserRepository _users;

    public ActivationsService(IActivationRepository acts, IActivationEventRepository eventsRepo, IUserRepository users)
    {
        _acts = acts;
        _events = eventsRepo;
        _users = users;
    }

    public async Task<Guid> CreateAsync(ActivationCreateRequest dto)
    {
        var existing = await _acts.GetByIdempotencyKeyAsync(dto.IdempotencyKey);
        if (existing is not null) return existing.Id;

        var user = await _users.GetAsync(dto.UserId);
        if (user is null || !user.IsActive) throw new InvalidOperationException("user not found or inactive");

        var a = new ActivationData
        {
            Id = Guid.NewGuid(),
            UserId = dto.UserId,
            CardCodeHash = dto.CardCodeHash,
            IdempotencyKey = dto.IdempotencyKey,
            Status = ActivationStatus.Pending.ToString(),
            CreatedAt = DateTime.UtcNow,
            ConfirmedAt = null
        };

        var id = await _acts.CreateAsync(a);

        var ev = new ActivationEventData
        {
            Id = Guid.NewGuid(),
            ActivationId = id,
            EventType = "Created",
            Message = "activation created",
            CreatedAt = DateTime.UtcNow
        };
        await _events.AddAsync(ev);

        return id;
    }

    public async Task ConfirmAsync(Guid activationId, ActivationConfirmRequest dto)
    {
        var a = await _acts.GetAsync(activationId);
        if (a is null) throw new KeyNotFoundException("activation not found");

        if (!string.Equals(a.Status, ActivationStatus.Pending.ToString(), StringComparison.OrdinalIgnoreCase))
        {
            return;
        }

        await _acts.SetStatusAsync(activationId, ActivationStatus.Confirmed.ToString(), DateTime.UtcNow);

        var ev = new ActivationEventData
        {
            Id = Guid.NewGuid(),
            ActivationId = activationId,
            EventType = "Confirmed",
            Message = $"confirmed by user {dto.UserId}",
            CreatedAt = DateTime.UtcNow
        };
        await _events.AddAsync(ev);
    }

    public async Task<ActivationResponse?> GetAsync(Guid id)
    {
        var a = await _acts.GetAsync(id);
        if (a is null) return null;

        return new ActivationResponse
        {
            Id = a.Id,
            UserId = a.UserId,
            CardCodeHash = a.CardCodeHash,
            Status = a.Status,
            CreatedAt = a.CreatedAt,
            ConfirmedAt = a.ConfirmedAt
        };
    }

    public async Task<List<ActivationResponse>> GetAllAsync()
    {
        var list = await _acts.GetAllAsync();
        var result = new List<ActivationResponse>(list.Count);
        foreach (var a in list)
        {
            result.Add(new ActivationResponse
            {
                Id = a.Id,
                UserId = a.UserId,
                CardCodeHash = a.CardCodeHash,
                Status = a.Status,
                CreatedAt = a.CreatedAt,
                ConfirmedAt = a.ConfirmedAt
            });
        }
        return result;
    }
}
