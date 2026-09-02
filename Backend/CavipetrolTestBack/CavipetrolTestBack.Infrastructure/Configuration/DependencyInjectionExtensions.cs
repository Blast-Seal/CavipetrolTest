using CavipetrolTestBack.Infrastructure.Configuration;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DependencyInjectionExtensions
    {
        #region methods
        public static IServiceCollection AddCustomServices(this IServiceCollection services, IConfiguration configuration)
        {
            IDependencyRegister.RegisterAssembliesDependencies(services, configuration);
            return services;
        }

        public static TConfig ConfigureStartupConfig<TConfig>(this IServiceCollection services, IConfiguration configuration) where TConfig : class, new()
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));

            //create instance of config
            var config = new TConfig();

            //bind it to the appropriate section of configuration
            //configuration.Bind(config);


            //and register it as a service
            services.AddSingleton(config);

            return config;
        }
        #endregion
    }
}
