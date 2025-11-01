using MassTransit;
using Microsoft.Extensions.Logging;
using SagaContracts.Orchestration;
using Services.Contracts.Dtos.Activations;
using Services.Interfaces;

namespace Services.Messages.Consumers.Orchestration;

/// <summary>
/// Обрабатывает команду ConfirmActivation от оркестратора, помечает активацию подтверждённой и публикует событие ActivationConfirmed
/// </summary>
public class ConfirmActivationConsumer(
    IActivationsService activationsService,
    IPublishEndpoint publish,
    ILogger<ConfirmActivationConsumer> logger)
    : IConsumer<ConfirmActivation>
{
    public async Task Consume(ConsumeContext<ConfirmActivation> context)
    {
        logger.LogInformation("Orchestration: подтверждаем активацию {ActivationId}", context.Message.ActivationId);
        await activationsService.ConfirmAsync(context.Message.ActivationId, new ActivationConfirmRequest { UserId = Guid.Empty });
        await publish.Publish<ActivationConfirmed>(new { context.Message.ActivationId, context.Message.CardId });
    }
}