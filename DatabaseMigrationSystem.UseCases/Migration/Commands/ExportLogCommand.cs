using DatabaseMigrationSystem.Common.Dto.Request;
using MediatR;

namespace DatabaseMigrationSystem.UseCases.Migration.Commands;

public class ExportLogCommand : IRequest<byte[]>
{
    public Guid Id { get; set; }
}