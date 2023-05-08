using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DatabaseMigrationSystem.Utils.Modules
{
    public abstract class ApplicationModule 
    {
        public IConfiguration Configuration { get; set; }

        public abstract void Load(IServiceCollection services);
    }
}