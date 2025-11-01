using MassTransit;
using Microsoft.Extensions.Logging;
using SagaContracts.Choreography;
using Services.Contracts.Dtos.Activations;
using Services.Interfaces;

namespace Services.Messages.Consumers.Choreography;

/// <summary>
/// Хореография: получили CardBlocked → подтверждаем активацию → публикуем ActivationConfirmed
/// </summary>
public class CardBlockedConsumer : IConsumer<CardBlocked>
{
    private readonly IActivationsService _activations;
    private readonly IPublishEndpoint _publish;
    private readonly ILogger<CardBlockedConsumer> _logger;

    public CardBlockedConsumer(
        IActivationsService activations,
        IPublishEndpoint publish,
        ILogger<CardBlockedConsumer> logger)
    {
        _activations = activations;
        _publish = publish;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<CardBlocked> context)
    {
        _logger.LogInformation("Choreography: CardBlocked: CardId={CardId}, ActivationId={ActivationId}",
            context.Message.CardId, context.Message.ActivationId);

        await _activations.ConfirmAsync(context.Message.ActivationId,
            new ActivationConfirmRequest { UserId = Guid.Empty });

        await _publish.Publish<ActivationConfirmed>(new
        {
            context.Message.ActivationId,
            context.Message.CardId
        });

        _logger.LogInformation("Choreography: ActivationConfirmed published: ActivationId={ActivationId}",
            context.Message.ActivationId);
    }
}