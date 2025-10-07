using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services.Contracts;
using Services.Interfaces;

namespace Services;

/// <summary>
/// расширения регистрации сервисов и инфраструктуры
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// зарегистрировать сервисы и подцепить инфраструктуру по имени сборки
    /// </summary>
    public static IServiceCollection AddActivationServices(this IServiceCollection services, IConfiguration cfg, string infrastructureAssemblyName = "Infrastructure")
    {
        services.AddScoped<IUsersService, UsersService>();
        services.AddScoped<IActivationsService, ActivationsService>();

        TryRegisterInfrastructure(services, cfg, infrastructureAssemblyName);
        return services;
    }

    private static void TryRegisterInfrastructure(IServiceCollection services, IConfiguration cfg, string asmName)
    {
        try
        {
            var asm = Assembly.Load(asmName);
            var registrarType = asm.GetTypes()
                .FirstOrDefault(t => typeof(IInfrastructureRegister).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);

            if (registrarType is null) return;

            var registrar = (IInfrastructureRegister?)Activator.CreateInstance(registrarType);
            registrar?.Register(services, cfg);
        }
        catch
        {
            // допускаем отсутствие инфраструктуры в юнит-тестах
        }
    }
}