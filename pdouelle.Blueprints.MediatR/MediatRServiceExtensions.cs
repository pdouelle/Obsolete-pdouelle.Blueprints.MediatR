using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace pdouelle.Blueprints.MediatR
{
    public static class MediatRServiceExtensions
    {
        public static IServiceCollection AddBlueprintMediatR(this IServiceCollection services, params Assembly[] assemblies)
        {
            services.AddMediatR(typeof(MediatRServiceExtensions).Assembly);
            services.AddAutoMapper(typeof(MediatRServiceExtensions).Assembly);
            
            ApiResources.Assemblies = assemblies;

            return services;
        }
    }
}