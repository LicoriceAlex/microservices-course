using Services.Contracts.Dtos.Activations;

namespace Services.Interfaces;

/// <summary>
/// Cервис активаций
/// </summary>
public interface IActivationsService
{
    /// <summary>
    /// Создать активацию
    /// </summary>
    Task<Guid> CreateAsync(ActivationCreateRequest dto);
    
    /// <summary>
    /// Подтвердить активацию
    /// </summary>
    Task ConfirmAsync(Guid activationId, ActivationConfirmRequest dto);
    
    /// <summary>
    /// Получить акцивацию по id
    /// </summary>
    Task<ActivationResponse?> GetAsync(Guid id, bool includeCard = false);
    
    /// <summary>
    /// Получить список всех активаций
    /// </summary>
    /// <returns></returns>
    Task<List<ActivationResponse>> GetAllAsync();
}