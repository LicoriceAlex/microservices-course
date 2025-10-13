using CoreLib.TraceId.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace CoreLib.TraceId;

/// <summary>
/// Настройка трейс айди
/// </summary>
public static class StartUpTraceId
{
    public static IServiceCollection TryAddTraceId(this IServiceCollection services)
    {
        services.AddScoped<TraceIdAccessor>();

        services.TryAddScoped<ITraceWriter>(p => p.GetRequiredService<TraceIdAccessor>());
        services.TryAddScoped<ITraceReader>(p => p.GetRequiredService<TraceIdAccessor>());
        services.TryAddScoped<ITraceIdAccessor>(p => p.GetRequiredService<TraceIdAccessor>());

        services.AddTransient<TraceIdHttpMessageHandler>();

        return services;
    }
}