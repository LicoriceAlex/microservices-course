using Logic.Managers.Interfaces;
using MassTransit;
using SagaContracts.Choreography;

namespace Api.Messages.Consumers.Choreography;

/// <summary>
/// Обрабатывает событие ActivationConfirmed (хореография) и активирует карту
/// </summary>
public class ActivationConfirmedConsumer : IConsumer<ActivationConfirmed>
{
    private readonly ICardsManager _cardsManager;

    public ActivationConfirmedConsumer(ICardsManager cardsManager) => _cardsManager = cardsManager;

    public async Task Consume(ConsumeContext<ActivationConfirmed> context)
    {
        await _cardsManager.ActivateAsync(context.Message.CardId);
    }
}