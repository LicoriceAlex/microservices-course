using MassTransit;

namespace ActivationSagaOrchestrator.Saga;

public class ActivationSagaState : SagaStateMachineInstance
{
    public Guid CorrelationId { get; set; }
    public string CurrentState { get; set; } = default!;
    public Guid CardId { get; set; }
}