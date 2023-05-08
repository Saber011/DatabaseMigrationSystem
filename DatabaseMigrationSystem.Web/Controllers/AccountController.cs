using System.ComponentModel.DataAnnotations;
using DatabaseMigrationSystem.Common.Dto;
using DatabaseMigrationSystem.UseCases.Account.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DatabaseMigrationSystem.Controllers
{
    /// <summary>
    /// Api для работы с пользователями
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Produces("application/json")]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Авторизация
        /// </summary>
        /// <response code = "200" > Успешное выполнение.</response>
        /// <response code = "500" > Непредвиденная ошибка сервера.</response>
        [HttpPost]
        public async Task<AuthenticateInfo> Authenticate(AuthenticateCommand request, CancellationToken cancellationToken)
        {
            request.IpAddress = IpAddress();
            
            var response = await _mediator.Send(request, cancellationToken);
        
            SetTokenCookie(response.RefreshToken);

            return response;
        }
        
        /// <summary>
        /// Получить рефреш токен
        /// </summary>
        /// <response code = "200" > Успешное выполнение.</response>
        /// <response code = "500" > Непредвиденная ошибка сервера.</response>
        [HttpPost]
        public async Task<AuthenticateInfo> RefreshToken(CancellationToken cancellationToken)
        {
            var refreshToken = Request.Cookies["refreshToken"];
            var request = new RefreshTokenCommand()
            {
                IpAddess = IpAddress(),
                Token = refreshToken
            };
            
            var response = await _mediator.Send(request, cancellationToken);
            
            SetTokenCookie(response.RefreshToken);
        
            return response;
        }
        
        /// <summary>
        /// Удалить токен.
        /// </summary>
        /// <response code = "200" > Успешное выполнение.</response>
        /// <response code = "500" > Непредвиденная ошибка сервера.</response>
        [HttpPost]
        public async Task RevokeToken(RevokeTokenCommand command, CancellationToken cancellationToken)
        {
            var token = command.Token ?? Request.Cookies["refreshToken"];
        
            if (string.IsNullOrEmpty(token))
                throw new  ValidationException("Token is required");
        
            var request = new RevokeTokenCommand()
            {
                IpAddess = IpAddress(),
                Token = token
            };
            
            await _mediator.Send(request, cancellationToken);
        }
        
        /// <summary>
        /// Регистрация нового пользователя.
        /// </summary>
        /// <response code = "200" > Успешное выполнение.</response>
        /// <response code = "401" > Данный запрос требует аутентификации.</response>
        /// <response code = "500" > Непредвиденная ошибка сервера.</response>
        [HttpPost]
        [AllowAnonymous]
        public async Task RegisterUser(RegisterUserCommand command, CancellationToken cancellationToken)
        {
            command.IpAddess = IpAddress();
            await _mediator.Send(command, cancellationToken);
        }

        private void SetTokenCookie(string token)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(7)
            };
            Response.Cookies.Append("refreshToken", token, cookieOptions);
        }
        
        private void DeleteTokenCookie()
        {
            Response.Cookies.Delete("refreshToken");
        }

        private string IpAddress()
        {
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                return Request.Headers["X-Forwarded-For"];
            return HttpContext.Connection.RemoteIpAddress != null
                ? HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString() 
                : string.Empty;
        }
    }
}