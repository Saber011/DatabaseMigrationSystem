using MediatR;

namespace DatabaseMigrationSystem.UseCases.Settings.Queries;

public class GetSettingsQuery : IRequest<Infrastructure.DbContext.Entities.Settings>
{
    
}