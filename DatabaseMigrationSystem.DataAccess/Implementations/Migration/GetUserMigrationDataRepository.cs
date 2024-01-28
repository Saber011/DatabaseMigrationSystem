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
    
    public async Task<List<UserMigrationData>> Get((int userId, int page, int pageSize) request, CancellationToken cancellationToken)
    {
        await using var context = _contextFactory();
        var userId = request.userId;
        var page = request.page;
        var pageSize = request.pageSize;

        // Получаем уникальные идентификаторы миграций (CorrectId) для пользователя
        var uniqueMigrationIds = await context.MigrationLog
            .Where(log => log.UserId == userId)
            .Select(log => log.ImportSessionId)
            .Distinct()
            .OrderByDescending(id => id) // Сортировка по убыванию для получения последних миграций
            .Skip(page * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        var result = new List<UserMigrationData>();

        // Для каждого идентификатора миграции получаем данные
        foreach (var migrationId in uniqueMigrationIds)
        {
            // Начальная запись миграции
            var startLog = await context.MigrationLog
                .Where(log => log.UserId == userId && log.ImportSessionId == migrationId && log.Status == (int)MigrationStatus.Start)
                .OrderBy(log => log.Date)
                .FirstOrDefaultAsync(cancellationToken);

            // Последняя запись миграции
            var latestLog = await context.MigrationLog
                .Where(log => log.UserId == userId && log.ImportSessionId == migrationId)
                .OrderByDescending(log => log.Date)
                .FirstOrDefaultAsync(cancellationToken);

            // Суммарное количество обработанных записей
            var processedLogsCount = await context.MigrationLog
                .Where(log => log.UserId == userId && log.ImportSessionId == migrationId && log.Status == MigrationStatus.Processed)
                .SumAsync(log => log.DataCount, cancellationToken);

            var migrationData = new UserMigrationData
            {
                TotalRecordsForMigration = startLog?.DataCount ?? 0,
                CurrentRecordsCount = processedLogsCount,
                MigrationStatus = latestLog?.Status ?? MigrationStatus.Start,
                MigrationDuration = latestLog != null && startLog != null ? latestLog.Date - startLog.Date : TimeSpan.Zero,
                MigrationId = migrationId,
                MigrationProgressPercentage = startLog != null ? (double)processedLogsCount / startLog.DataCount * 100 : 0
            };

            result.Add(migrationData);
        }

        return result;
    }
}
