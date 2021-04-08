using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace StartupModule
{
    public class StartupModuleOptions
    {
        public ICollection<IStartupModule> StartupModules { get; } = new List<IStartupModule>();
        public ICollection<Type> ApplicationInitializers { get; } = new List<Type>();
        public IDictionary<string, object> Settings { get; set; } = new Dictionary<string, object>();
        public void DiscoverStartupModules() => DiscoverStartupModules(Assembly.GetEntryAssembly()!);
        public void DiscoverStartupModules(params Assembly[] assemblies)
        {
            if (assemblies == null || assemblies.Length == 0 || assemblies.All(a => a == null)) {
                throw new ArgumentException("No assemblies", nameof(assemblies));
            }
            foreach (var type in assemblies.SelectMany(a => a.ExportedTypes)) {
                if (typeof(IStartupModule).IsAssignableFrom(type))
                {
                    var instance = Activate(type);
                    StartupModules.Add(instance);
                }
                else if (typeof(IApplicationInitializer).IsAssignableFrom(type)) {
                    ApplicationInitializers.Add(type);
                }
            }
        }

        public void AddStartupModule<T>() where T : IStartupModule
            => AddStartupModule(typeof(T));

        public void AddStartupModule(Type type) {
            if (typeof(IStartupModule).IsAssignableFrom(type))
            {
                var instance = Activate(type);
                StartupModules.Add(instance);
            }
            else {
                throw new ArgumentException($"Specified startup module '{type.Name}' does not implement {nameof(IStartupModule)}.", nameof(type));   
            }
        }

        public void ConfigureMiddleware(Action<IApplicationBuilder, ConfigureMiddlewareContext> action)
            => StartupModules.Add(new InlineMiddlewareConfiguration(action));


        private IStartupModule Activate(Type type)
        {
            try
            {
                return (IStartupModule)Activator.CreateInstance(type);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Failed to create {type.Name} of {nameof(IStartupModule)}", ex);
            }
        }
    }
}