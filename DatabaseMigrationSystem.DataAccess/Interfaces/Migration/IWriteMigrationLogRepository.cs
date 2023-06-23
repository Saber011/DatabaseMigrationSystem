namespace DatabaseMigrationSystem.DataAccess.Interfaces.Migration;

/// <summary>
/// 
/// </summary>
public interface IWriteMigrationLogRepository: IMutateRepository<Infrastructure.DbContext.Entities.MigrationLog>
{
    
}
