using DatabaseMigrationSystem.Common.Enums;

namespace DatabaseMigrationSystem.Common.Dto;

public class UserMigrationData
{
    public int Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string SourceDatabase { get; set; }
    public string DestinationDatabase { get; set; }
    public string TableList { get; set; }
    public TimeSpan ExecutionTime { get; set; }
    public MigrationStatus MigrationStatus { get; set; }
}