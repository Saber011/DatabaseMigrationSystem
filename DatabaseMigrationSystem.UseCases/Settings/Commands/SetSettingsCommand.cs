using DatabaseMigrationSystem.Common.Enums;
using MediatR;

namespace DatabaseMigrationSystem.UseCases.Settings.Commands;

/// <summary>
/// Установить настройки
/// </summary>
public class SetSettingsCommand : IRequest
{
    /// <summary>
    /// Строка подключения к исходной базе данных
    /// </summary>
    public string SourceConnectionString  { get; set; }
    
    /// <summary>
    /// Тип базы данных
    /// </summary>
    public DatabaseType SourceDatabaseType { get; set; }
    
    /// <summary>
    /// Строка подключения к базе данных
    /// </summary>
    public string DestinationConnectionString  { get; set; }
    
    /// <summary>
    /// Тип базы данных
    /// </summary>
    public DatabaseType DestinationDatabaseType { get; set; }
}