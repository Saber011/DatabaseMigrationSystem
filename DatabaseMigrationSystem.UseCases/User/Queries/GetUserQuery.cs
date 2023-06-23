using DatabaseMigrationSystem.Common.Dto;
using MediatR;

namespace DatabaseMigrationSystem.UseCases.User.Queries;

public class GetUserQuery : IRequest<UserDto>
{
    public int UserId { get; set; }
}