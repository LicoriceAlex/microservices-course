using Microsoft.AspNetCore.Mvc;
using Services.Contracts.Dtos.Audit;
using Services.Contracts.Repositories;

namespace Api.Controllers;

/// <summary>
/// контроллер чтения аудита активаций
/// </summary>
[ApiController]
[Route("api/audit")]
public class AuditController : ControllerBase
{
    private readonly IActivationEventRepository _events;

    public AuditController(IActivationEventRepository eventsRepository)
    {
        _events = eventsRepository;
    }

    /// <summary>получить события по активации</summary>
    [HttpGet("activation/{activationId:guid}")]
    public async Task<IActionResult> GetEventsByActivation(Guid activationId)
    {
        var list = await _events.GetByActivationAsync(activationId);
        if (list == null || list.Count == 0)
        {
            return NotFound($"события для активации {activationId} не найдены");
        }

        var result = new List<ActivationEventResponse>(list.Count);
        foreach (var e in list)
        {
            result.Add(new ActivationEventResponse
            {
                Id = e.Id,
                ActivationId = e.ActivationId,
                EventType = e.EventType,
                Message = e.Message,
                CreatedAt = e.CreatedAt
            });
        }

        return Ok(result);
    }
}