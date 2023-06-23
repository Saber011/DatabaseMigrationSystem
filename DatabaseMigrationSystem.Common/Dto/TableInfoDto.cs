namespace DatabaseMigrationSystem.Common.Dto;

/// <summary>
/// 
/// </summary>
public sealed class TableInfoDto
{
    /// <summary>
    /// 
    /// </summary>
    public string TableName { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public string Schema { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public int DataCount { get; set; }
}