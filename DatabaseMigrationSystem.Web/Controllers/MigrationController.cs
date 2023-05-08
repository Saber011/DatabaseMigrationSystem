using DatabaseMigrationSystem.UseCases.Settings.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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
    [HttpPost]
    public async Task SetSettings(SetSettingsCommand command, CancellationToken cancellationToken)
    {
        await _mediator.Send(command, cancellationToken);
    }
    
    /// <summary>
    /// Мигрировать все таблицы
    /// </summary>
    /// <response code = "200" > Успешное выполнение.</response>
    /// <response code = "500" > Непредвиденная ошибка сервера.</response>
    [HttpPost]
    public async Task SetSettings1(SetSettingsCommand command, CancellationToken cancellationToken)
    {
        await _mediator.Send(command, cancellationToken);
    }
    
    /// <summary>
    /// Мигрировать переданные таблицы
    /// </summary>
    /// <response code = "200" > Успешное выполнение.</response>
    /// <response code = "500" > Непредвиденная ошибка сервера.</response>
    [HttpPost]
    public async Task SetSettings2(SetSettingsCommand command, CancellationToken cancellationToken)
    {
        await _mediator.Send(command, cancellationToken);
    }
    
    /// <summary>
    /// Получить статус миграции
    /// </summary>
    /// <response code = "200" > Успешное выполнение.</response>
    /// <response code = "500" > Непредвиденная ошибка сервера.</response>
    [HttpPost]
    public async Task SetSettings3(SetSettingsCommand command, CancellationToken cancellationToken)
    {
        await _mediator.Send(command, cancellationToken);
    }
    
    /// <summary>
    /// Получить сведения о данных в базах данных во всех таблицах
    /// </summary>
    /// <response code = "200" > Успешное выполнение.</response>
    /// <response code = "500" > Непредвиденная ошибка сервера.</response>
    [HttpPost]
    public async Task SetSettings4(SetSettingsCommand command, CancellationToken cancellationToken)
    {
        await _mediator.Send(command, cancellationToken);
    }
}