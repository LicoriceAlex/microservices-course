using MassTransit;
using Microsoft.AspNetCore.Mvc;
using SagaContracts.Choreography;
using SagaContracts.Orchestration;
using Services.Contracts.Dtos.Activations;
using Services.Interfaces;

namespace Api.Controllers;

[ApiController]
[Route("api/saga")]
public class SagaController(IActivationsService activationsService, IPublishEndpoint publish, ISendEndpointProvider sender)
    : ControllerBase
{
    [HttpPost("choreography")]
    public async Task<IActionResult> StartChoreography([FromQuery] Guid cardId, [FromQuery] Guid userId)
    {
        var activationId = await activationsService.CreateAsync(new ActivationCreateRequest
        {
            CardId = cardId,
            CardCodeHash = string.Empty,
            IdempotencyKey = Guid.NewGuid().ToString(),
            UserId = userId
        });

        await publish.Publish<ActivationInitiated>(new { ActivationId = activationId, CardId = cardId });
        return Ok(new { ActivationId = activationId, Mode = "choreography" });
    }

    [HttpPost("orchestration")]
    public async Task<IActionResult> StartOrchestration([FromQuery] Guid cardId, [FromQuery] Guid userId)
    {
        var activationId = await activationsService.CreateAsync(new ActivationCreateRequest
        {
            CardId = cardId,
            CardCodeHash = string.Empty,
            IdempotencyKey = Guid.NewGuid().ToString(),
            UserId = userId
        });

        var endpoint = await sender.GetSendEndpoint(new Uri("queue:activation-saga"));
        await endpoint.Send<StartActivation>(new { ActivationId = activationId, CardId = cardId });

        return Ok(new { ActivationId = activationId, Mode = "orchestration" });
    }
}