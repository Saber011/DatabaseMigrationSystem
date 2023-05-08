using DatabaseMigrationSystem.ApplicationServices.Implementations.Account;
using DatabaseMigrationSystem.ApplicationServices.Implementations.Migration;
using DatabaseMigrationSystem.ApplicationServices.Interfaces.Account;
using DatabaseMigrationSystem.ApplicationServices.Interfaces.Migration;
using DatabaseMigrationSystem.DataAccess.Implementations.Migration;
using DatabaseMigrationSystem.Utils.Modules;
using Microsoft.Extensions.DependencyInjection;

namespace DatabaseMigrationSystem.ApplicationServices;

public class ApplicationServicesModule: ApplicationModule
{
    public override void Load(IServiceCollection services)
    {
        services.AddScoped<IGenerateJwtTokenService, GenerateJwtTokenService>();
        services.AddScoped<IGenerateRefreshTokenService, GenerateRefreshTokenService>();
        
        services.AddScoped<IGetCurrentUserInfoService, GetCurrentUserInfoService>();
        
        services.AddScoped<IMigrationServiceFactory, MigrationServiceFactory>();
        services.AddScoped<IDataMigratorService, DataMigratorService>();
    }
}