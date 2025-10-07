using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services.Contracts;
using Services.Contracts.Repositories;
using Infrastructure.Repositories;

namespace Infrastructure;

/// <summary>
/// регистратор инфраструктуры
/// </summary>
public sealed class InfrastructureRegister : IInfrastructureRegister
{
    public void Register(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ActivationDbContext>(opt =>
            opt.UseNpgsql(configuration.GetConnectionString("ActivationDb")));

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IActivationRepository, ActivationRepository>();
        services.AddScoped<IActivationEventRepository, ActivationEventRepository>();
    }
}