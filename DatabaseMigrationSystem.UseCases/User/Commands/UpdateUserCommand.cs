using MediatR;

namespace DatabaseMigrationSystem.UseCases.User.Commands;

/// <summary>
/// Обновить пользователя
/// </summary>
public class UpdateUserCommand : IRequest
{
    /// <summary>
    /// Id.
    /// </summary>
    public int UserId { get; set; }
        
    /// <summary>
    /// Логин.
    /// </summary>
    public string Login { get; set; }
}