using Api.Controllers.Vendors.Dtos;
using Api.UseCases.Interfaces;
using Logic.Managers.Interfaces;

namespace Api.UseCases;

/// <summary>
/// Юзкейс-менеджер вендоров
/// </summary>
public class VendorUseCaseManager : IVendorUseCaseManager
{
    private readonly IVendorManager _vendorManager;
    
    public VendorUseCaseManager(IVendorManager vendorManager)
    {
        _vendorManager = vendorManager;
    }

    /// <inheritdoc />
    public async Task<List<VendorResponse>> GetAllAsync()
    {
        var entities = await _vendorManager.GetAllAsync();
        var result = new List<VendorResponse>(entities.Count);

        foreach (var entity in entities)
        {
            var dto = new VendorResponse
            {
                Id = entity.Id,
                Name = entity.Name,
                Slug = entity.Slug,
                IsActive = entity.IsActive,
                CreatedAt = entity.CreatedAt
            };
            result.Add(dto);
        }

        return result;
    }

    /// <inheritdoc />
    public async Task<VendorResponse?> GetByIdAsync(Guid id)
    {
        var entity = await _vendorManager.GetByIdAsync(id);
        if (entity is null)
        {
            return null;
        }

        var dto = new VendorResponse
        {
            Id = entity.Id,
            Name = entity.Name,
            Slug = entity.Slug,
            IsActive = entity.IsActive,
            CreatedAt = entity.CreatedAt
        };

        return dto;
    }

    /// <inheritdoc />
    public async Task<Guid> CreateAsync(VendorCreateRequest dto)
    {
        var vendorId = await _vendorManager.CreateAsync(dto.Name, dto.Slug, dto.IsActive);
        return vendorId;
    }

    /// <inheritdoc />
    public async Task UpdateAsync(Guid id, VendorUpdateRequest dto)
    {
        await _vendorManager.UpdateAsync(id, dto.Name, dto.Slug, dto.IsActive);
    }

    /// <inheritdoc />
    public async Task DeleteAsync(Guid id)
    {
        await _vendorManager.DeleteAsync(id);
    }
}