using ActivationSagaOrchestrator.Saga;
using MassTransit;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddMassTransit(x =>
        {
            x.SetKebabCaseEndpointNameFormatter();

            x.AddSagaStateMachine<ActivationSagaStateMachine, ActivationSagaState>()
                .InMemoryRepository();

            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host("localhost", "/", h =>
                {
                    h.Username("test");
                    h.Password("test");
                });
                
                cfg.ReceiveEndpoint("activation-saga", e =>
                {
                    e.UseInMemoryOutbox(context);
                    e.ConfigureSaga<ActivationSagaState>(context);
                });
            });
        });
    })
    .Build();

await host.RunAsync();