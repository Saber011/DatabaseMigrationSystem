using DatabaseMigrationSystem.Common.Enums;

namespace DatabaseMigrationSystem.Infrastructure.Validators;

/// <summary>
/// Валидатор строки подключения
/// </summary>
public interface IConnectionValidator
{
    /// <summary>
    /// Валидация строки подключения.
    /// </summary>
    /// <param name="databaseType">Тип СУБД</param>
    /// <param name="connectionString">Строка подлючения</param>
    Task ValidateConnection(DatabaseType databaseType, string connectionString);
}