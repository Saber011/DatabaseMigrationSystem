using DatabaseMigrationSystem.Common.Dto;

namespace DatabaseMigrationSystem.ApplicationServices.Interfaces.Account;

/// <summary>
/// Сервис для генерации refresh токена
/// </summary>
public interface IGenerateRefreshTokenService : IApplicationService<string, RefreshToken>
{
    
}