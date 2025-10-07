using Domain.Entities;
using Domain.Enums;
using Services.Contracts.Dtos.Activations;
using Services.Contracts.Repositories;
using Services.Interfaces;

namespace Services;

/// <summary>
/// Реализация сервиса активаций
/// </summary>
public class ActivationsService : IActivationsService
{
    private readonly IActivationRepository _activationRepository;
    private readonly IActivationEventRepository _activationEventRepository;
    private readonly IUserRepository _userRepository;

    public ActivationsService(
        IActivationRepository activationRepository, 
        IActivationEventRepository activationEventRepositoryRepo, 
        IUserRepository userRepository)
    {
        _activationRepository = activationRepository;
        _activationEventRepository = activationEventRepositoryRepo;
        _userRepository = userRepository;
    }

    /// <inheritdoc />
    public async Task<Guid> CreateAsync(ActivationCreateRequest dto)
    {
        var existing = await _activationRepository.GetByIdempotencyKeyAsync(dto.IdempotencyKey);
        if (existing is not null)
        {
            return existing.Id;
        }

        var user = await _userRepository.GetAsync(dto.UserId);
        if (user is null || !user.IsActive)
        {
            throw new InvalidOperationException("user not found or inactive");
        }

        var entity = new Activation
        {
            UserId = dto.UserId,
            CardCodeHash = dto.CardCodeHash,
            IdempotencyKey = dto.IdempotencyKey,
            Status = ActivationStatus.Pending,
            CreatedAt = DateTime.UtcNow
        };

        var id = await _activationRepository.CreateAsync(entity);

        var activationEvent = new ActivationEvent
        {
            ActivationId = id,
            EventType = "Created",
            Message = "activation created",
            CreatedAt = DateTime.UtcNow
        };
        await _activationEventRepository.AddAsync(activationEvent);

        return id;
    }

    /// <inheritdoc />
    public async Task ConfirmAsync(Guid activationId, ActivationConfirmRequest dto)
    {
        var activation = await _activationRepository.GetAsync(activationId);
        if (activation is null)
        {
            throw new KeyNotFoundException("activation not found");
        }

        if (activation.Status != ActivationStatus.Pending)
        {
            return;
        }

        await _activationRepository.SetStatusAsync(activationId, ActivationStatus.Confirmed, DateTime.UtcNow);

        var activationEvent = new ActivationEvent
        {
            ActivationId = activationId,
            EventType = "Confirmed",
            Message = $"confirmed by user {dto.UserId}",
            CreatedAt = DateTime.UtcNow
        };
        await _activationEventRepository.AddAsync(activationEvent);
    }

    /// <inheritdoc />
    public async Task<ActivationResponse?> GetAsync(Guid id)
    {
        var activation = await _activationRepository.GetAsync(id);
        if (activation is null)
        {
            return null;
        }

        return new ActivationResponse
        {
            Id = activation.Id,
            UserId = activation.UserId,
            CardCodeHash = activation.CardCodeHash,
            Status = activation.Status.ToString(),
            CreatedAt = activation.CreatedAt,
            ConfirmedAt = activation.ConfirmedAt
        };
    }

    /// <inheritdoc />
    public async Task<List<ActivationResponse>> GetAllAsync()
    {
        var list = await _activationRepository.GetAllAsync();
        var result = new List<ActivationResponse>(list.Count);
        foreach (var activation in list)
        {
            result.Add(new ActivationResponse
            {
                Id = activation.Id,
                UserId = activation.UserId,
                CardCodeHash = activation.CardCodeHash,
                Status = activation.Status.ToString(),
                CreatedAt = activation.CreatedAt,
                ConfirmedAt = activation.ConfirmedAt
            });
        }
        return result;
    }
}
