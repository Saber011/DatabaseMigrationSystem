namespace DatabaseMigrationSystem.Infrastructure.DbContext.Entities;

/// <summary>
/// Пользователь.
/// </summary>
public sealed class User
{
    /// <summary>
    /// id.
    /// </summary>
    public int Id { get; set; }
        
    /// <summary>
    /// Логин.
    /// </summary>
    public string Login { get; set; }
        
    /// <summary>
    /// Пароль.
    /// </summary>
    public string Password { get; set; }
    
    /// <summary>
    /// Признак удаления
    /// </summary>
    public int IsDeleted { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public ICollection<UserRoles> Roles { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public ICollection<UserToken> UserTokens { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public Settings Settings { get; set; }
}