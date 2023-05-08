namespace DatabaseMigrationSystem.DataAccess.Entity;

public sealed class TableInfo
{
    public string TableName { get; set; }
    public string Schema { get; set; }
    public bool HasParent { get; set; }
    public string ParentTableName { get; set; }
    public List<string> ChildTables { get; set; } = new List<string>();
}