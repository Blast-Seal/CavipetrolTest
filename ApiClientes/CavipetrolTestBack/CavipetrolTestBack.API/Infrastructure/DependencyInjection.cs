using CavipetrolTestBack.API.Models;
using CavipetrolTestBack.Repositories.Configuration;
using CavipetrolTestBack.Infrastructure.Configuration;

namespace CavipetrolTestBack.API.Infrastructure
{
    public class DependencyInjection : IDependencyRegister
    {
        public void RegisterDependencies(IConfiguration configuration, IServiceCollection services)
        {
            services.AddScoped<IDBFactory, EntitiesContextFactory>();
            services.ConfigureStartupConfig<ApiConfig>(configuration.GetSection("ApiConfig"));
        }
    }
}
