using DatabaseMigrationSystem.Common.Enums;

namespace DatabaseMigrationSystem.Common.Dto.Request;

public class StartDataMigrate
{
    public string SourceTable { get; set; }
    
    public string SourceSchema { get; set; }
    
    public string DestinationTable { get; set; }
    
    public string DestinationSchema { get; set; }
    
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
    /// 
    /// </summary>
    public int UserId { get; set; }
}