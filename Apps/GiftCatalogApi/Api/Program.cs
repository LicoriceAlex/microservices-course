using Api.Messages.Consumers.Choreography;
using Api.Messages.Consumers.Orchestration;
using Dal;
using Dal.Repositories.Interfaces;
using Dal.Repositories.Implementations;
using Logic.Managers;
using Api.UseCases.Interfaces;
using Api.UseCases;
using CoreLib.DistributedSync.Redis;
using Dal.Repositories;
using Logic.Managers.Interfaces;
using Microsoft.EntityFrameworkCore;
using Serilog;
using CoreLib.Logs;
using CoreLib.TraceId;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

// Логирование
Log.Logger = new LoggerConfiguration()
    .GetConfiguration()
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.TryAddTraceId();
builder.Services.AddLoggerServices();
builder.Services.AddHttpClient("default").AddHttpMessageHandler<TraceIdHttpMessageHandler>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<GiftCatalogDbContext>(opt =>
{
    opt.UseNpgsql(builder.Configuration.GetConnectionString("GiftCatalogDb"));
});

builder.Services.AddScoped<IVendorRepository, VendorRepository>();
builder.Services.AddScoped<IDenominationRepository, DenominationRepository>();
builder.Services.AddScoped<IBatchRepository, BatchRepository>();
builder.Services.AddScoped<ICardRepository, CardRepository>();

builder.Services.AddScoped<IVendorManager, VendorManager>();
builder.Services.AddScoped<IDenominationManager, DenominationManager>();
builder.Services.AddScoped<IBatchesManager, BatchesManager>();
builder.Services.AddScoped<ICardsManager, CardsManager>();

builder.Services.AddScoped<IVendorUseCaseManager, VendorUseCaseManager>();
builder.Services.AddScoped<IDenominationUseCaseManager, DenominationUseCaseManager>();
builder.Services.AddScoped<IBatchUseCaseManager, BatchUseCaseManager>();
builder.Services.AddScoped<ICardUseCaseManager, CardUseCaseManager>();

builder.Services.AddRedisDistributedSync(builder.Configuration);

builder.Services.AddMassTransit(x =>
{
    // Оркестрация
    x.AddConsumer<BlockCardConsumer>();
    x.AddConsumer<ActivateCardConsumer>();

    // Хореография
    x.AddConsumer<ActivationInitiatedConsumer>();
    x.AddConsumer<ActivationConfirmedConsumer>();

    x.SetKebabCaseEndpointNameFormatter();

    x.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host("localhost", "/", h => { h.Username("test"); h.Password("test"); });
        
        cfg.ReceiveEndpoint("block-card", e =>
        {
            e.UseInMemoryOutbox(ctx);
            e.ConfigureConsumer<BlockCardConsumer>(ctx);
        });

        cfg.ReceiveEndpoint("activate-card", e =>
        {
            e.UseInMemoryOutbox(ctx);
            e.ConfigureConsumer<ActivateCardConsumer>(ctx);
        });
        
        cfg.ConfigureEndpoints(ctx);
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// 1. TraceId: создаём/читаем TraceId и добавляем в LogContext
app.UseMiddleware<TraceIdMiddleware>();

// 2. Логирование HTTP-запросов через Serilog (видит TraceId)
app.UseSerilogRequestLogging();

// 3. HTTPS
app.UseHttpsRedirection();

// 4. Контроллеры
app.MapControllers();

app.MapGet("/health", () => Results.Ok(new
{
    status = "ok",
    time = DateTime.UtcNow
}));

app.Run();
