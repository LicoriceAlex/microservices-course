using Api.Controllers.Denominations.Dtos;

namespace Api.UseCases.Interfaces;

/// <summary>
/// Интерфейс юзкейс-менеджера для номиналов
/// </summary>
public interface IDenominationUseCaseManager
{
    Task<List<DenominationResponse>> GetAllAsync();
    Task<DenominationResponse?> GetByIdAsync(Guid id);
    Task<Guid> CreateAsync(DenominationCreateRequest dto);
    Task UpdateAsync(Guid id, DenominationUpdateRequest dto);
    Task DeleteAsync(Guid id);
}