using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Services.Contracts;

/// <summary>
/// Контракт регистратора инфраструктуры
/// </summary>
public interface IInfrastructureRegister
{
    /// <summary>
    /// Зарегистрировать инфраструктуру хранилища
    /// </summary>
    void Register(IServiceCollection services, IConfiguration configuration);
}