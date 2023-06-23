using System.ComponentModel;

namespace DatabaseMigrationSystem.Common.Enums;

/// <summary>
/// Типы подключения
/// </summary>
public enum DatabaseType
{
    /// <summary>
    /// 
    /// </summary>
    [Description(nameof(SqlServer))]
    SqlServer,
    
    /// <summary>
    /// 
    /// </summary>
    [Description(nameof(PostgresSql))]
    PostgresSql,
    
    /// <summary>
    /// 
    /// </summary>
    [Description(nameof(Oracle))]
    Oracle,
    
    /// <summary>
    /// 
    /// </summary>
    [Description(nameof(MySQl))]
    MySQl
}