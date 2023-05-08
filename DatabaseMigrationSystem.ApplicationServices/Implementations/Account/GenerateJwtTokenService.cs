using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DatabaseMigrationSystem.ApplicationServices.Interfaces.Account;
using DatabaseMigrationSystem.Infrastructure.Configurations;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace DatabaseMigrationSystem.ApplicationServices.Implementations.Account;

public class GenerateJwtTokenService : IGenerateJwtTokenService
{
    private readonly JwtSettings _jwtSettings;
    
    public GenerateJwtTokenService(IOptions<JwtSettings> config)
    {
        _jwtSettings = config.Value;
    }

    public async Task<string> Handle(long request, CancellationToken cancellationToken)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new(ClaimTypes.Name, request.ToString())
            }),
            Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.AccessTokenExpiration),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
        };
        
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}