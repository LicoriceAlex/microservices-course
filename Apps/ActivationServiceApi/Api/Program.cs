using CoreLib.HttpService;
using Infrastructure;
using Services;
using CoreLib.Logs;
using CoreLib.TraceId;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .GetConfiguration()
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.TryAddTraceId();
builder.Services.AddLoggerServices();

builder.Services.AddHttpRequestService();

builder.Services.AddHttpClient("gift-catalog", c =>
{
    c.BaseAddress = new Uri(builder.Configuration["GIFT_CATALOG_BASEURL"]!);
    c.Timeout = TimeSpan.FromSeconds(5);
}).AddHttpMessageHandler<TraceIdHttpMessageHandler>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddActivationServices(builder.Configuration);
builder.Services.AddActivationInfrastructure(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// 1. TraceId — создаём/читаем TraceId и добавляем в LogContext
app.UseMiddleware<TraceIdMiddleware>();

// 2. Логирование HTTP-запросов через Serilog (уже с TraceId)
app.UseSerilogRequestLogging();

// 3. HTTPS (опционально, можно убрать в Docker)
app.UseHttpsRedirection();

// 4. Контроллеры
app.MapControllers();

app.MapGet("/health", () =>
{
    return Results.Ok(new
    {
        status = "ok",
        time = DateTime.UtcNow
    });
});

app.Run();
