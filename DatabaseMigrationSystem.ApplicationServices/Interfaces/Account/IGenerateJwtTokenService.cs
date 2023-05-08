namespace DatabaseMigrationSystem.ApplicationServices.Interfaces.Account;

/// <summary>
/// Сервис для генерации токена
/// </summary>
public interface IGenerateJwtTokenService : IApplicationService<long, string>
{
    
}