using System.Reflection;
using Foreca.Infrastructure;
using Foreca.Infrastructure.Database.Database;
using Foreca.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Foreca.App.Extensions;

public static class ServiceConfigureExtensions
{
    public static IServiceCollection ConfigureServices(this IServiceCollection services,
        ConfigurationManager configurationManager)
    {
        services.AddDbContext<ForecaContext>(p =>
            p.UseNpgsql(configurationManager.GetConnectionString(nameof(ForecaContext))));

        Assembly.Load(typeof(InfrastructureAssembly).Assembly.GetName());

        AppDomain.CurrentDomain
            .GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .Where(type => type is
            {
                IsClass: true,
                IsAbstract: false,
            })
            .Where(type => type.GetInterfaces().Contains(typeof(IAutoRegisteredService)))
            .ToList()
            .ForEach(type =>
            {
                var interfaceImplementedConstraint = type.GetInterfaces()
                    .Where(p => p.IsAssignableTo(typeof(IAutoRegisteredService)))
                    .Where(p => p.IsGenericType)
                    .FirstOrDefault();

                if (interfaceImplementedConstraint != null)
                {
                    services.AddTransient(interfaceImplementedConstraint.GenericTypeArguments.FirstOrDefault(), type);
                }
                else
                {
                    services.AddTransient(type);
                }
            });

        return services;
    }
}