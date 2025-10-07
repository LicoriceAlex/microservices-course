using Services.Contracts.Models;

namespace Services.Contracts.Repositories;

/// <summary>
/// репозиторий активаций
/// </summary>
public interface IActivationRepository
{
    Task<Guid> CreateAsync(ActivationData activation);
    Task<ActivationData?> GetAsync(Guid id);
    Task<List<ActivationData>> GetAllAsync();
    Task<ActivationData?> GetByIdempotencyKeyAsync(string key);
    Task SetStatusAsync(Guid id, string status, DateTime? confirmedAt = null);
}
