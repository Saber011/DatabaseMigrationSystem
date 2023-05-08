using DatabaseMigrationSystem.DataAccess.Implementations.Settings;
using DatabaseMigrationSystem.DataAccess.Implementations.User;
using DatabaseMigrationSystem.DataAccess.Interfaces.Settings;
using DatabaseMigrationSystem.DataAccess.Interfaces.User;
using DatabaseMigrationSystem.Utils.Modules;
using Microsoft.Extensions.DependencyInjection;

namespace DatabaseMigrationSystem.DataAccess;

public class DataAccessModule : ApplicationModule
{
    public override void Load(IServiceCollection services)
    {
        #region User
        services.AddScoped<ICreateUserRepository, CreateUserRepository>();
        services.AddScoped<ICreateUserTokenRepository, CreateUserTokenRepository>();
        services.AddScoped<IGetAllUsersRepository, GetAllUsersRepository>();
        services.AddScoped<IGetByIdUserGetRepository, GetByIdUserGetRepository>();
        services.AddScoped<IGetByLoginUserRepository, GetByLoginUserRepository>();
        services.AddScoped<IGetUserByTokenRepository, GetUserByTokenRepository>();
        services.AddScoped<IGetUserRolesRepository, GetUserRolesRepository>();
        services.AddScoped<IGetUserTokensRepository, GetUserTokensRepository>();
        services.AddScoped<IRemoveUserRepository, RemoveUserRepository>();
        services.AddScoped<IUpdateUserInfoRepository, UpdateUserInfoRepository>();
        services.AddScoped<IUpdateUserTokenRepository, UpdateUserTokenRepository>();
        #endregion

        #region Settings 
        services.AddScoped<IGetSettingsRepository, GetSettingsRepository>();
        services.AddScoped<IRemoveSettingsRepository, RemoveSettingsRepository>();
        services.AddScoped<ISetSettingsRepository, SetSettingsRepository>();
        services.AddScoped<IUpdateSettingsRepository, UpdateSettingsRepository>();
        #endregion

    }
}