using ActivationSagaOrchestrator.Saga;
using MassTransit;
using SagaContracts.Orchestration;

public class ActivationSagaStateMachine : MassTransitStateMachine<ActivationSagaState>
{
    public State WaitingForCardBlock { get; private set; } = null!;
    public State WaitingForConfirmation { get; private set; } = null!;
    public State WaitingForActivation { get; private set; } = null!;

    public Event<StartActivation> StartActivationEvent { get; private set; } = null!;
    public Event<CardBlocked> CardBlockedEvent { get; private set; } = null!;
    public Event<ActivationConfirmed> ActivationConfirmedEvent { get; private set; } = null!;
    public Event<CardActivated> CardActivatedEvent { get; private set; } = null!;

    public ActivationSagaStateMachine()
    {
        InstanceState(x => x.CurrentState);

        Event(() => StartActivationEvent, x =>
        {
            x.CorrelateById(m => m.Message.ActivationId);
            
            x.InsertOnInitial = true;
            
            x.SetSagaFactory(ctx => new ActivationSagaState
            {
                CorrelationId = ctx.Message.ActivationId,
                CardId = ctx.Message.CardId,
                CurrentState  = string.Empty
            });
        });

        Event(() => CardBlockedEvent, x => x.CorrelateById(m => m.Message.ActivationId));
        Event(() => ActivationConfirmedEvent, x => x.CorrelateById(m => m.Message.ActivationId));
        Event(() => CardActivatedEvent, x => x.CorrelateById(m => m.Message.ActivationId));

        Initially(
            When(StartActivationEvent)
                .SendAsync(
                    ctx => new Uri("queue:block-card"),
                    ctx => ctx.Init<BlockCard>(new
                    {
                        ActivationId = ctx.Message.ActivationId,
                        CardId = ctx.Saga.CardId
                    }))
                .TransitionTo(WaitingForCardBlock)
        );

        During(WaitingForCardBlock,
            When(CardBlockedEvent)
                .SendAsync(
                    ctx => new Uri("queue:confirm-activation"),
                    ctx => ctx.Init<ConfirmActivation>(new
                    {
                        ActivationId = ctx.Message.ActivationId,
                        CardId = ctx.Saga.CardId
                    }))
                .TransitionTo(WaitingForConfirmation)
        );

        During(WaitingForConfirmation,
            When(ActivationConfirmedEvent)
                .SendAsync(
                    ctx => new Uri("queue:activate-card"),
                    ctx => ctx.Init<ActivateCard>(new
                    {
                        ActivationId = ctx.Message.ActivationId,
                        CardId = ctx.Saga.CardId
                    }))
                .TransitionTo(WaitingForActivation)
        );

        During(WaitingForActivation,
            When(CardActivatedEvent).Finalize()
        );

        SetCompletedWhenFinalized();
    }
}
