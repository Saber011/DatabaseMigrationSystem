using DatabaseMigrationSystem.Common.Enums;

namespace DatabaseMigrationSystem.ApplicationServices.Interfaces.Migration;

/// <summary>
/// Фабрика создания сервиса миграции
/// </summary>
public interface IMigrationServiceFactory
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public IMigrationService Create(DatabaseType type);
}