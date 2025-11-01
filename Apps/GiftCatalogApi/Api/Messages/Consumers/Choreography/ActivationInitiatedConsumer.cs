using Logic.Managers.Interfaces;
using MassTransit;
using SagaContracts.Choreography;

namespace Api.Messages.Consumers.Choreography;

/// <summary>
/// Ловит событие ActivationInitiated, блокирует карту и публикует CardBlocked
/// </summary>
public class ActivationInitiatedConsumer : IConsumer<ActivationInitiated>
{
    private readonly ICardsManager _cardsManager;
    private readonly IPublishEndpoint _publish;

    public ActivationInitiatedConsumer(ICardsManager cardsManager, IPublishEndpoint publish)
    {
        _cardsManager = cardsManager;
        _publish = publish;
    }

    public async Task Consume(ConsumeContext<ActivationInitiated> context)
    {
        await _cardsManager.BlockAsync(context.Message.CardId);
        await _publish.Publish<CardBlocked>(new { context.Message.ActivationId, context.Message.CardId });
    }
}