using DatabaseMigrationSystem.Common;
using DatabaseMigrationSystem.Common.Dto;
using DatabaseMigrationSystem.UseCases.Migration.Commands;
using DatabaseMigrationSystem.UseCases.Migration.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DatabaseMigrationSystem.Controllers;

/// <summary>
/// Api для работы с пользователями
/// </summary>
[Route("api/[controller]/[action]")]
[ApiController]
[Produces("application/json")]
public class MigrationController : ControllerBase
{
    private readonly IMediator _mediator;

    public MigrationController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    /// <summary>
    /// Получить список таблиц с исходной базы данных
    /// </summary>
    /// <response code = "200" > Успешное выполнение.</response>
    /// <response code = "500" > Непредвиденная ошибка сервера.</response>
    [HttpGet]
    [ProducesResponseType(typeof(TableInfosDto),200)]
    [ProducesResponseType(typeof(IList<BrokenRule>),400)]
    [ProducesResponseType(500)]
    public async Task<TableInfosDto> GetTables(CancellationToken cancellationToken)
    {
        var query = new GetSourceTablesQuery();
        return await _mediator.Send(query, cancellationToken);
    }
    
    /// <summary>
    /// Мигрировать все таблицы
    /// </summary>
    /// <response code = "200" > Успешное выполнение.</response>
    /// <response code = "500" > Непредвиденная ошибка сервера.</response>
    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(typeof(IList<BrokenRule>),400)]
    [ProducesResponseType(500)]
    public async Task MigrateTables(CancellationToken cancellationToken)
    {
        var command = new MigrateTablesCommand();
        await _mediator.Send(command, cancellationToken);
    }
    
    /// <summary>
    /// Мигрировать переданные таблицы
    /// </summary>
    /// <response code = "200" > Успешное выполнение.</response>
    /// <response code = "500" > Непредвиденная ошибка сервера.</response>
    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(typeof(IList<BrokenRule>),400)]
    [ProducesResponseType(500)]
    public async Task MigrateTable(MigrateTableCommand command, CancellationToken cancellationToken)
    {
        await _mediator.Send(command, cancellationToken);
    }
    
    /// <summary>
    /// Получить статус миграции
    /// </summary>
    /// <response code = "200" > Успешное выполнение.</response>
    /// <response code = "500" > Непредвиденная ошибка сервера.</response>
    [HttpGet]
    [ProducesResponseType(200)]
    [ProducesResponseType(typeof(IList<BrokenRule>),400)]
    [ProducesResponseType(500)]
    public async Task<MigrationStatusDto> GetStatus(CancellationToken cancellationToken)
    {
        var query = new GetMigrationStatusQuery();
        return await _mediator.Send(query, cancellationToken);
    }
    
    /// <summary>
    /// Получить данные об миграциях
    /// </summary>
    /// <response code = "200" > Успешное выполнение.</response>
    /// <response code = "500" > Непредвиденная ошибка сервера.</response>
    [HttpGet]
    [ProducesResponseType(200)]
    [ProducesResponseType(typeof(IList<BrokenRule>),400)]
    [ProducesResponseType(500)]
    public async Task<List<UserMigrationData>> GetMigrationJournalData(CancellationToken cancellationToken)
    {
        var query = new GetMigrationJournalDataQuery();
        return await _mediator.Send(query, cancellationToken);
    }
    
    
    /// <summary>
    /// Получить статус миграции
    /// </summary>
    /// <response code = "200" > Успешное выполнение.</response>
    /// <response code = "500" > Непредвиденная ошибка сервера.</response>
    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(typeof(IList<BrokenRule>),400)]
    [ProducesResponseType(500)]
    public async Task CancelMigration(CancellationToken cancellationToken)
    {
        var command = new CancelMigrateCommand();
        await _mediator.Send(command, cancellationToken);
    }
    
    /// <summary>
    /// Получить информацию по заданнаном подключении
    /// </summary>
    /// <response code = "200" > Успешное выполнение.</response>
    /// <response code = "500" > Непредвиденная ошибка сервера.</response>
    [HttpGet]
    [ProducesResponseType(200)]
    [ProducesResponseType(typeof(IList<BrokenRule>),400)]
    [ProducesResponseType(500)]
    public async Task<CurrentMigrationSettingsDto> GetCurrentMigrationSettings(CancellationToken cancellationToken)
    {
        return await _mediator.Send(new GetCurrentMigrationSettingsQuery(), cancellationToken);
    }
}