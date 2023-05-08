namespace DatabaseMigrationSystem.Common.Dto;

/// <summary>
/// 
/// </summary>
public class TableInfosDto
{
    /// <summary>
    /// 
    /// </summary>
    public List<TableInfoDto> SourceTables { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public List<TableInfoDto> DestinationTables { get; set; }
}