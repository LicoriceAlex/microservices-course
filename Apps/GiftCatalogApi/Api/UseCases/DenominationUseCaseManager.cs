using Api.Controllers.Denominations.Dtos;
using Api.UseCases.Interfaces;
using Logic.Managers;

namespace Api.UseCases;

/// <summary>Юзкейс-менеджер номиналов.</summary>
public class DenominationUseCaseManager : IDenominationUseCaseManager
{
    private readonly DenominationManager _mgr;
    public DenominationUseCaseManager(DenominationManager mgr) { _mgr = mgr; }

    public async Task<List<DenominationResponse>> GetAllAsync() =>
        (await _mgr.GetAllAsync()).Select(x => new DenominationResponse(x.Id, x.Amount, x.Currency, x.IsActive, x.CreatedAt)).ToList();

    public async Task<DenominationResponse?> GetByIdAsync(Guid id)
    {
        var d = await _mgr.GetByIdAsync(id);
        return d is null ? null : new DenominationResponse(d.Id, d.Amount, d.Currency, d.IsActive, d.CreatedAt);
    }

    public Task<Guid> CreateAsync(DenominationCreateRequest dto) => _mgr.CreateAsync(dto.Amount, dto.Currency, dto.IsActive);
    public Task UpdateAsync(Guid id, DenominationUpdateRequest dto) => _mgr.UpdateAsync(id, dto.Amount, dto.Currency, dto.IsActive);
    public Task DeleteAsync(Guid id) => _mgr.DeleteAsync(id);
}