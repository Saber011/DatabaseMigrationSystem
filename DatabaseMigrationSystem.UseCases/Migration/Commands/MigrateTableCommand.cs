using DatabaseMigrationSystem.Common.Dto.Request;
using MediatR;

namespace DatabaseMigrationSystem.UseCases.Migration.Commands;

public class MigrateTableCommand : IRequest
{
    public MigrateTableRequest[] Tables { get; set; }
}