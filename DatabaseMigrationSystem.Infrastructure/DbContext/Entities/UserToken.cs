namespace DatabaseMigrationSystem.Infrastructure.DbContext.Entities;

public class UserToken
{
    /// <summary>
    /// 
    /// </summary>
    public int UserId { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public string Token { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public DateTime Expires { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public DateTime Created { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public string CreatedByIp { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public DateTime Revoked { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public string RevokedByIp { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public string ReplacedByToken { get; set; }

    public User User { get; set; }
}