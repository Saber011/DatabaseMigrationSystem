using DatabaseMigrationSystem.Common.Dto;
using MediatR;

namespace DatabaseMigrationSystem.UseCases.User.Queries;

public class GetUsersQuery : IRequest<UserDto[]>
{
    
}