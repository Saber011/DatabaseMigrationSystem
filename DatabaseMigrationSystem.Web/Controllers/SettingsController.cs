using DatabaseMigrationSystem.UseCases.Settings.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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
    public async Task SetSettings(SetSettingsCommand command, CancellationToken cancellationToken)
    {
         await _mediator.Send(command, cancellationToken);
    }
}