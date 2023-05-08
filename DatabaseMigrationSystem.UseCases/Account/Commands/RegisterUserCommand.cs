using System.Text.Json.Serialization;
using MediatR;

namespace DatabaseMigrationSystem.UseCases.Account.Commands;

public class RegisterUserCommand : IRequest
{
    /// <summary>
    /// Логин.
    /// </summary>
    public string Login { get; set; }
        
    /// <summary>
    /// Пароль.
    /// </summary>
    public string Password { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    [JsonIgnore]
    public string IpAddess { get; set; }
}