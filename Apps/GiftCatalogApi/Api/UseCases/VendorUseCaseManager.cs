using Api.Controllers.Vendors.Dtos;
using Api.UseCases.Interfaces;
using Logic.Managers;

namespace Api.UseCases;

/// <summary>Юзкейс-менеджер вендоров.</summary>
public class VendorUseCaseManager : IVendorUseCaseManager
{
    private readonly VendorManager _mgr;
    public VendorUseCaseManager(VendorManager mgr) { _mgr = mgr; }

    public async Task<List<VendorResponse>> GetAllAsync() =>
        (await _mgr.GetAllAsync()).Select(v => new VendorResponse(v.Id, v.Name, v.Slug, v.IsActive, v.CreatedAt)).ToList();

    public async Task<VendorResponse?> GetByIdAsync(Guid id)
    {
        var v = await _mgr.GetByIdAsync(id);
        return v is null ? null : new VendorResponse(v.Id, v.Name, v.Slug, v.IsActive, v.CreatedAt);
    }

    public Task<Guid> CreateAsync(VendorCreateRequest dto) => _mgr.CreateAsync(dto.Name, dto.Slug, dto.IsActive);
    public Task UpdateAsync(Guid id, VendorUpdateRequest dto) => _mgr.UpdateAsync(id, dto.Name, dto.Slug, dto.IsActive);
    public Task DeleteAsync(Guid id) => _mgr.DeleteAsync(id);
}