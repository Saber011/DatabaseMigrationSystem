
using System.Data;
using DatabaseMigrationSystem.DataAccess.Interfaces.Migration;
using DatabaseMigrationSystem.Infrastructure.DbContext.Entities;
using DatabaseMigrationSystem.UseCases.Migration.Commands;
using MediatR;
using OfficeOpenXml;

namespace DatabaseMigrationSystem.UseCases.Migration.Handlers;

public class ExportLogCommandHandler : IRequestHandler<ExportLogCommand, byte[]>
{
    private readonly IGetMigrationLogsRepository _getMigrationLogRepository;

    public ExportLogCommandHandler( IGetMigrationLogsRepository getMigrationLogRepository)
    {
        _getMigrationLogRepository = getMigrationLogRepository;
    }

    public async Task<byte[]> Handle(ExportLogCommand command,
        CancellationToken cancellationToken)
    {
       // Установка лицензии EPPlus на бесплатное использование
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        using var package = new ExcelPackage();
        var worksheet = package.Workbook.Worksheets.Add("Data");

        var data = await _getMigrationLogRepository.Get(command.Id, cancellationToken);

        var dataTable = ConvertToDataTable(data);
        
        // Добавление заголовков
        for (var columnIndex = 1; columnIndex <= dataTable.Columns.Count; columnIndex++)
        {
            worksheet.Cells[1, columnIndex].Value = dataTable.Columns[columnIndex - 1].ColumnName;
        }

        // Добавление данных
        for (var rowIndex = 0; rowIndex < dataTable.Rows.Count; rowIndex++)
        {
            for (var columnIndex = 0; columnIndex < dataTable.Columns.Count; columnIndex++)
            {
                worksheet.Cells[rowIndex + 2, columnIndex + 1].Value = dataTable.Rows[rowIndex][columnIndex];
            }
        }

        // Авто-ресайз колонок
        worksheet.Cells.AutoFitColumns();

        // Сохранение Excel файла в массив байтов
        byte[] fileContents;
        using (var memoryStream = new MemoryStream())
        {
            package.SaveAs(memoryStream);
            fileContents = memoryStream.ToArray();
        }

        return fileContents;
    }


    private DataTable ConvertToDataTable(MigrationLog[] logs)
    {
        var dataTable = new DataTable();

        dataTable.Columns.Add("Id", typeof(int));
        dataTable.Columns.Add("TableName", typeof(string));
        dataTable.Columns.Add("Schema", typeof(string));
        dataTable.Columns.Add("DataCount", typeof(long));
        dataTable.Columns.Add("Status", typeof(int));
        dataTable.Columns.Add("Date", typeof(DateTime));
        dataTable.Columns.Add("UserId", typeof(int));
        dataTable.Columns.Add("CorrectId", typeof(Guid));
        dataTable.Columns.Add("Exception", typeof(string));

        foreach (var log in logs)
        {
            dataTable.Rows.Add(log.Id, log.TableName, log.Schema, log.DataCount, log.Status, log.Date, log.UserId, log.ImportSessionId, log.Exception);
        }

        return dataTable;
    }

}