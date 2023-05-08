using System.Security.Cryptography;
using DatabaseMigrationSystem.ApplicationServices.Interfaces.Account;
using DatabaseMigrationSystem.Common.Dto;
using DatabaseMigrationSystem.Infrastructure.Configurations;
using Microsoft.Extensions.Options;

namespace DatabaseMigrationSystem.ApplicationServices.Implementations.Account;

public class GenerateRefreshTokenService : IGenerateRefreshTokenService
{
    private readonly JwtSettings _jwtSettings;
    
    public GenerateRefreshTokenService(IOptions<JwtSettings> config)
    {
        _jwtSettings = config.Value;
    }

    public async Task<RefreshToken> Handle(string request, CancellationToken cancellationToken)
    {
        using var rngCryptoServiceProvider = RandomNumberGenerator.Create();
        var randomBytes = new byte[64];

        rngCryptoServiceProvider.GetBytes(randomBytes);

        return new RefreshToken
        {
            Token = Convert.ToBase64String(randomBytes),
            Expires = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpiration),
            Created = DateTime.UtcNow,
            CreatedByIp = request
        };
    }
}