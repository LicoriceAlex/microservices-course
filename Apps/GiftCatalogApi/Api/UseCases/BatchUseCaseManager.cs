using Api.Controllers.Batches.Dtos;
using Api.UseCases.Interfaces;
using Logic.Managers.Interfaces;

namespace Api.UseCases;

/// <summary>
/// Юзкейс-менеджер партий
/// </summary>
public class BatchUseCaseManager : IBatchUseCaseManager
{
    private readonly IBatchesManager _batchesManager;
    
    public BatchUseCaseManager(IBatchesManager batchesManager)
    {
        _batchesManager = batchesManager;
    }

    /// <inheritdoc />
    public async Task<List<BatchResponse>> GetAllAsync()
    {
        var entities = await _batchesManager.GetAllAsync();
        var result = new List<BatchResponse>(entities.Count);

        foreach (var x in entities)
        {
            var dto = new BatchResponse
            {
                Id = x.Id,
                VendorId = x.VendorId,
                DenominationId = x.DenominationId,
                Count = x.Count,
                ExpireAt = x.ExpireAt,
                Status = x.Status.ToString(),
                CreatedAt = x.CreatedAt
            };
            result.Add(dto);
        }

        return result;
    }

    /// <inheritdoc />
    public async Task<BatchResponse?> GetByIdAsync(Guid id)
    {
        var giftBatchDal = await _batchesManager.GetByIdAsync(id);
        if (giftBatchDal is null)
        {
            return null;
        }

        var dto = new BatchResponse
        {
            Id = giftBatchDal.Id,
            VendorId = giftBatchDal.VendorId,
            DenominationId = giftBatchDal.DenominationId,
            Count = giftBatchDal.Count,
            ExpireAt = giftBatchDal.ExpireAt,
            Status = giftBatchDal.Status.ToString(),
            CreatedAt = giftBatchDal.CreatedAt
        };

        return dto;
    }

    /// <inheritdoc />
    public async Task<Guid> CreateAsync(BatchCreateRequest dto)
    {
        var batchId = await _batchesManager.CreateAsync(dto.VendorId, dto.DenominationId, dto.Count, dto.ExpireAtUtc);
        return batchId;
    }

    /// <inheritdoc />
    public async Task CloseAsync(Guid id)
    {
        await _batchesManager.CloseAsync(id);
    }
}
