using Api.Controllers.Batches.Dtos;
using Api.UseCases.Interfaces;
using Logic.Managers;

namespace Api.UseCases;

/// <summary>Юзкейс-менеджер партий.</summary>
public class BatchUseCaseManager : IBatchUseCaseManager
{
    private readonly BatchesManager _batchesManager;

    public BatchUseCaseManager(BatchesManager batchesManager)
    {
        _batchesManager = batchesManager;
    }

    public async Task<List<BatchResponse>> GetAllAsync() =>
        (await _batchesManager.GetAllAsync()).Select(x => new BatchResponse(x.Id, x.VendorId, x.DenominationId, x.Count, x.ExpireAt, x.Status.ToString(), x.CreatedAt)).ToList();

    public async Task<BatchResponse?> GetByIdAsync(Guid id)
    {
        var b = await _batchesManager.GetByIdAsync(id);
        return b is null ? null : new BatchResponse(b.Id, b.VendorId, b.DenominationId, b.Count, b.ExpireAt, b.Status.ToString(), b.CreatedAt);
    }

    public Task<Guid> CreateAsync(BatchCreateRequest dto) => _batchesManager.CreateAsync(dto.VendorId, dto.DenominationId, dto.Count, dto.ExpireAtUtc);
    public Task CloseAsync(Guid id) => _batchesManager.CloseAsync(id);
}