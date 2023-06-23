using DatabaseMigrationSystem.Infrastructure.PipelineBehavior;
using DatabaseMigrationSystem.Infrastructure.services;
using DatabaseMigrationSystem.Infrastructure.Validators;
using DatabaseMigrationSystem.Utils.Modules;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace DatabaseMigrationSystem.Infrastructure;

public class InfrastructureModule : ApplicationModule
{
    public override void Load(IServiceCollection services)
    {
        services.AddScoped<IConnectionValidator, ConnectionValidator>();
        services.AddMemoryCache();
        services.AddSingleton<ICacheService, InMemoryCacheService>();
    }
}