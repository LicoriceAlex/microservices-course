using Microsoft.AspNetCore.Mvc;
using Services.Contracts.Dtos.Audit;
using Services.Contracts.Repositories;

namespace Api.Controllers;

/// <summary>
/// Контроллер чтения аудита активаций
/// </summary>
[ApiController]
[Route("api/audit")]
public class AuditController : ControllerBase
{
    private readonly IActivationEventRepository _activationEventRepository;

    public AuditController(IActivationEventRepository activationEventRepositoryRepository)
    {
        _activationEventRepository = activationEventRepositoryRepository;
    }

    /// <summary>
    /// Получить события по активации
    /// </summary>
    [HttpGet("activation/{activationId:guid}")]
    public async Task<IActionResult> GetEventsByActivation(Guid activationId)
    {
        var list = await _activationEventRepository.GetByActivationAsync(activationId);
        if (list.Count == 0)
        {
            return NotFound($"события для активации {activationId} не найдены");
        }

        var result = new List<ActivationEventResponse>(list.Count);
        foreach (var activationEvent in list)
        {
            result.Add(new ActivationEventResponse
            {
                Id = activationEvent.Id,
                ActivationId = activationEvent.ActivationId,
                EventType = activationEvent.EventType,
                Message = activationEvent.Message,
                CreatedAt = activationEvent.CreatedAt
            });
        }

        return Ok(result);
    }
}