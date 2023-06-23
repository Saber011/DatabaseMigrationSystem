using DatabaseMigrationSystem.Common;
using DatabaseMigrationSystem.Common.Dto;
using DatabaseMigrationSystem.UseCases.User.Commands;
using DatabaseMigrationSystem.UseCases.User.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DatabaseMigrationSystem.Controllers
{
    /// <summary>
    /// Api для работы с пользователями
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Produces("application/json")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Получить всех пользователей.
        /// </summary>
        /// <response code = "200" > Успешное выполнение.</response>
        /// <response code = "401" > Данный запрос требует аутентификации.</response>
        /// <response code = "500" > Непредвиденная ошибка сервера.</response>
        [HttpGet]
        [ProducesResponseType(typeof(UserDto[]),200)]
        [ProducesResponseType(typeof(IList<BrokenRule>),400)]
        [ProducesResponseType(500)]
        public async Task<UserDto[]> GetAllUsers([FromQuery]GetUsersQuery request, CancellationToken cancellationToken)
        {
           return await _mediator.Send(request, cancellationToken);
        }
        
        /// <summary>
        /// Получить пользователя по id.
        /// </summary>
        /// <response code = "200" > Успешное выполнение.</response>
        /// <response code = "401" > Данный запрос требует аутентификации.</response>
        /// <response code = "500" > Непредвиденная ошибка сервера.</response>
        [HttpGet]
        [ProducesResponseType(typeof(UserDto),200)]
        [ProducesResponseType(typeof(IList<BrokenRule>),400)]
        [ProducesResponseType(500)]
        public async Task<UserDto> GetUserById([FromQuery]GetUserQuery request, CancellationToken cancellationToken)
        {
           return await _mediator.Send(request, cancellationToken);
        }
        
        /// <summary>
        /// Получить удалить пользователя.
        /// </summary>
        /// <response code = "200" > Успешное выполнение.</response>
        /// <response code = "401" > Данный запрос требует аутентификации.</response>
        /// <response code = "500" > Непредвиденная ошибка сервера.</response>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(IList<BrokenRule>),400)]
        [ProducesResponseType(500)]
        public async  Task DeleteUser(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            await _mediator.Send(request, cancellationToken);
        }

        // /// <summary>
        // /// Обновление информации пользователя.
        // /// </summary>
        // /// <response code = "200" > Успешное выполнение.</response>
        // /// <response code = "401" > Данный запрос требует аутентификации.</response>
        // /// <response code = "500" > Непредвиденная ошибка сервера.</response>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(IList<BrokenRule>),400)]
        [ProducesResponseType(500)]
        public async Task UpdateUserInfo(UpdateUserCommand request, CancellationToken cancellationToken)
        {
           await _mediator.Send(request, cancellationToken);
        }
    }
}