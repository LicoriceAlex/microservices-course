using Services.Contracts.Models;

namespace Services.Contracts.Repositories;

/// <summary>
/// репозиторий событий аудита
/// </summary>
public interface IActivationEventRepository
{
    Task AddAsync(ActivationEventData ev);
    Task<List<ActivationEventData>> GetByActivationAsync(Guid activationId);
}