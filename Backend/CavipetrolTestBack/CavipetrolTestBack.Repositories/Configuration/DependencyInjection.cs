using CavipetrolTestBack.DTOs.Contracts;
using CavipetrolTestBack.Infrastructure.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace CavipetrolTestBack.Repositories.Configuration
{
    public class DependencyInjection : IDependencyRegister
    {
        public void RegisterDependencies(IConfiguration configuration, IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IRepository<>), typeof(EFRepository<>));

            var assemblyRepositories = (typeof(DependencyInjection).Assembly).DefinedTypes.Where(t => t.Name.EndsWith("Repository") && !t.IsInterface);
            foreach (var repository in assemblyRepositories)
            {
                var interfaces = repository.GetInterfaces().Where(x => !x.Name.Equals(typeof(IRepository<>).Name) && !x.Name.Equals("IDisposable"));

                if (interfaces.Any())
                    services.AddScoped(interfaces.FirstOrDefault(), repository);
            }
        }
    }
}
