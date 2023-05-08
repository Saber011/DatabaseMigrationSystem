using DatabaseMigrationSystem.ApplicationServices.Interfaces.Account;
using DatabaseMigrationSystem.DataAccess.Interfaces.User;
using DatabaseMigrationSystem.Infrastructure.DbContext.Entities;
using Microsoft.AspNetCore.Http;

namespace DatabaseMigrationSystem.ApplicationServices.Implementations.Account;

public class GetCurrentUserInfoService : IGetCurrentUserInfoService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IGetUserByTokenRepository _getUserByTokenRepository;

    public GetCurrentUserInfoService(IGetUserByTokenRepository getUserByTokenRepository, IHttpContextAccessor httpContextAccessor)
    {
        _getUserByTokenRepository = getUserByTokenRepository;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<User> Handle(CancellationToken cancellationToken)
    {

        var token = _httpContextAccessor.HttpContext?.Request.Cookies["refreshToken"];
        if (token is not null)
        {
            return await _getUserByTokenRepository.Get(token, cancellationToken);
        }
        
        return null;
    }
}