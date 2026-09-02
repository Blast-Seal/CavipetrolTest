using CavipetrolTestBack.Infrastructure.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System;

namespace CavipetrolTestBack.Services.Infrastructure
{
    public class DependencyRegister : IDependencyRegister
    {
        public void RegisterDependencies(IConfiguration configuration, IServiceCollection services)
        {
            var assemblyServices = typeof(DependencyRegister).Assembly.DefinedTypes.Where(t => t.Name.EndsWith("Service") && !t.IsInterface);
            foreach (var service in assemblyServices)
            {
                var interfaces = service.GetInterfaces().Where(x => !x.Name.Equals("IDefaultService") && !x.Name.Equals("IDisposable"));

                if (interfaces.Any())
                    services.AddScoped(interfaces.FirstOrDefault(), service);
            }
        }
    }
}
