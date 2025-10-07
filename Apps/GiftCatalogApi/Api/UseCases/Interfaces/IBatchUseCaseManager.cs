using Api.Controllers.Batches.Dtos;

namespace Api.UseCases.Interfaces;

/// <summary>
/// Интерфейс юзкейс-менеджера для партий карт
/// </summary>
public interface IBatchUseCaseManager
{
    Task<List<BatchResponse>> GetAllAsync();
    Task<BatchResponse?> GetByIdAsync(Guid id);
    Task<Guid> CreateAsync(BatchCreateRequest dto);
    Task CloseAsync(Guid id);
}