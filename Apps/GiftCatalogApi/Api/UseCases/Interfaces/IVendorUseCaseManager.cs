using Api.Controllers.Vendors.Dtos;

namespace Api.UseCases.Interfaces;

/// <summary>
/// Интерфейс юзкейс-менеджера для вендоров
/// </summary>
public interface IVendorUseCaseManager
{
    Task<List<VendorResponse>> GetAllAsync();
    Task<VendorResponse?> GetByIdAsync(Guid id);
    Task<Guid> CreateAsync(VendorCreateRequest dto);
    Task UpdateAsync(Guid id, VendorUpdateRequest dto);
    Task DeleteAsync(Guid id);
}