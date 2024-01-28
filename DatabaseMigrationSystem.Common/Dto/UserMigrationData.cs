using DatabaseMigrationSystem.Common.Enums;

namespace DatabaseMigrationSystem.Common.Dto;

public class UserMigrationData
{
    public long TotalRecordsForMigration { get; set; }
    public long CurrentRecordsCount { get; set; }
    public MigrationStatus MigrationStatus { get; set; }
    public TimeSpan MigrationDuration { get; set; }
    public Guid MigrationId { get; set; }
    public double MigrationProgressPercentage { get; set; }
    
    public DateTime Date { get; set; }
}