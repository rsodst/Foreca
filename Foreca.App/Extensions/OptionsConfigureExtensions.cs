using System.Reflection;
using Foreca.Infrastructure;
using Foreca.Shared;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Foreca.App.Extensions;

public static class OptionsConfigureExtensions
{
    public static IServiceCollection ConfigureOptions(this IServiceCollection services,
        ConfigurationManager configurationManager)
    {
        configurationManager.AddJsonFile(Path.Combine($"{AppDomain.CurrentDomain.BaseDirectory}", "Files",
            "options.json"));

        configurationManager.AddEnvironmentVariables();

        Assembly.Load(typeof(InfrastructureAssembly).Assembly.GetName());

        AppDomain.CurrentDomain
            .GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .Where(type => type.GetInterfaces().Contains(typeof(IAutoRegisteredOptions)))
            .ToList()
            .ForEach(type =>
            {
                var configuration = configurationManager.GetSection(type.Name);

                var genericMethod = typeof(OptionsConfigurationServiceCollectionExtensions)
                    .GetMethods()
                    .Where(p => p.IsGenericMethod)
                    .Where(p => p.GetParameters().Length == 2)
                    .Where(p => p.Name == nameof(OptionsConfigurationServiceCollectionExtensions.Configure))
                    .FirstOrDefault()
                    .MakeGenericMethod(type);

                genericMethod.Invoke(null, new object[] { services, configuration });
            });

        return services;
    }
}