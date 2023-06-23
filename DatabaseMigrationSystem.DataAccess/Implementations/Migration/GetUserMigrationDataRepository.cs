using DatabaseMigrationSystem.Common.Dto;
using DatabaseMigrationSystem.Common.Enums;
using DatabaseMigrationSystem.DataAccess.Interfaces.Migration;
using DatabaseMigrationSystem.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace DatabaseMigrationSystem.DataAccess.Implementations.Migration;

public class GetUserMigrationDataRepository : IGetUserMigrationDataRepository
{
    private readonly Func<ApplicationDbContext> _contextFactory;

    public GetUserMigrationDataRepository(Func<ApplicationDbContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }
    
    public async Task<List<UserMigrationData>> Get(int request, CancellationToken cancellationToken)
    {
        await using var context = _contextFactory();

        var migrationLogs = await context.MigrationLog
            .Where(log => log.UserId == request)
            .OrderBy(log => log.Date)
            .ThenBy(log => log.TableName)
            .ToListAsync(cancellationToken: cancellationToken);

        var migrationData = new List<UserMigrationData>();
        UserMigrationData currentMigration = null;

        foreach (var log in migrationLogs)
        {
            switch (log.Status)
            {
                case MigrationStatus.Start: // Начало новой миграции
                    currentMigration = new UserMigrationData
                    {
                        Id = log.Id,
                        StartDate = log.Date,
                        TableList = log.TableName,
                        MigrationStatus = log.Status,
                    };
                    migrationData.Add(currentMigration);
                    break;
                case MigrationStatus.Processed: // Обновление текущей миграции
                    if (currentMigration != null) currentMigration.MigrationStatus = log.Status;
                    break;
                case MigrationStatus.Finish:
                case MigrationStatus.Cancel: // Завершение текущей миграции
                    if (currentMigration != null)
                    {
                        currentMigration.EndDate = log.Date;
                        currentMigration.ExecutionTime = currentMigration.EndDate - currentMigration.StartDate;
                        currentMigration.MigrationStatus = log.Status;
                    }

                    currentMigration = null; // Переход к следующей миграции
                    break;
            }
        }

        return migrationData;
    }
}