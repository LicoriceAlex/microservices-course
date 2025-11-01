using Logic.Managers.Interfaces;
using MassTransit;
using SagaContracts.Orchestration;

namespace Api.Messages.Consumers.Orchestration;

public class BlockCardConsumer : IConsumer<BlockCard>
{
    private readonly ICardsManager _cardsManager;
    private readonly IPublishEndpoint _publish;

    public BlockCardConsumer(ICardsManager cardsManager, IPublishEndpoint publish)
    {
        _cardsManager = cardsManager;
        _publish = publish;
    }

    public async Task Consume(ConsumeContext<BlockCard> context)
    {
        await _cardsManager.BlockAsync(context.Message.CardId);
        await _publish.Publish<CardBlocked>(new { context.Message.ActivationId, context.Message.CardId });
    }
}