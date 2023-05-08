using DatabaseMigrationSystem.Common.Enums;

namespace DatabaseMigrationSystem.Infrastructure.DbContext.Entities;

/// <summary>
/// 
/// </summary>
public class Settings
{
    /// <summary>
    /// 
    /// </summary>
    public int Id { get; set; }
    
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
    
    /// <summary>
    /// Пользователь
    /// </summary>
    public int UserId { get; set; }
    
    /// <summary>
    /// Пользователь
    /// </summary>
    public User User { get; set; }
}