using DatabaseMigrationSystem.Infrastructure.PipelineBehavior;
using DatabaseMigrationSystem.UseCases.Account.Mappings;
using DatabaseMigrationSystem.Utils.Modules;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace DatabaseMigrationSystem.UseCases
{
    public class UseCasesModule : ApplicationModule
    {
        public override void Load(IServiceCollection services)
        {
            services.AddMediatR(typeof(AccountAutoMapperProfile));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddValidatorsFromAssembly(typeof(UseCasesModule).Assembly);
            
        }
    }
}
