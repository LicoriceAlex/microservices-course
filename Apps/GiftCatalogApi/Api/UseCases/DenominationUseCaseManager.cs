using Api.Controllers.Denominations.Dtos;
using Api.UseCases.Interfaces;
using Logic.Managers.Interfaces;

namespace Api.UseCases;

/// <summary>
/// Юзкейс-менеджер номиналов
/// </summary>
public class DenominationUseCaseManager : IDenominationUseCaseManager
{
    private readonly IDenominationManager _denominationManager;
    
    public DenominationUseCaseManager(IDenominationManager denominationManager)
    {
        _denominationManager = denominationManager;
    }

    /// <inheritdoc />
    public async Task<List<DenominationResponse>> GetAllAsync()
    {
        var entities = await _denominationManager.GetAllAsync();
        var result = new List<DenominationResponse>(entities.Count);

        foreach (var entity in entities)
        {
            var dto = new DenominationResponse
            {
                Id = entity.Id,
                Amount = entity.Amount,
                Currency = entity.Currency,
                IsActive = entity.IsActive,
                CreatedAt = entity.CreatedAt
            };
            result.Add(dto);
        }

        return result;
    }

    /// <inheritdoc />
    public async Task<DenominationResponse?> GetByIdAsync(Guid id)
    {
        var entity = await _denominationManager.GetByIdAsync(id);
        if (entity is null)
        {
            return null;
        }

        var dto = new DenominationResponse
        {
            Id = entity.Id,
            Amount = entity.Amount,
            Currency = entity.Currency,
            IsActive = entity.IsActive,
            CreatedAt = entity.CreatedAt
        };

        return dto;
    }

    /// <inheritdoc />
    public async Task<Guid> CreateAsync(DenominationCreateRequest dto)
    {
        var denominationId = await _denominationManager.CreateAsync(dto.Amount, dto.Currency, dto.IsActive);
        return denominationId;
    }

    /// <inheritdoc />
    public async Task UpdateAsync(Guid id, DenominationUpdateRequest dto)
    {
        await _denominationManager.UpdateAsync(id, dto.Amount, dto.Currency, dto.IsActive);
    }

    /// <inheritdoc />
    public async Task DeleteAsync(Guid id)
    {
        await _denominationManager.DeleteAsync(id);
    }
}
