namespace DatabaseMigrationSystem.Infrastructure.Configurations;

public class JwtSettings
{
    /// <summary>
    /// 
    /// </summary>
    public string Secret { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public int AccessTokenExpiration { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public int RefreshTokenExpiration { get; set; }
}