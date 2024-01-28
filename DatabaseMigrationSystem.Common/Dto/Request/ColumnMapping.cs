namespace DatabaseMigrationSystem.Common.Dto.Request;

public class ColumnMapping
{
    public string DestinationColumn { get; set; }
    
    public string SourceColumn { get; set; }
    
    public string DefaultValue { get; set; }
}