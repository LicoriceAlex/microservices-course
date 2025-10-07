using Services.Contracts.Dtos.Activations;

namespace Services.Interfaces;

/// <summary>
/// сервис активаций
/// </summary>
public interface IActivationsService
{
    Task<Guid> CreateAsync(ActivationCreateRequest dto);
    Task ConfirmAsync(Guid activationId, ActivationConfirmRequest dto);
    Task<ActivationResponse?> GetAsync(Guid id);
    Task<List<ActivationResponse>> GetAllAsync();
}