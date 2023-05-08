namespace DatabaseMigrationSystem.Infrastructure.DbContext.Entities;

/// <summary>
/// 
/// </summary>
public class Role
{
    /// <summary>
    /// 
    /// </summary>
    public int RoleId { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public UserRoles[] Roles { get; set; }
}