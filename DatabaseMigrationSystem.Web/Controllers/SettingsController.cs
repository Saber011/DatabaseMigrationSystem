using DatabaseMigrationSystem.Common;
using DatabaseMigrationSystem.Infrastructure.DbContext.Entities;
using DatabaseMigrationSystem.UseCases.Migration.Queries;
using DatabaseMigrationSystem.UseCases.Settings.Commands;
using DatabaseMigrationSystem.UseCases.Settings.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DatabaseMigrationSystem.Controllers;

/// <summary>
/// Api для работы с настройками
/// </summary>
[Route("api/[controller]/[action]")]
[ApiController]
[Produces("application/json")]
public class SettingsController : ControllerBase
{
    private readonly IMediator _mediator;

    public SettingsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    /// <summary>
    /// Задать настройки
    /// </summary>
    /// <response code = "200" > Успешное выполнение.</response>
    /// <response code = "500" > Непредвиденная ошибка сервера.</response>
    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(typeof(IList<BrokenRule>),400)]
    [ProducesResponseType(500)]
    public async Task SetSettings(SetSettingsCommand command, CancellationToken cancellationToken)
    {
         await _mediator.Send(command, cancellationToken);
    }
    
    /// <summary>
    /// Задать настройки
    /// </summary>
    /// <response code = "200" > Успешное выполнение.</response>
    /// <response code = "500" > Непредвиденная ошибка сервера.</response>
    [HttpGet]
    [ProducesResponseType(typeof(Settings),200)]
    [ProducesResponseType(typeof(IList<BrokenRule>),400)]
    [ProducesResponseType(500)]
    public async Task<Settings> GetSettings(CancellationToken cancellationToken)
    {
        var query = new GetSettingsQuery();
        return await _mediator.Send(query, cancellationToken);
    }
}