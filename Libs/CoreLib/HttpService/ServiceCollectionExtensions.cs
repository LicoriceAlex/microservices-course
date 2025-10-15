// CoreLib/HttpService/ServiceCollectionExtensions.cs
using CoreLib.HttpService.Services;
using CoreLib.HttpService.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace CoreLib.HttpService;

/// <summary>
/// Регистрация сервисов HTTP-взаимодействия V1
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Добавляет сервисы для отправки HTTP-запросов
    /// </summary>
    public static IServiceCollection AddHttpRequestService(this IServiceCollection services)
    {
        services.AddHttpClient(); // без AddHttpMessageHandler

        services.TryAddTransient<IHttpConnectionService, HttpConnectionService>();
        services.TryAddTransient<IHttpRequestService, HttpRequestService>();

        return services;
    }
}