using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CavipetrolTestBack.Infrastructure.Configuration
{
    public interface IDependencyRegister
    {
        void RegisterDependencies(IConfiguration configuration, IServiceCollection services);

        public static void RegisterAssembliesDependencies(IServiceCollection services, IConfiguration configuration)
        {
            var registerType = typeof(IDependencyRegister);
            var types = AppDomain.CurrentDomain.GetAssemblies().Where(p => !p.IsDynamic)
                .SelectMany(s => s.GetExportedTypes())
                .Where(p => registerType.IsAssignableFrom(p) && !p.IsInterface);
            foreach (var type in types)
            {
                object target = Activator.CreateInstance(type);
                Type DependenyRegisterUnboundType = typeof(IDependencyRegister);
                MethodInfo RegisterDependenciesMethod = DependenyRegisterUnboundType.GetMethod("RegisterDependencies");
                RegisterDependenciesMethod.Invoke(target, new object[] { configuration, services });
            }
        }
    }
}