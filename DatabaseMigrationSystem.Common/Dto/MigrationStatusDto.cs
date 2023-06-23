using DatabaseMigrationSystem.Common.Enums;

namespace DatabaseMigrationSystem.Common.Dto;

/// <summary>
/// 
/// </summary>
public class MigrationStatusDto
{
    /// <summary>
    /// 
    /// </summary>
    public string CurrentTable { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public MigrationStatus Status { get; set; }
}