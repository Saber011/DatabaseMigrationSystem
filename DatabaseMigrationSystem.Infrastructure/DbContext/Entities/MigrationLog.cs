using DatabaseMigrationSystem.Common.Enums;

namespace DatabaseMigrationSystem.Infrastructure.DbContext.Entities;

public class MigrationLog
{
    /// <summary>
    /// 
    /// </summary>
    public int Id { get; set; }
    
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
    
    public MigrationStatus Status { get; set; }
    
    public DateTime Date { get; set; }
    
    public int UserId { get; set; }
}