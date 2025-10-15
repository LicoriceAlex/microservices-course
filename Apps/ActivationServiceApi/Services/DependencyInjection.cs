using CoreLib.HttpService.Services;
using CoreLib.HttpService.Services.Interfaces;
using CoreLib.TraceId;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services.External;
using Services.External.Interfaces;
using Services.Interfaces;

namespace Services;

/// <summary>
/// Расширения регистрации application-сервисов
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Добавить все сервисы приложения (application layer)
    /// </summary>
    public static IServiceCollection AddActivationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IUsersService, UsersService>();
        services.AddScoped<IActivationsService, ActivationsService>();
        services.AddScoped<IGiftCatalogClient, GiftCatalogClient>();
        return services;
    }
}