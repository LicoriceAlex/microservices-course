using Logic.Managers.Interfaces;
using MassTransit;
using SagaContracts.Orchestration;

namespace Api.Messages.Consumers.Orchestration;

public class ActivateCardConsumer : IConsumer<ActivateCard>
{
    private readonly ICardsManager _cardsManager;
    private readonly IPublishEndpoint _publish;

    public ActivateCardConsumer(ICardsManager cardsManager, IPublishEndpoint publish)
    {
        _cardsManager = cardsManager;
        _publish = publish;
    }

    public async Task Consume(ConsumeContext<ActivateCard> context)
    {
        await _cardsManager.ActivateAsync(context.Message.CardId);
        await _publish.Publish<CardActivated>(new { context.Message.ActivationId, context.Message.CardId });
    }
}