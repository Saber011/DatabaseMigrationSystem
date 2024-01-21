using DatabaseMigrationSystem.Common.Enums;

namespace DatabaseMigrationSystem.Common.Dto;

public class CurrentMigrationSettingsDto
{
    /// <summary>
    /// 
    /// </summary>
    public string SourceDatabaseDataInfo  { get; set; }
    
    /// <summary>
    /// Тип базы данных
    /// </summary>
    public DatabaseType SourceDatabaseType { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public string DestinationDatabaseDataInfo  { get; set; }
    
    /// <summary>
    /// Тип базы данных
    /// </summary>
    public DatabaseType DestinationDatabaseType { get; set; }
}