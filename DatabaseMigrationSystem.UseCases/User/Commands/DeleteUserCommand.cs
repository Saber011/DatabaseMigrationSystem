using MediatR;

namespace DatabaseMigrationSystem.UseCases.User.Commands;

/// <summary>
/// Удалить пользователя
/// </summary>
public class DeleteUserCommand : IRequest
{
    /// <summary>
    /// ИД пользователя
    /// </summary>
    public int UserId { get; set; }
}