namespace DatabaseMigrationSystem.Common.Dto.Request;

public class MigrateTableRequest
{
    public string SourceTable { get; set; }
    
    public string SourceSchema { get; set; }
    
    public string DestinationTable { get; set; }
    
    public string DestinationSchema { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public ColumnMapping[] ColumnsMapping { get; set; }
}