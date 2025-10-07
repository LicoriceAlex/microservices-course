using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services.Contracts.Repositories;
using Infrastructure.Repositories;

namespace Infrastructure;

/// <summary>
/// Расширения регистрации инфраструктуры
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Добавить инфраструктуру: DbContext и репозитории
    /// </summary>
    public static IServiceCollection AddActivationInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<ActivationDbContext>(opt =>
            opt.UseNpgsql(configuration.GetConnectionString("ActivationDb")));

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IActivationRepository, ActivationRepository>();
        services.AddScoped<IActivationEventRepository, ActivationEventRepository>();

        return services;
    }
}